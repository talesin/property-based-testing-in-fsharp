namespace Diamond.FSharp

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

module Diamonds =
    let size (ch:char) = int ch - int 'A' + 1

    let charNum ch = int ch - int 'A'

    let numChar x = char <| int 'A' + x

    let pad = String.replicate

    let diamond1 (ch:char) =
        ""

    let diamond2 (ch:char) =
        string ch

    let diamond3 (ch:char) =
        [ 0 .. size ch * 2 - 2 ]
        |> List.map (fun x -> string x)
        |> List.toArray
        |> String.concat "\n"

    let diamond4 (ch:char) =
        let n = charNum ch
        [ 0 .. n] @ [ n - 1 .. -1 .. 0 ]
        |> List.map (fun x -> string <| numChar x)
        |> List.toArray
        |> String.concat "\n"

    let diamond5 (ch:char) =
        let n = charNum ch
        [ 0 .. n] @ [ n - 1 .. -1 .. 0 ]
        |> List.map (fun x -> sprintf "%s%c" (pad (n - x) " ") (numChar x))
        |> List.toArray
        |> String.concat "\n"

    let diamond6 (ch:char) =
        let n = charNum ch
        [ 0 .. n] @ [ n - 1 .. -1 .. 0 ]
        |> List.map (fun x -> (pad (n - x) " ") + (pad ((x+1)*2-1) (string (numChar x))))
        |> List.toArray
        |> String.concat "\n"

    let diamond7 (ch:char) =
        let n = charNum ch
        [ 0 .. n] @ [ n - 1 .. -1 .. 0 ]
        |> List.map (fun x ->
            if x = 0 then
                sprintf "%s%c" (pad (n - x) " ") (numChar x)
            else
                sprintf "%s%c%s%c" (pad (n - x) " ") (numChar x) (pad (x*2-1) " ") (numChar x))
        |> List.toArray
        |> String.concat "\n"