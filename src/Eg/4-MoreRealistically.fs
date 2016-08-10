namespace Examples

open System
open System.Configuration
open System.Web
open System.Threading.Tasks

open Microsoft.Owin
open log4net

open NUnit.Framework
open FsCheck
open FsCheck.NUnit
open Foq


type Method = Method of string
type Route = Route of string
type SiteKey = SiteKey of string

type StatsdMiddlewareGenerators () =
    static let _null = Gen.constant null

    static member OwinMiddleware () =
        { new Arbitrary<OwinMiddleware>() with
            override x.Generator = gen {
                let mw = { new OwinMiddleware(null) with
                                member this.Invoke (ctx) = Task.Run(fun () -> ())}

                return mw }}

    static member OwinContext () =
        { new Arbitrary<IOwinContext>() with
            override x.Generator = gen {

                let sc = Mock<IReadableStringCollection>()
                            .Create()

                let rq = Mock<IOwinRequest>()
                            .Setup(fun x -> <@ x.Query @>).Returns(sc)
                            .Create()

                let rs = Mock<IOwinResponse>()
                            .Setup(fun x -> <@ x.StatusCode @>).Returns(1)
                            .Create()

                let! ctx = Mock<IOwinContext>()
                            .Setup(fun x -> <@ x.Get<string>(any()) @>).Returns("")
                            .Setup(fun x -> <@ x.Request @>).Returns(rq)
                            .Setup(fun x -> <@ x.Response @>).Returns(rs)
                            .Create()
                            |> Gen.constant
                            |> fun x -> Gen.oneof [ x; _null ]

                return ctx }}

module ``4 More Realistically`` =


    [<Property(Arbitrary=[|typeof<StatsdMiddlewareGenerators>|])>]
    let ``When invoked should result in logging of timing and count`` (owinMiddleware:OwinMiddleware) (owinContext:IOwinContext) =
        let statsd = mock()
        let log = mock()

        let statsdmw = SampleMiddleware (owinMiddleware, statsd, log) 

        statsdmw.Invoke (owinContext) |> Async.AwaitTask |> Async.RunSynchronously


