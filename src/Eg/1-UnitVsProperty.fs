namespace Examples

open NUnit.Framework

open FsCheck
open FsCheck.NUnit

module ``1 Unit Tests vs Property Tests`` =


    module ``1-1 Sample`` =
        let increment = (+) 1

        [<Test>]
        let ``The increment of 2 should be 3`` () =
            let result = increment 2
            Assert.True(3 = result)

        [<Property(Verbose=true)>]
        let ``The different between x and the expected result should be 1`` (x:int) =
            let result = increment x

            result - x = 1

       
    module ``1-2 Reversing`` =
        let reverse = List.rev

        [<Test>]
        let ``Reversing a specific list should return that list in reverse`` () =
            let result = reverse [1; 2; 3; 4; 5]
            Assert.True([5; 4; 3; 2; 1] = result)

        [<Property(Verbose=true)>]
        let ``The reverse of the reverse of the list should equal the original`` (list:int list) =
            let doubleReversed = list |> reverse |> reverse

            doubleReversed = list
