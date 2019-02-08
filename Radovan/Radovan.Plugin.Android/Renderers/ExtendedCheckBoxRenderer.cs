using System;
using System.ComponentModel;
using Android.Content.Res;
using Android.Graphics;
using Android.Widget;
using Radovan.Plugin.Android.Renderers;
using Radovan.Plugin.Core.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedCheckBox), typeof(ExtendedCheckBoxRenderer))]
namespace Radovan.Plugin.Android.Renderers
{
    public class ExtendedCheckBoxRenderer : ViewRenderer<ExtendedCheckBox, CheckBox>
	{
		private ColorStateList defaultTextColor;

		protected override void OnElementChanged(ElementChangedEventArgs<ExtendedCheckBox> e)
		{
			base.OnElementChanged(e);

			if (this.Control == null)
			{
				var checkBox = new CheckBox(this.Context);
				checkBox.CheckedChange += CheckBoxCheckedChange;

				defaultTextColor = checkBox.TextColors;
				this.SetNativeControl(checkBox);
			}

			Control.Text = e.NewElement.Text;
			Control.Checked = e.NewElement.Checked;
			UpdateTextColor();

			if (e.NewElement.FontSize > 0)
			{
				Control.TextSize = (float)e.NewElement.FontSize;
			}

			if (!string.IsNullOrEmpty(e.NewElement.FontName))
			{
				Control.Typeface = TrySetFont(e.NewElement.FontName);
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			switch (e.PropertyName)
			{
				case "Checked":
					Control.Text = Element.Text;
					Control.Checked = Element.Checked;
					break;
				case "TextColor":
					UpdateTextColor();
					break;
				case "FontName":
					if (!string.IsNullOrEmpty(Element.FontName))
					{
						Control.Typeface = TrySetFont(Element.FontName);
					}
					break;
				case "FontSize":
					if (Element.FontSize > 0)
					{
						Control.TextSize = (float)Element.FontSize;
					}
					break;
				case "CheckedText":
				case "UncheckedText":
					Control.Text = Element.Text;
					break;
				default:
					System.Diagnostics.Debug.WriteLine("Property change for {0} has not been implemented.", e.PropertyName);
					break;
			}
		}

		void CheckBoxCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
		{
			this.Element.Checked = e.IsChecked;
		}

		private Typeface TrySetFont(string fontName)
		{
			Typeface tf = Typeface.Default;
			try
			{
				tf = Typeface.CreateFromAsset(Context.Assets, fontName);
				return tf;
			}
			catch (Exception ex)
			{
				Console.Write("not found in assets {0}", ex);
				try
				{
					tf = Typeface.CreateFromFile(fontName);
					return tf;
				}
				catch (Exception ex1)
				{
					Console.Write(ex1);
					return Typeface.Default;
				}
			}
		}

		private void UpdateTextColor()
		{
			if (Control == null || Element == null)
				return;

			if (Element.TextColor == Xamarin.Forms.Color.Default)
				Control.SetTextColor(defaultTextColor);
			else
				Control.SetTextColor(Element.TextColor.ToAndroid());
		}
	}
}
