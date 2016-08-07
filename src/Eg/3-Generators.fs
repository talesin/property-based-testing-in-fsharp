namespace Examples

open System

open NUnit.Framework

open FsCheck
open FsCheck.NUnit

// Another element of property based testing is the need to generate specifc data for your tests.
module ``3 Generators`` =

    let sort = List.sort


    let vsort versions =
        // F# helpfully turns the out parameters into tuple return values.
        // Here we use pattern matching on the output to either return the converted interger or a default of 0
        let parseInt s =
            match Int32.TryParse s with
            | (true, x)  -> x
            | (false, _) -> 0
    
        
        // Another example of pattern matching, this time using the `function` keyword which is shortcut
        // to using `match` with a single argument
        let versionToTuple (ver:string) =
            ver.Split '.'
            |> Array.map parseInt
            |> function
            | [| x; y; z; |] -> (x, y, x)
            | _              -> (0, 0, 0)

        versions
        |> List.map versionToTuple
        |> List.sort

    // We need to generate version numbers where we can specifically test the numerical
    // sorting, such as comparing 1.3.5 to 1.20.0 and expect it to be ordered correctly.
    type Versions =
        static member Versions () =
            // Here `gen` looks like a keyword, it is what's called a computation expression in F#.
            gen {
                // Let bang (let!) allows use to pull the value out of the function
                let! major = Gen.choose (0, 3)
                let! minor = Gen.oneof [ Gen.choose (0, 9); Gen.choose (10, 99) ]
                let! patch = Gen.choose (0, 999)

                return sprintf "%d.%d.%d" major minor patch
            } |> Arb.fromGen

    

         
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