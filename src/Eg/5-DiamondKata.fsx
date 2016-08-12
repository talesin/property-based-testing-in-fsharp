#load "Header.fsx"
#load "5-DiamondKata.fs"

open FsCheck

open Examples

open ``5 Diamond Kata``

Arb.registerByType typeof<Letter>
Check.Verbose ``Diamond A``
Check.Verbose ``Diamond B``
Check.Verbose ``Diamond C``
Check.Verbose ``Test Diamond``
