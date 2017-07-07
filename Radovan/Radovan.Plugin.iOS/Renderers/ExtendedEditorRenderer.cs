using System;
using System.ComponentModel;
using Radovan.Plugin.Core.Controls;
using Radovan.Plugin.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedEditor), typeof(ExtendedEditorRenderer))]
namespace Radovan.Plugin.iOS.Renderers
{
public class ExtendedEditorRenderer : EditorRenderer
{
	private string Placeholder { get; set; }

	protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
	{
		base.OnElementChanged(e);
		var element = this.Element as ExtendedEditor;

		if (Control != null && element != null)
		{
			Placeholder = element.Placeholder;
			Control.TextColor = UIColor.LightGray;
			Control.Text = Placeholder;

			Control.Layer.CornerRadius = 3;
            Control.Layer.BorderColor = element.BorderColor.ToCGColor();
			Control.Layer.BorderWidth = 2;

			Control.ShouldBeginEditing += (UITextView textView) =>
			{
				if (textView.Text == Placeholder)
				{
					textView.Text = "";
					textView.TextColor = UIColor.Black; // Text Color
					}

				return true;
			};

			Control.ShouldEndEditing += (UITextView textView) =>
			{
				if (textView.Text == "")
				{
					textView.Text = Placeholder;
					textView.TextColor = UIColor.LightGray; // Placeholder Color
					}

				return true;
			};
		}
	}

	protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		base.OnElementPropertyChanged(sender, e);

		var view = (ExtendedEditor)Element;

		if (e.PropertyName == ExtendedEditor.PlaceholderProperty.PropertyName)
		{
			Placeholder = view.Placeholder;
			Control.TextColor = UIColor.LightGray;
			Control.Text = Placeholder;
		}
	}	}
}
