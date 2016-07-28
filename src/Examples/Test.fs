namespace Examples

open NUnit.Framework

open FsCheck
open FsCheck.NUnit


// Reverse of the reverse of a list
module Lists =


    [<Property>]
    let ``Reversing a list 1`` (xs:int list) =
        "The reverse of the list should not be the same" @| (xs <> List.rev xs)

        .&.

        "The reverse of the reverse of the list should qual the original" @| (List.rev (List.rev xs) = xs)

    type NonEmptyList =
        static member List () =
            Gen.choose (0, 9)
            |> Gen.nonEmptyListOf
            |> Arb.fromGen

    [<Property(Arbitrary=[| typeof<NonEmptyList> |])>]
    let ``Reversing a list 2`` (xs:int list) =
        "The reverse of the list should not be the same" @| (xs <> List.rev xs)

        .&.

        "The reverse of the reverse of the list should qual the original" @| (List.rev (List.rev xs) = xs)
    

    type ListOfAtLeastLengthTwo =
        static member List () =
            gen {
                let! n = Gen.choose(2, 5)
                return! Gen.choose (0, 9)
                        |> Gen.listOfLength n
            }|> Arb.fromGen

    [<Property(Arbitrary=[| typeof<ListOfAtLeastLengthTwo> |])>]
    let ``Reversing a list 3`` (xs:int list) =
        "The reverse of the list should not be the same" @| (xs <> List.rev xs)

        .&.

        "The reverse of the reverse of the list should qual the original" @| (List.rev (List.rev xs) = xs)

     
    type ListWithAtLeastOneDistinctItem =
        static member List () =
            gen {
                let! n = Gen.choose(1, 3)
                let! xs = Gen.choose (0, 4) |> Gen.listOfLength n
                let! ys = Gen.choose (5, 9) |> Gen.listOfLength n

                return xs @ ys
            }|> Arb.fromGen

    [<Property(Arbitrary=[| typeof<ListWithAtLeastOneDistinctItem> |])>]
    let ``Reversing a list 4`` (xs:int list) =
        "The reverse of the list should not be the same" @| (xs <> List.rev xs)

        .&.

        "The reverse of the reverse of the list should qual the original" @| (List.rev (List.rev xs) = xs)   