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
                list.Length = sorted.Length)

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
            printfn "%A" list
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


    module ``Dollar Example`` =
        type Currency = Dollar of int
            with
                member this.Value = match this with | Dollar d -> d
                member this.Add x = Dollar (this.Value + x)
                member this.Times x = Dollar (this.Value * x)

        [<Property>]
        let ``The value create for a Dollar should be the value retrieved`` (value:int) =
            let dollar = Dollar value
            
            dollar.Value = value

        [<Property>]
        let ``Multiplying a dollar value should be the same as creating with the multiplication`` (value:int) (multiplier:int) =
            let d1 = (Dollar value).Times multiplier
            let d2 = Dollar (value * multiplier)

            d1 = d2

        [<Property>]
        let ``Adding a dollar value should be the same as creating with the addition`` (value:int) (addition:int) =
            let d1 = (Dollar value).Add addition
            let d2 = Dollar (value + addition)

            d1 = d2


    module ``Dollar Example 2`` =
        type Currency = Dollar of int
        with
            member this.Value = match this with | Dollar d -> d
            member this.Map f = (f this.Value)
            member this.Add x = this.Map ((+) x)
            member this.Times x = this.Map ((*) x)


        [<Property>]
        let ``Mapping a dollar value should be the same as creating with the result of the map`` (value:int) (f:int->int) =
            let d1 = (Dollar value).Map f
            let d2 = Dollar (f value)

            d1 = d2

        
        [<Property(Verbose=true)>]
        let ``Mapping a dollar value should be the same as creating with the result of the map (2)`` (value:int) (F (_,f)) =
            let d1 = (Dollar value).Map f
            let d2 = Dollar (f value)

            d1 = d2

