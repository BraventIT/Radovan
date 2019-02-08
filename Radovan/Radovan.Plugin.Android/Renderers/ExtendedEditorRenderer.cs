using System;
using System.ComponentModel;
using Android.Graphics;
using Radovan.Plugin.Android.Renderers;
using Radovan.Plugin.Core.Controls;
using Xamarin.Forms;
using Android.Graphics.Drawables;
using Android.Content.Res;

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

            Control.SetHintTextColor(element.HintTextColor.ToAndroid());

            Control.SetPadding(30, 30, 30, 30);

            var customColor = Xamarin.Forms.Color.FromHex("#868686");                
            GradientDrawable gradient = new GradientDrawable();
            gradient.SetStroke(1, customColor.ToAndroid());
            Control.SetBackground(gradient);
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
