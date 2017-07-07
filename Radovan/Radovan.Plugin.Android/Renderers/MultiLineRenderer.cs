using System;
using Radovan.Plugin.Android.Renderers;
using Radovan.Plugin.Core.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MultiLineLabel), typeof(MultiLineRenderer))]

namespace Radovan.Plugin.Android.Renderers
{
	public class MultiLineRenderer : LabelRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			MultiLineLabel multiLineLabel = (MultiLineLabel)this.Element;

			if (multiLineLabel != null && multiLineLabel.Lines != -1)
			{
				this.Control.SetSingleLine(false);
				this.Control.SetLines(multiLineLabel.Lines);
			}
		}
	}
}