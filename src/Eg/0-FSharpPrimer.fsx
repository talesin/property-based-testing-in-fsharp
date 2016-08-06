// A short and inadequate primer to F#

// Copy lines to F# Interactive to see the results


// Values and functions are declared using let bindings
// These are immutable by default.

let a = 1

let b = "foo"

let addWithTypes (a:int) (b:int) = a + b

addWithTypes 1 2

// In many circumstances you do not need to declare types, the compiler will attempt to infer the types
// based on there use.
let addNoTypes a b = a + b

addNoTypes 3 4

// Uncomment the below and see what happens, now comment the above one.
//addNoTypes 3.0 4.0


// Lambdas can be used in place of functions
let addFromLambda = fun a b -> a + b

addFromLambda 5 6

// Functions can be passed in or returned like any other value
let applyfunc f x = f x

applyfunc addFromLambda 7 8

applyfunc (fun a b -> a + b) 9 10


let addAndReturnFunc a = fun b -> a + b

(addAndReturnFunc 11) 12

// Functions are "curried" by default in F#, this means functions with multiple arguments can be thought of
// taking only one argument and then returning another function that handles the next argument. The two
// functions below are equivalent, have a look at their signatures to confirm.,

let addMany a b c d = a + b + c + d

let curriedAdd a = fun b -> (fun c -> (fun d -> a + b + c + d))

addMany 1 2 3 4

curriedAdd 1 2 3 4

// The pipe (|>) operator is used with abandon in F#, and allows you to reverse the input order when
// calling a chain of functions. It is declared as:

let (|>) x f = f x

// Given the simply functions below

let add a b = a + b
let multiply a b = a * b
let square a = a * a

// The following statements are equivalent

square (add 3 (multiply 2 3))

multiply 2 3 |> add 3 |> square

// It is quite normal to spread this over multiple lines

multiply 2 3
|> add 3
|> square

// It'll be fiarly common to use tuples in F#, where their syntax is easier to use than in C#.
// Below we declare a tuple and then retrieve it's values

let point = (1, 2)

let x, y = point


// Lists are a big part of functional programming langages and F# is no exception. You may be familar with List<T>
// in C# however F# lists are quite different, essentially being based on good old dependable singly-linked lists
// whereas C# lists use arrays.

// In F# an empty list is represented by [] and constructing by the :: (cons) operator. Head represents the first item
// in the list and tail all the items apart from the first.
//
// Let's see some examples for constructing lists - copy the following into the REPL to see the result.

let empty         = []

let madeWithCons  = 1 :: 2 :: 3 :: 4 :: []

let explicit      = [ 1; 2; 3; 4 ]

let multiline     = [
                      1
                      2
                      3
                      4
                    ]

let range         = [ 1 .. 10 ]

let rangeWithSkip = [ 1 .. 2 .. 10 ]

let alphaRange    = [ 'a' .. 'z' ]

let floatRange    = [ 0.1 .. 0.1 .. 1.0 ]

let downRange     = [ 10 .. (-1) .. 1 ]

//  You can append to a list using the '@' symbol, however this iterates through the items in the first list before
//  it can return the newly combined list. 

let appendedList  = [ 1; 2; 3 ] @ [ 4; 5; 6 ]


// Now that you have an idea of what lists are and how to construct them, let's look at some of the common functions
// that are used with them.

// List.map lets you apply a function to each item in the list.
// The LINQ equivalent is Select
[ 1 .. 10 ]
|> List.map (fun x -> x * x)

// List.filter lets you filter items in the list that conform to the predicate passed in.
// The LINQ equivalent is Where
[ 1 .. 10 ]
|> List.filter (fun x -> x % 2 = 0)

// List.sum will return the sum of the items in the list.
[ 1 .. 10]
|> List.sum

// List.length will return the number of items in the list.
[ 1 .. 10 ]
|> List.length

// List.rev will reverse the items in the list
[ 1 .. 10 ]
|> List.rev

// List.reduce will successively apply a function to each item in the list, passing in the accumulated value with
// the list item on each iteration result in a value of the same type as the list
[ 1 .. 10 ]
|> List.reduce (fun acc x -> acc + x)

// List.fold, like List.reduce, will apply a function to each item in the list, however it starts off with an initial
// value that is passed in and does not have to be of the same type as the list. 
// The LINQ equivalent for this is Aggregate
[ 1 .. 10 ]
|> List.fold (fun state x -> sprintf "%s %d" state x) "Numbers:"

// List.pairwise will convert your list into another list of tuples each containing itself and it's predecessor
[ 1 .. 10 ]
|> List.pairwise

// List.forall returns true if each element in the list matches the predicate
[ 1 .. 10 ]
|> List.forall (fun x -> x <= 10)

// List.contains returns true when the list contains the specified item
[ 1 .. 10 ]
|> List.contains 3