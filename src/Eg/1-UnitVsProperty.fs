namespace Examples

open NUnit.Framework

open FsCheck
open FsCheck.NUnit

module ``1 Unit Tests vs Property Tests`` =


    module ``1-1 Sample`` =
        let increment = (+) 1

        [<Test>]
        let ``1 Sample unit test`` () =
            let result = increment 2
            Assert.True(3 = result)

        [<Property>]
        let ``2 Sample property test`` (x:int) =
            printfn "%i" x
            let result = increment x
            "The different between x and the expected result should be 1" @| (result - x = 1)

       
    module ``1-2 Reversing`` =
        let reverse = List.rev


        [<Test>]
        let ``3 Reversing a list unit test`` () =
            let result = reverse [1; 2; 3; 4; 5]
            Assert.True([5; 4; 3; 2; 1] = result)

        [<Property>]
        let ``4 Reversing a list property test`` (list:int list) =
            printfn "%A" list
            let result = list |> reverse |> reverse
            "The reverse of the reverse of the list should equal the original" @| (result = list)