namespace Examples

open NUnit.Framework

open FsCheck
open FsCheck.NUnit

module ``2 List Sorting`` =
    let sort = List.sort

    [<Property>]
    let ``2-1 List Sort`` (list:int list) =
        let sorted = sort list

        "A sorted list should contain the same number of elements as the original" @| (list.Length = sorted.Length)

        .&.

        "Each element in a sorted list should be greater than the last" @| (
            sorted
            |> List.pairwise
            |> List.forall (fun (a, b) -> b > a)
        )