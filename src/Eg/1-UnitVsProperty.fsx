#load "Header.fsx"
#load "1-UnitVsProperty.fs"


open NUnit.Framework
open FsCheck
open FsCheck.NUnit

open Examples
open ``1 Unit Tests vs Property Tests``
open  ``1-1 Sample``
open  ``1-2 Reversing``

Check.Quick ``The different between x and the expected result should be 1``
//Check.Verbose ``The different between x and the expected result should be 1``

Check.Quick  ``The reverse of the reverse of the list should equal the original``
//Check.Verbose  ``The reverse of the reverse of the list should equal the original``


