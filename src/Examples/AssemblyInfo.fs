﻿namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Examples")>]
[<assembly: AssemblyProductAttribute("PropertyBasedTestingInFSharp")>]
[<assembly: AssemblyDescriptionAttribute("A short example of property based testing using F#")>]
[<assembly: AssemblyVersionAttribute("1.0")>]
[<assembly: AssemblyFileVersionAttribute("1.0")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "1.0"
    let [<Literal>] InformationalVersion = "1.0"
