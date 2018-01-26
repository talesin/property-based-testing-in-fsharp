#load "Header.fsx"
#load "4-CSharp.fs"


open FsCheck

open Examples

open ``4 CSharp``

Arb.registerByType typeof<WidgetGen>
//Check.Quick ``Widget vetter vets widgets according to vetting rules``
Check.Verbose ``Widget vetter vets widgets according to vetting rules``