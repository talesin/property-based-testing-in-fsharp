namespace Examples

open System
open System.Collections.Generic

open FsCheck
open FsCheck.Xunit



module ``4 CSharp`` =

    // Creates an IWidget using an object expression
    let widget name size = {
        new Object() with
            override x.ToString() = sprintf "(%s, %d)" name size
        interface IWidget with
            member x.Name = name
            member x.Size = size }

    // Creates an IWidgetMaker
    let widgetMaker (widgets: IWidget list) =
        let stack = Stack (widgets |> List.rev)
        { new IWidgetMaker with 
            member x.MakeWidget() = stack.Pop()
            member x.CanMake = stack.Count > 0 }

    // Composition of widgetMaker and WidgetProducer
    let widgetProducer = widgetMaker >> WidgetProducer

    // And finally a function that we can use in our tests
    let produceWidgets fn xs = (widgetProducer xs).ProduceWidgets (fun x -> fn x) |> Seq.toList


    type Widgets = class end

    [<Property(Verbose=true, Arbitrary=[| typeof<Widgets> |])>]
    let ``WidgetProducer produces and filters widgets`` (widgets: IWidget list) =
        false