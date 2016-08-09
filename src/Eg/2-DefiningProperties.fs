namespace Examples

open System

open NUnit.Framework

open FsCheck
open FsCheck.NUnit

// TODO move completed code to branch and leave behind stub

module ``2 Defining Properties`` =

    module ``2-1 Sorting a list`` =
        //let sort list = []
        //let sort list = list
        //let sort (list:int list) = List.replicate list.Length 1
        let sort = List.sort

        [<Property>]
        let ``List Sort`` (list:int list) =
            let sorted = sort list

            // invariant property
            "A sorted list should contain the same number of elements as the original" @| (
                List.length list = List.length sorted)

            .&.

            // idempotent property
            "Sorting a list twice should be the same as sorting it once" @| (
                sorted = sort sorted)

            .&.

            // invariant property - but we want to try and avoid reimplementing sorting code
            "Each element in a sorted list should be greater than or equal to the last" @| (
                sorted
                |> List.pairwise
                |> List.forall (fun (a, b) -> b >= a))

            .&.

            // invariant property
            "The smallest element in the list should be first" @| (
                let head = list @ [Int32.MinValue] |> sort |> List.head

                head = Int32.MinValue)

            .&.

            // invariant property
            "The largest element in the list should be last" @| (
                let last = Int32.MaxValue :: list |> sort |> List.last

                last = Int32.MaxValue)

            .&.

            // invariant property
            "Each element in the original list must exist in the sorted" @| (
                list
                |> List.forall (fun x -> sorted |> List.contains x))


    module ``2-2 Reversing a list`` =
        //let reverse list = []
        //let reverse list = list
        //let reverse (list:int list) = List.replicate list.Length 1
        let reverse = List.rev

        [<Property>]
        let ``Reversing a list property test`` (list:int list) (x:int) =
            let doubleReversed = list |> reverse |> reverse

            // inverse property
            "The reverse of the reverse of the list should equal the original" @| (
                doubleReversed = list)

            .&.

            // communicative property
            "Prepended value then reversed should be the same as reversed and appended" @| (
                let prepended = x :: list |> reverse
                let appended = (list |> reverse) @ [x]

                prepended = appended
            )


