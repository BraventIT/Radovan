using System;
using System.IO;
using Foundation;
using Radovan.Plugin.Core.Controls;
using Radovan.Plugin.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedLabel), typeof(ExtendedLabelRenderer))]
namespace Radovan.Plugin.iOS.Renderers
{
	public class ExtendedLabelRenderer : LabelRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			var view = e.NewElement as ExtendedLabel;

			//UpdateUi(view, this.Control);
			if (view != null)
			{
				SetPlaceholder(view);
			}
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var view = Element as ExtendedLabel;

			if (view != null &&
				e.PropertyName == Label.TextProperty.PropertyName ||
				e.PropertyName == Label.FormattedTextProperty.PropertyName ||
				e.PropertyName == ExtendedLabel.PlaceholderProperty.PropertyName ||
				e.PropertyName == ExtendedLabel.FormattedPlaceholderProperty.PropertyName ||
				e.PropertyName == ExtendedLabel.IsDropShadowProperty.PropertyName ||
				e.PropertyName == ExtendedLabel.IsStrikeThroughProperty.PropertyName ||
				e.PropertyName == ExtendedLabel.IsUnderlineProperty.PropertyName)
			{
				SetPlaceholder(view);
			}
		}

		private void UpdateUi(ExtendedLabel view)
		{
			// Prefer font set through Font property.
			if (view.Font == Font.Default)
			{
				if (view.FontSize > 0)
				{
					this.Control.Font = UIFont.FromName(this.Control.Font.Name, (float)view.FontSize);
				}

				if (!string.IsNullOrEmpty(view.FontName))
				{
					var fontName = Path.GetFileNameWithoutExtension(view.FontName);

					var font = UIFont.FromName(fontName, this.Control.Font.PointSize);

					if (font != null)
					{
						this.Control.Font = font;
					}
				}
			}
			else
			{
				try
				{
					var font = UIFont.FromName(view.FontFamily, (float)view.FontSize);
					if (font != null)
						this.Control.Font = font;
				}
				catch (Exception ex)
				{
					var x = ex;
				}
			}

			var underline = view.IsUnderline ? NSUnderlineStyle.Single : NSUnderlineStyle.None;
			var strikethrough = view.IsStrikeThrough ? NSUnderlineStyle.Single : NSUnderlineStyle.None;

			NSShadow dropShadow = null;

			if (view.IsDropShadow)
			{
				dropShadow = new NSShadow
				{
					ShadowColor = view.DropShadowColor.ToUIColor(),
					ShadowBlurRadius = 1.4f,
					ShadowOffset = new CoreGraphics.CGSize(new CoreGraphics.CGPoint(0.3f, 0.8f))
				};
			}

			if (view.TextColor != Color.Default)
			{
				this.Control.TextColor = view.TextColor.ToUIColor();
			}

			this.Control.AttributedText = new NSMutableAttributedString(view.Text,
				this.Control.Font,
				underlineStyle: underline,
				strikethroughStyle: strikethrough,
				shadow: dropShadow);

			if (view.Lines != -1)
				Control.Lines = view.Lines;
		}

		private void SetPlaceholder(ExtendedLabel view)
		{
			if (view == null)
			{
				return;
			}

			if (view.FormattedText != null)
			{
				this.Control.AttributedText = view.FormattedText.ToAttributed(view.Font, view.TextColor);
				LayoutSubviews();
				return;
			}

			if (!string.IsNullOrEmpty(view.Text))
			{
				this.UpdateUi(view);
				LayoutSubviews();
				return;
			}

			if (string.IsNullOrWhiteSpace(view.Placeholder) && view.FormattedPlaceholder == null)
			{
				return;
			}

			var formattedPlaceholder = view.FormattedPlaceholder ?? view.Placeholder;

			Control.AttributedText = formattedPlaceholder.ToAttributed(view.Font, view.TextColor);

			LayoutSubviews();
		}
	}
}
