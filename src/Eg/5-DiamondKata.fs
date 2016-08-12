namespace Examples

#if NUNIT
open FsCheck
open FsCheck.NUnit
#else
open FsCheck
open FsCheck.Xunit
#endif

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

module ``5 Diamond Kata`` =

    let size (ch:char) = int ch - int 'A' + 1

    let charNum ch = int ch - int 'A'

    let numChar x = char <| int 'A' + x

    let pad = String.replicate

    let diamond (ch:char) =
        let n = charNum ch
        [ 0 .. n] @ [ n - 1 .. -1 .. 0 ]
        |> List.map (fun x ->
            if x = 0 then
                sprintf "%s%c" (pad (n - x) " ") (numChar x)
            else
                sprintf "%s%c%s%c" (pad (n - x) " ") (numChar x) (pad (x*2-1) " ") (numChar x))
        |> List.toArray
        |> String.concat "\n"

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
    let ``Test Diamond`` (ch:char) = diamondTest diamond