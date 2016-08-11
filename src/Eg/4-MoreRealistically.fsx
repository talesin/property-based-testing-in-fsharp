#load "Header.fsx"
#load "4-MoreRealistically.fs"


open NUnit.Framework
open FsCheck
open FsCheck.NUnit

open Examples

open ``4 More Realistically``

Arb.registerByType typeof<StatsdMiddlewareGenerators>
Check.Quick ``When invoked should result in logging of timing and count``
Check.Verbose ``When invoked should result in logging of timing and count``