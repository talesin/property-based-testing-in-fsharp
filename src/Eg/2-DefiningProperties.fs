namespace Examples

open NUnit.Framework

open FsCheck
open FsCheck.NUnit

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

            // invariant property
            "Each element in a sorted list should be greater than or equal to the last" @| (
                sorted
                |> List.pairwise
                |> List.forall (fun (a, b) -> b >= a))

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


