using System;
using Android.Graphics;
using Radovan.Plugin.Android.Renderers;
using Radovan.Plugin.Core.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NoBorderButton), typeof(NoBorderButtonRenderer))]
namespace Radovan.Plugin.Android.Renderers
{
	public class NoBorderButtonRenderer : ButtonRenderer
	{
		protected override void OnDraw(Canvas canvas)
		{
			base.OnDraw(canvas);
		}
	}
}
