using System;
using System.ComponentModel;
using Radovan.Plugin.Core.Controls;
using Radovan.Plugin.iOS.Extensions;
using Radovan.Plugin.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedCheckBox), typeof(ExtendedCheckBoxRenderer))]
namespace Radovan.Plugin.iOS.Renderers
{
	public class ExtendedCheckBoxRenderer : ViewRenderer<ExtendedCheckBox, ExtendedCheckBoxView>
	{
		private UIColor defaultTextColor;

		protected override void OnElementChanged(ElementChangedEventArgs<ExtendedCheckBox> e)
		{
			base.OnElementChanged(e);

			if (Element == null) return;

			BackgroundColor = Element.BackgroundColor.ToUIColor();
			if (e.NewElement != null)
			{
				if (Control == null)
				{
					var checkBox = new ExtendedCheckBoxView(Bounds);
					checkBox.TouchUpInside += (s, args) => Element.Checked = Control.Checked;
					defaultTextColor = checkBox.TitleColor(UIControlState.Normal);
					SetNativeControl(checkBox);
				}
				Control.LineBreakMode = UILineBreakMode.CharacterWrap;
				Control.VerticalAlignment = UIControlContentVerticalAlignment.Top;
				Control.CheckedTitle = string.IsNullOrEmpty(e.NewElement.CheckedText) ? e.NewElement.DefaultText : e.NewElement.CheckedText;
				Control.UncheckedTitle = string.IsNullOrEmpty(e.NewElement.UncheckedText) ? e.NewElement.DefaultText : e.NewElement.UncheckedText;
				Control.Checked = e.NewElement.Checked;
				UpdateTextColor();
			}

			Control.Frame = Frame;
			Control.Bounds = Bounds;

			UpdateFont();
		}

		private void ResizeText()
		{
			if (Element == null)
				return;

			var text = Element.Checked ? string.IsNullOrEmpty(Element.CheckedText) ? Element.DefaultText : Element.CheckedText :
				string.IsNullOrEmpty(Element.UncheckedText) ? Element.DefaultText : Element.UncheckedText;

			var bounds = Control.Bounds;

			var width = Control.TitleLabel.Bounds.Width;

			var height = text.StringHeight(Control.Font, width);

			var minHeight = string.Empty.StringHeight(Control.Font, width);

			var requiredLines = Math.Round(height / minHeight, MidpointRounding.AwayFromZero);

			var supportedLines = Math.Round(bounds.Height / minHeight, MidpointRounding.ToEven);

			if (supportedLines != requiredLines)
			{
				bounds.Height += (float)(minHeight * (requiredLines - supportedLines));
				Control.Bounds = bounds;
				Element.HeightRequest = bounds.Height;
			}
		}

		public override void Draw(CoreGraphics.CGRect rect)
		{
			base.Draw(rect);
			ResizeText();
		}

		private void UpdateFont()
		{
			if (!string.IsNullOrEmpty(Element.FontName))
			{
				var font = UIFont.FromName(Element.FontName, (Element.FontSize > 0) ? (float)Element.FontSize : 12.0f);
				if (font != null)
				{
					Control.Font = font;
				}
			}
			else if (Element.FontSize > 0)
			{
				var font = UIFont.FromName(Control.Font.Name, (float)Element.FontSize);
				if (font != null)
				{
					Control.Font = font;
				}
			}
		}

		private void UpdateTextColor()
		{
			Control.SetTitleColor(Element.TextColor.ToUIColorOrDefault(defaultTextColor), UIControlState.Normal);
			Control.SetTitleColor(Element.TextColor.ToUIColorOrDefault(defaultTextColor), UIControlState.Selected);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			switch (e.PropertyName)
			{
				case "Checked":
					Control.Checked = Element.Checked;
					break;
				case "TextColor":
					UpdateTextColor();
					break;
				case "CheckedText":
					Control.CheckedTitle = string.IsNullOrEmpty(Element.CheckedText) ? Element.DefaultText : Element.CheckedText;
					break;
				case "UncheckedText":
					Control.UncheckedTitle = string.IsNullOrEmpty(Element.UncheckedText) ? Element.DefaultText : Element.UncheckedText;
					break;
				case "FontSize":
					UpdateFont();
					break;
				case "FontName":
					UpdateFont();
					break;
				case "Element":
					break;
				default:
					System.Diagnostics.Debug.WriteLine("Property change for {0} has not been implemented.", e.PropertyName);
					return;
			}
		}
	}
}
