using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Radovan.Plugin.iOS.Extensions
{
	public static class StringExtensions
	{

		public static nfloat StringHeight(this string text, UIFont font, nfloat width)
		{
			return text.StringRect(font, width).Height;
		}

		public static NSString ToNativeString(this string text)
		{
			return new NSString(text);
		}

		public static CGRect StringRect(this string text, UIFont font, nfloat width)
		{
			return text.ToNativeString().GetBoundingRect(
				new CGSize(width, nfloat.MaxValue),
				NSStringDrawingOptions.OneShot,//.UsesFontLeading | NSStringDrawingOptions.UsesLineFragmentOrigin,
				new UIStringAttributes { Font = font },
				null);
		}

		public static CGSize StringSize(this string text, UIFont font)
		{
			return text.ToNativeString().GetSizeUsingAttributes(new UIStringAttributes { Font = font });
		}
	}
}
