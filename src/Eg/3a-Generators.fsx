#I "../../packages/FsCheck/lib/portable-net45+netcore45"
#r "FsCheck.dll"

open FsCheck

let print s g = Gen.eval 10 (Random.mkStdGen System.DateTime.Now.Ticks) g |> printfn "%s: %A" s

// Gen.choose randomly chooses a value betweem the lower and upper bounds
Gen.choose (0, 10)
|> print "Gen.choose"

// Gen.listOf generates a list of random size using the given generator for the elements
Gen.listOf (Gen.choose (0, 10))
|> print "Gen.listOf"


// Gen.map will apply the function to the given generator to create a new generator
Gen.map (fun x -> x * x) (Gen.choose (0, 10))
|> print "Gen.map"

// Gen.oneof returns one of the given generators
Gen.oneof [ Gen.choose (0, 9); Gen.choose (10, 99); Gen.choose (100, 999)]
|> print "Gen.oneof"

// Gen.two, Gen.three and Gen.four generator tuples of that size with the given generator
Gen.two (Gen.choose (0, 10))
|> print "Gen.two"

Gen.three (Gen.choose (0, 10))
|> print "Gen.three"

Gen.four (Gen.choose (0, 10))
|> print "Gen.four"

// Gen.constant returns the given value as a generator
Gen.constant 10
|> print "Gen.constant"

// The `gen` keyword (actually a computation expression, but that's a discussion for another day)
// allows you to do more complicated code using the special let bang (!) keyword
gen {
    let! x = Gen.choose (0, 10)
    let! xs = Gen.listOf (Gen.choose (0, x))

    return (x, xs)
} |> print "gen"