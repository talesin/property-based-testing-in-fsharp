namespace Diamond.Tests

open NUnit.Framework

open FsCheck
open FsCheck.NUnit

// Print a diamond based on a letter, there the letter determines both the
// content and size of the diamond.
//
//
// A returns:
// 
// A
//
// B returns:
//
//  A
// B B
//  A
//
// C returns:
//
//   A
//  B B 
// C   C
//  B B 
//   A


module DiamondTests =
    open Diamond.FSharp.Diamonds

    type Letter =
        static member Letter () =
            Gen.choose (int 'A', int 'F')
            |> Gen.map char
            |> Arb.fromGen

    let diamondTest (diamond:char->string) (ch:char) =
        let n = size ch

        let lookup =
            [ 'A' .. ch ] @ List.rev [ 'A' .. char (int ch - 1) ]
            |> List.toArray

        let result = diamond ch

        let lines = result.Split [|'\n'|]

        printfn "%c (%i / %i):\n%s" ch lines.Length n result

        "Diamond must not be empty" @| (
            result <> null && result.Length > 0)

        .&. "Diamond should contain double the lines minus one of size" @| (
            lines.Length = n * 2 - 1)

        .&. "Ignoring whitespace, each line should contain only the letter corresponding to the line number" @| (
            lines
            |> Array.mapi (fun i s -> s |> String.forall (fun c -> c = ' ' || c = lookup.[i]))
            |> Array.forall id)

       .&. "Index of first letter should equal size minus the letter order" @| (
           lines
           |> Array.mapi (fun i s -> s.IndexOf(lookup.[i]) = n - (int lookup.[i] - int 'A') - 1)
           |> Array.forall id)

       .&. "Index of second letter should equal letter order plus the size" @| (
           lines
           |> Array.mapi (fun i s -> s.LastIndexOf(lookup.[i]) = int lookup.[i] - int 'A' + n - 1)
           |> Array.forall id)

       .&. "The first and last lines should contain one letter, all others two" @| (
           lines
           |> Array.mapi (fun i s -> (s |> String.filter (fun c -> c <> ' ') |> String.length) = if (i = 0 || i = lookup.Length-1) then 1 else 2)
           |> Array.forall id)



    [<Property(MaxTest=1)>]
    let ``Diamond A`` () =
        diamondTest (fun _ -> "A") 'A'

    [<Property(MaxTest=1)>]
    let ``Diamond B`` () =
        let diamondB =
            " A\n" +
            "B B\n" + 
            " A"

        diamondTest (fun _ -> diamondB) 'B'

    [<Property(MaxTest=1)>]
    let ``Diamond C`` () =
        let diamondC =
            "  A\n" +
            " B B\n" + 
            "C   C\n" +
            " B B\n" + 
            "  A"

        diamondTest (fun _ -> diamondC) 'C'


    [<Property(Arbitrary=[| typeof<Letter> |])>]
    let ``Diamond 1 (FSharp)`` (ch:char) = diamondTest diamond1

    [<Property(Arbitrary=[| typeof<Letter> |])>]
    let ``Diamond 2 (FSharp)`` (ch:char) = diamondTest diamond2

    [<Property(Arbitrary=[| typeof<Letter> |])>]
    let ``Diamond 3 (FSharp)`` (ch:char) = diamondTest diamond3

    [<Property(Arbitrary=[| typeof<Letter> |])>]
    let ``Diamond 4 (FSharp)`` (ch:char) = diamondTest diamond4

    [<Property(Arbitrary=[| typeof<Letter> |])>]
    let ``Diamond 5 (FSharp)`` (ch:char) = diamondTest diamond5

    [<Property(Arbitrary=[| typeof<Letter> |])>]
    let ``Diamond 6 (FSharp)`` (ch:char) = diamondTest diamond6

    [<Property(Arbitrary=[| typeof<Letter> |])>]
    let ``Diamond 7 (FSharp)`` (ch:char) = diamondTest diamond7


    [<Property(Arbitrary=[| typeof<Letter> |])>]
    let ``Diamond 1 (CSharp)`` (ch:char) = diamondTest Diamond.CSharp.Diamonds.Diamond1

    [<Property(Arbitrary=[| typeof<Letter> |])>]
    let ``Diamond 2 (CSharp)`` (ch:char) = diamondTest Diamond.CSharp.Diamonds.Diamond2

    [<Property(Arbitrary=[| typeof<Letter> |])>]
    let ``Diamond 3 (CSharp)`` (ch:char) = diamondTest Diamond.CSharp.Diamonds.Diamond3

    [<Property(Arbitrary=[| typeof<Letter> |])>]
    let ``Diamond 4 (CSharp)`` (ch:char) = diamondTest Diamond.CSharp.Diamonds.Diamond4

    [<Property(Arbitrary=[| typeof<Letter> |])>]
    let ``Diamond 5 (CSharp)`` (ch:char) = diamondTest Diamond.CSharp.Diamonds.Diamond5

    [<Property(Arbitrary=[| typeof<Letter> |])>]
    let ``Diamond 6 (CSharp)`` (ch:char) = diamondTest Diamond.CSharp.Diamonds.Diamond6

    [<Property(Arbitrary=[| typeof<Letter> |])>]
    let ``Diamond 7 (CSharp)`` (ch:char) = diamondTest Diamond.CSharp.Diamonds.Diamond7