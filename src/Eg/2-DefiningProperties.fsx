#load "Header.fsx"
#load "2-DefiningProperties.fs"

open FsCheck

open Examples

open ``2 Defining Properties``
open ``2-1 Sorting a list``
open ``2-2 Reversing a list``

Check.Quick ``List Sort``
//Check.Verbose ``List Sort``

Check.Quick ``Reversing a list property test``
//Check.Verbose ``Reversing a list property test``

