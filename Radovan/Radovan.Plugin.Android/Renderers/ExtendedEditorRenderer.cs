using System;
using System.ComponentModel;
using Android.Graphics;
using Radovan.Plugin.Android.Renderers;
using Radovan.Plugin.Core.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedEditor), typeof(ExtendedEditorRenderer))]
namespace Radovan.Plugin.Android.Renderers
{
public class ExtendedEditorRenderer : EditorRenderer
{
	protected override void OnElementChanged(
		ElementChangedEventArgs<Editor> e)
	{
		base.OnElementChanged(e);

		if (e.NewElement != null)
		{
			var element = e.NewElement as ExtendedEditor;
			this.Control.Hint = element.Placeholder;

			Control.Background.SetColorFilter(element.BorderColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
		}
	}

	protected override void OnElementPropertyChanged(
		object sender,
		PropertyChangedEventArgs e)
	{
		base.OnElementPropertyChanged(sender, e);

		if (e.PropertyName == ExtendedEditor.PlaceholderProperty.PropertyName)
		{
			var element = this.Element as ExtendedEditor;
			this.Control.Hint = element.Placeholder;
		}
	}	}
}
