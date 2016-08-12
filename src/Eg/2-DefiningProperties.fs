namespace Examples

open System

#if NUNIT
open FsCheck
open FsCheck.NUnit
#else
open FsCheck
open FsCheck.Xunit
#endif

// Now that we've tackled one property test, let's try a few more!
module ``2 Defining Properties`` =

    module ``2-1 Sorting a list`` =

        // Let's try and come up with some properties iteratively
        let sort list = []
        //let sort list = list
        //let sort (list:int list) = List.replicate list.Length 1
        //let sort = List.sort

        [<Property(Verbose=true)>]
        let ``List Sort`` (list:int list) =
            let sorted = sort list

            false 


    // Let's go back to the reverse function and see if we can apply some of what we learned above
    module ``2-2 Reversing a list`` =
        
        // Cycle through the implementations of `reverse` if it helps...
        let reverse list = []
        //let reverse list = list
        //let reverse (list:int list) = List.replicate list.Length 1
        //let reverse = List.rev

        [<Property(Verbose=true)>]
        let ``Reversing a list property test`` (list:int list) (x:int) =
            let doubleReversed = list |> reverse |> reverse

            false


