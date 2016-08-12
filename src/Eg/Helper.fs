namespace Examples

#if NUNIT
open FsCheck
open FsCheck.NUnit

type TestAttribute = global.NUnit.Framework.TestAttribute

type Assert =
    static member IsTrue (x:bool) = global.Xunit.Assert.True x
#else
open FsCheck
open FsCheck.Xunit

type TestAttribute = global.Xunit.FactAttribute

type Assert =
    static member IsTrue (x:bool) = global.Xunit.Assert.True x
#endif
