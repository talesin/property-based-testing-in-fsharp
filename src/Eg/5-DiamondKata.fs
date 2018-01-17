namespace Examples

open FsCheck
open FsCheck.Xunit

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

    // Diamond function - build up
    let diamond (ch:char) =
        ""

    // Generator - modify as needed
    type Letter =
        static member Letter () =
            Arb.Default.Char ()
            

    // Test function - flesh out with properties
    let diamondTest (diamond:char->string) (ch:char) =
        let result = diamond ch

        "Diamond must not be empty" @| (
            result <> null && result.Length > 0)


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


    [<Property(Verbose=true, Arbitrary=[| typeof<Letter> |])>]
    let ``Test Diamond`` (ch:char) = diamondTest diamond ch