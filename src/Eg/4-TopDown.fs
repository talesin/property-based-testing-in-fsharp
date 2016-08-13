namespace Examples

open System
open System.Configuration
open System.Web
open System.Threading.Tasks

open Microsoft.Owin
open log4net

// We will be using an F# mocking framework called Foq
open Foq

#if NUNIT
open FsCheck
open FsCheck.NUnit
#else
open FsCheck
open FsCheck.Xunit
#endif




type StatsdMiddlewareGenerators () =
    
    // A rudimentary little helper function that returns either the item or null
    static let thisOrNull x = Gen.oneof [
                                Gen.constant x
                                Gen.constant null ]

    // For the OwinMiddleware will use an object expression to handle the Invoke method
    static member OwinMiddleware () =
        { new Arbitrary<OwinMiddleware>() with
            override x.Generator = gen {
                let mw = { new OwinMiddleware(null) with
                                // Dummy invoke method
                                member this.Invoke (ctx) = Task.Run(fun () -> ()) }

                // Use our little helper method to return object or null
                return! thisOrNull mw }}

    // OwinContext with a few mocked methods and properties - this is the sort of thing
    // you may do with your own classes
    static member OwinContext () =
        { new Arbitrary<IOwinContext>() with
            override x.Generator = gen {

                let! sc = Mock<IReadableStringCollection>()
                            .Create()
                            |> thisOrNull

                let! rq = Mock<IOwinRequest>()
                            .Setup(fun x -> <@ x.Query @>).Returns(sc)
                            .Create()
                            |> thisOrNull

                let! rs = Mock<IOwinResponse>()
                            .Setup(fun x -> <@ x.StatusCode @>).Returns(1)
                            .Create()
                            |> thisOrNull

                let! ctx = Mock<IOwinContext>()
                            .Setup(fun x -> <@ x.Get<string>(any()) @>).Returns("")
                            .Setup(fun x -> <@ x.Request @>).Returns(rq)
                            .Setup(fun x -> <@ x.Response @>).Returns(rs)
                            .Create()
                            |> thisOrNull

                return ctx }}

module ``4 Top Down`` =

    // The class we're testing is pretty simple, no real business logic so we'll be testing more for
    // for overall robustness than testing properties of functions
    [<Property(Verbose=true, MaxTest=10, Arbitrary=[|typeof<StatsdMiddlewareGenerators>|])>]
    let ``Test Owin Middleware component`` (owinMiddleware:OwinMiddleware) (owinContext:IOwinContext) =
        // The mock() function simple returns a default mock of the required type
        //let statsd = mock()
        //let log = mock()

        let timing = ResizeArray<string * int64> ()
        let count = ResizeArray<string * int> ()
        let error = ResizeArray<string * exn> ()
        let warn = ResizeArray<string> ()

        //let statsdmw = SampleMiddleware (owinMiddleware, statsd, log) 
        let statsdmw = SampleMiddleware (   owinMiddleware,
                                            (fun s l -> timing.Add (s, l)),
                                            (fun s i -> count.Add (s, i)),
                                            (fun s e -> error.Add(s, e)),
                                            (fun s -> warn.Add(s)))

        // Uncomment to invoke code
        statsdmw.Invoke (owinContext) |> Async.AwaitTask |> Async.RunSynchronously

        (owinMiddleware <> null) ==> ("There should be no errors logged when owinMiddleware is not null" @| (error.Count = 0))

