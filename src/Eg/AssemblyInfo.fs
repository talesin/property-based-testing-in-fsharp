namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Eg")>]
[<assembly: AssemblyProductAttribute("PropertyBasedTestingInFSharp")>]
[<assembly: AssemblyDescriptionAttribute("A short example of property based testing using F#")>]
[<assembly: AssemblyVersionAttribute("0.1")>]
[<assembly: AssemblyFileVersionAttribute("0.1")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.1"
    let [<Literal>] InformationalVersion = "0.1"
