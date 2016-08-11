#load "Header.fsx"
#load "4-TopDown.fs"


open NUnit.Framework
open FsCheck
open FsCheck.NUnit

open Examples

open ``4 Top Down``

Arb.registerByType typeof<StatsdMiddlewareGenerators>
//Check.Quick ``Test Owin Middleware component``
Check.Verbose ``Test Owin Middleware component``