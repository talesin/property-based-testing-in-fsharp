namespace Examples

open System
open FsCheck
open FsCheck.Xunit

// Now that we've tackled one property test, let's try a few more!
module ``2 Defining Properties`` =

    module ``2-1 Sorting a list`` =

        // Let's try and come up with some properties iteratively
        //let sort list = []
        // let sort list = list
        // let sort (list:int list) = List.replicate list.Length 1
        let sort = List.sort

        [<Property(Verbose=true)>]
        let ``List Sort`` (list:int list) =
            let sorted = sort list

            // invariant property - this type of property will not be changed by sort
            "A sorted list should contain the same number of elements as the original" @| (
                List.length list = List.length sorted)

            // You can `AND` or `OR` properties in a single test using .&. or .|. 
            .&.

            // idempotent property - repeating the function shouldn't change the data
            "Sorting a list twice should be the same as sorting it once" @| (
                sorted = sort sorted)

            .&.

            // verification property - in this case being ordered is relatively easy to verify
            "Each element in a sorted list should be greater than or equal to the last" @| (
                sorted
                |> List.pairwise
                |> List.forall (fun (a, b) -> b >= a))

            .&.

            // verification property
            "The smallest element in the list should be first" @| (
                let head = list @ [Int32.MinValue] |> sort |> List.head

                head = Int32.MinValue)

            .&.

            // verification property
            "The largest element in the list should be last" @| (
                let last = Int32.MaxValue :: list |> sort |> List.last

                last = Int32.MaxValue)

            .&.

            // invariant property
            "Each element in the original list must exist in the sorted" @| (
                list
                |> List.forall (fun x -> sorted |> List.contains x))


    // Let's go back to the reverse function and see if we can apply some of what we learned above
    module ``2-2 Reversing a list`` =
        
        // Cycle through the implementations of `reverse` if it helps...
        // let reverse list = []
        // let reverse list = list
        let reverse (list:int list) = List.replicate list.Length 1
        //let reverse = List.rev

        [<Property(Verbose=true)>]
        let ``Reversing a list property test`` (list:int list) (x:int) =
            let doubleReversed = list |> reverse |> reverse

            // inverse property - useful for for testing serialize/deserialize style of functions
            "The reverse of the reverse of the list should equal the original" @| (
                doubleReversed = list)

            .&.

            // communicative property - prepending a value then reversing the list should be the same as
            // applying reverse then appending the value
            "Prepended value then reversed should be the same as reversed and appended" @| (
                let prepended = x :: list |> reverse
                let appended = (list |> reverse) @ [x]

                prepended = appended
            )

