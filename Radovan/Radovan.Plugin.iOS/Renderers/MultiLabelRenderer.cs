using System;
using Radovan.Plugin.Core.Controls;
using Radovan.Plugin.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MultiLineLabel), typeof(MultiLabelRenderer))]

namespace Radovan.Plugin.iOS.Renderers
{
	public class MultiLabelRenderer : LabelRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			MultiLineLabel multiLineLabel = (MultiLineLabel)Element;

			if (multiLineLabel != null && multiLineLabel.Lines != -1)
				Control.Lines = multiLineLabel.Lines;
		}
	}
}
