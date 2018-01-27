#load "Header.fsx"
#load "1-UnitVsProperty.fs"

open FsCheck

open Examples
open ``1 Unit Tests vs Property Tests``
open  ``1-1 Sample``
open  ``1-2 Reversing``

Check.Quick ``The difference between x and the expected result should be 1``
//Check.Verbose ``The difference between x and the expected result should be 1``

