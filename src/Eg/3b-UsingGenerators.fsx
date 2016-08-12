#load "Header.fsx"
#load "3b-UsingGenerators.fs"

open FsCheck

open Examples

open ``3 Using Generators``

Arb.registerByType typeof<Versions>
Check.Quick ``Sort version numbers``
//Check.Verbose ``Sort version numbers``

Arb.registerByType typeof<NumericStringList>
Check.Quick ``The sort of major version should be the same as a numerical sort``
//Check.Verbose ``The sort of major version should be the same as a numerical sort``