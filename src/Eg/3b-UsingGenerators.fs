namespace Examples

open System
open FsCheck
open FsCheck.Xunit

// Another element of property based testing is the need to generate specifc data for your tests. Generators allow you
// to do that for existing types but also your custom types
module ``3 Using Generators`` =

    // F# helpfully turns the out parameters into tuple return values.
    // Here we use pattern matching on the output to either return the converted integer or a default of 0
    let parseInt s =
        match Int32.TryParse s with
        | (true, x)  -> x
        | (false, _) -> 0

    let sort = List.sort

//    let sort versions =
//        // Another example of pattern matching, this time using the `function` keyword which is shortcut
//        // to using `match` with a single argument
//        let versionToTuple (ver:string) =
//            ver.Split '.'
//            |> Array.map parseInt
//            |> function
//            | [| x; y; z; |] -> (x, y, x)
//            | _              -> (0, 0, 0)
//
//        versions
//        |> List.sortBy versionToTuple

    // We need to generate version numbers where we can specifically test the numerical
    // sorting, such as comparing 1.3.5 to 1.20.0 and expect it to be ordered correctly.
    type Versions =
        static member Versions () =
            Gen.choose (0, 999)
            |> Gen.three
            |> Gen.map (Version >> string)
            |> Arb.fromGen

    type NumericStringList =
        static member NumericStringList () =
            Gen.constant () |> Arb.fromGen

    [<Property(Verbose=true, Arbitrary=[| typeof<Versions> |])>]
    let ``Sort version numbers`` (list:string list) =
        let sorted = sort list

        //printfn "%A" sorted

        // invariant property
        "A sorted list should contain the same number of elements as the original" @| (
            List.length list = List.length sorted)

    // test oracle - comparing to an existing implementation
    [<Property(Verbose=true, Arbitrary=[| typeof<NumericStringList> |])>]
    let ``The sort of major version should be the same as a numerical sort`` (list:string list) =
        false




