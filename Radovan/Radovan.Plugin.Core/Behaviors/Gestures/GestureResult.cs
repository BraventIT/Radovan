using System;
using System.Collections.Generic;
using Radovan.Plugin.Core.Enums;
using Xamarin.Forms;

namespace Radovan.Plugin.Core.Behaviors
{
	public class GestureResult
	{

		public GestureType GestureType { get; set; }

		public Directionality Direction { get; set; }

		public Point Origin { get; set; }

		public Point Origin2 { get; set; }

		public View StartView { get; set; }

		public Double Length { get; set; }

		public Double VerticalDistance { get; set; }

		public Double HorizontalDistance { get; set; }

		public List<View> ViewStack { get; set; }
	}
}
