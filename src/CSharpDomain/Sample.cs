using System;
using System.Collections.Generic;
using System.Linq;

namespace Examples
{
	public interface IWidget
	{
		string Name { get; }
		int Size { get; }
	}

	public interface IWidgetMaker
	{
		IWidget MakeWidget();
		bool CanMake { get; }
	}

	public class WidgetProducer
	{
		readonly IWidgetMaker maker;

        public WidgetProducer(IWidgetMaker maker) => this.maker = maker;

        public IEnumerable<IWidget> ProduceWidgets(Func<IWidget,bool> isValid)
		{
			while (maker.CanMake)
			{
				var widget = maker.MakeWidget();
				if (isValid(widget)) yield return widget;
			}
		}
	}
}
