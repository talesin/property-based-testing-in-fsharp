namespace Examples

// 'open' in F# is the same as 'using' in C#
open Xunit
open FsCheck.Xunit

// A module can be thought of as a static class in C#
module ``1 Unit Tests vs Property Tests`` =

    // Double backticks allow you to use any character when naming an item
    module ``1-1 Sample`` =

        // Function to test
        let increment = (+) 1

        // Bog standard unit test
        [<Fact>]
        let ``The increment of 2 should be 3`` () =
            let result = increment 2
            Assert.True(3 = result)

        // The property attribute turns a function into a proprty test.  Notice the argument to the test?
        // A generated value will be passed in there for each iteration.
        [<Property(Verbose=true)>]
        let ``The difference between x and the expected result should be 1`` (x:int) =
            let result = increment x

            // For a property test you can either return value or a `Property` - we'll look at that shortly
            result - x = 1

      
    // Let's look at something a slightly more interesting, how to test reversing the elements of a list. 
    module ``1-2 Reversing`` =

        // Let's pretend this is our specialized implementation of reverse
        let reverse = List.rev

        // Again, your bog standard unit test - testing the one specific case
        [<Fact>]
        let ``Reversing a specific list should return that list in reverse`` () =
            let result = reverse [1; 2; 3; 4; 5]
            Assert.True([5; 4; 3; 2; 1] = result)

        // We need to come up with something to test, ideally a `property` of the reverse function
        // that doesn't mean re-implementing the reverse function
        [<Property(Verbose=true)>]
        let ``What property of reverse should we test?`` (list:int list) =
            ()