using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Radovan.Plugin.iOS.Renderers
{
	[Register("ExtendedCheckBoxView")]
	public class ExtendedCheckBoxView : UIButton
	{

		public ExtendedCheckBoxView()
		{
			Initialize();
		}

		public ExtendedCheckBoxView(CGRect bounds) : base(bounds)
		{
			Initialize();
		}

		public string CheckedTitle
		{
			set
			{
				SetTitle(value, UIControlState.Selected);
			}
		}

		public string UncheckedTitle
		{
			set
			{
				SetTitle(value, UIControlState.Normal);
			}
		}

		public bool Checked
		{
			set { Selected = value; }
			get { return Selected; }
		}

		void Initialize()
		{
			AdjustEdgeInsets();
			ApplyStyle();

			TouchUpInside += (sender, args) => Selected = !Selected;
			// set default color, because type is not UIButtonType.System 
			SetTitleColor(UIColor.DarkTextColor, UIControlState.Normal);
			SetTitleColor(UIColor.DarkTextColor, UIControlState.Selected);
		}

		void AdjustEdgeInsets()
		{
			const float Inset = 8f;

			HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
			ImageEdgeInsets = new UIEdgeInsets(0f, Inset, 0f, 0f);
			TitleEdgeInsets = new UIEdgeInsets(0f, Inset * 2, 0f, 0f);
		}

		void ApplyStyle()
		{
			SetImage(UIImage.FromBundle("Images/Checkbox/checked_checkbox.png"), UIControlState.Selected);
			SetImage(UIImage.FromBundle("Images/Checkbox/unchecked_checkbox.png"), UIControlState.Normal);
		}
	}
}
