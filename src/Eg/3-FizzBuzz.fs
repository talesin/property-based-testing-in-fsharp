namespace Examples

open NUnit.Framework

open FsCheck
open FsCheck.NUnit

[<AutoOpen>]
module ``3 FizzBuzz`` =

    let fizzOrBuzz x =
        if x % 15 = 0 then              "FizzBuzz"
        else if x % 3 = 0 then          "Fizz"
        else if x % 5 = 0 then          "Buzz"
        else                            string x   

    module ``3-1 FizzBuzz Unit Tests`` =

        [<Test>]
        let ``fizzOrBuzz should return FizzBuzz when given 15``  () =
            let result = fizzOrBuzz 15
            Assert.True("FizzBuzz" = result)

        [<Test>]
        let ``fizzOrBuzz should return Fizz when given 3``  () =
            let result = fizzOrBuzz 3
            Assert.True("Fizz" = result)

        [<Test>]
        let ``fizzOrBuzz should return Buzz when given 5``  () =
            let result = fizzOrBuzz 5
            Assert.True("Buzz" = result)

        [<Test>]
        let ``fizzOrBuzz should return Buzz when given 7``  () =
            let result = fizzOrBuzz 7
            Assert.True("7" = result)


    module ``3-2 FizzBuzz Property Tests`` =

        [<Property>]
        let ``fizzOrBuzz properties should hold true`` (x:int) =
            let result = fizzOrBuzz x

            "FizzBuzz should be returned for any number divisible by both 3 and 5" @| (
                x % 15 = 0
                && result = "FizzBuzz")

            .|.

            "Fizz should be returned for any number divisible by just 3" @| (
                x % 3 = 0
                && result = "Fizz")

            .|.

            "Buzz should be returned for any number divisible by just 5" @| (
                x % 5 = 0
                && result = "Buzz")

            .|.

            "The number should be return if it's not divisible by 3 or 5" @| (
                x % 3 <> 0
                && x % 5 <> 0
                && result = string x)


    module ``3-3 FizzBuzz Property Tests With Conditionals`` =

        [<Property>]
        let ``FizzBuzz should be returned for any number divisible by both 3 and 5`` (x:int) =
            let result = fizzOrBuzz x

            x % 15 = 0 ==> (result = "FizzBuzz")

        [<Property>]
        let ``Fizz should be returned for any number divisible by just 3`` (x:int) =
            let result = fizzOrBuzz x

            (x % 3 = 0 && x % 5 <> 0) ==> (result = "Fizz")

        [<Property>]
        let ``Buzz should be returned for any number divisible by just 5`` (x:int) =
            let result = fizzOrBuzz x

            (x % 5 = 0 && x % 3 <> 0) ==> (result = "Buzz")

        [<Property>]
        let ``The number should be returned for any number not divisible by 3 or 5`` (x:int) =
            let result = fizzOrBuzz x

            (x % 5 <> 0 && x % 3 <> 0) ==> (result = string x)


    module ``3-4 FizzBuzz Property Tests With Generator`` =

        type FizzBuzzNumbers =
            static member Numbers () =
                gen {
                    let! multiple = Gen.elements [ 1; 3; 5 ]
                    let! factor = Gen.choose (1, 50)
                    return multiple * factor
                } |> Arb.fromGen


        [<Property(Arbitrary = [| typeof<FizzBuzzNumbers> |])>]
        let ``FizzBuzz should be returned for any number divisible by both 3 and 5`` (x:int) =
            let result = fizzOrBuzz x

            x % 15 = 0 ==> (result = "FizzBuzz")

        [<Property(Arbitrary = [| typeof<FizzBuzzNumbers> |])>]
        let ``Fizz should be returned for any number divisible by just 3`` (x:int) =
            let result = fizzOrBuzz x

            (x % 3 = 0 && x % 5 <> 0) ==> (result = "Fizz")

        [<Property(Arbitrary = [| typeof<FizzBuzzNumbers> |])>]
        let ``Buzz should be returned for any number divisible by just 5`` (x:int) =
            let result = fizzOrBuzz x

            (x % 5 = 0 && x % 3 <> 0) ==> (result = "Buzz")

        [<Property(Arbitrary = [| typeof<FizzBuzzNumbers> |])>]
        let ``The number should be returned for any number not divisible by 3 or 5`` (x:int) =
            let result = fizzOrBuzz x

            (x % 5 <> 0 && x % 3 <> 0) ==> (result = string x)
