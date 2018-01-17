#load "Header.fsx"
#load "4-TopDown.fs"


open FsCheck

open Examples

open ``4 Top Down``

Arb.registerByType typeof<StatsdMiddlewareGenerators>
//Check.Quick ``Test Owin Middleware component``
Check.Verbose ``Test Owin Middleware component``