namespace Examples

open System

open NUnit.Framework

open FsCheck
open FsCheck.NUnit

// Another element of property based testing is the need to generate specifc data for your tests. Generators allow you
// to do that for existing types but also your custom types
module ``3 Using Generators`` =

    // F# helpfully turns the out parameters into tuple return values.
    // Here we use pattern matching on the output to either return the converted interger or a default of 0
    let parseInt s =
        match Int32.TryParse s with
        | (true, x)  -> x
        | (false, _) -> 0

    //let sort = List.sort

    let sort versions =
              
        // Another example of pattern matching, this time using the `function` keyword which is shortcut
        // to using `match` with a single argument
        let versionToTuple (ver:string) =
            ver.Split '.'
            |> Array.map parseInt
            |> function
            | [| x; y; z; |] -> (x, y, x)
            | _              -> (0, 0, 0)

        versions
        |> List.sortBy versionToTuple

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
            Gen.oneof [ Gen.choose (0, 9); Gen.choose (0, 99); Gen.choose (0, 999) ]
            |> Gen.map string
            |> Arb.fromGen  

         
    [<Property(Verbose=true, Arbitrary=[| typeof<Versions> |])>]
    let ``Sort version numbers`` (list:string list) =
        let sorted = sort list

        // invariant property
        "A sorted list should contain the same number of elements as the original" @| (
            List.length list = List.length sorted)

        .&.

        // idempotent property
        "Sorting a list twice should be the same as sorting it once" @| (
            sorted = sort sorted)

        .&.

        // invariant property
        "Each element in the original list must exist in the sorted" @| (
            list
            |> List.forall (fun x -> sorted |> List.contains x))

        .&.

        // invariant property
        "The smallest element in the list should be first" @| (
            let head = list @ ["0.0.0"] |> sort |> List.head

            head = "0.0.0")

        .&.

        // invariant property
        "The largest element in the list should be last" @| (
            let last = "999.999.999" :: list |> sort |> List.last

            last = "999.999.999")


    // test oracle - comparing to an existing implementation
    [<Property(Verbose=true, Arbitrary=[| typeof<NumericStringList> |])>]
    let ``The sort of major version should be the same as a numerical sort`` (list:string list) =
        let sorted = list |> List.map (sprintf "%s.0.0") |> sort
        let numbers = list |> List.sortBy parseInt |> List.map (sprintf "%s.0.0")

        sorted = numbers




