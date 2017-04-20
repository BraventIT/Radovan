using System;
using Xamarin.Forms;

namespace Radovan.Plugin.Core.Controls
{
	public class ExtendedLabel : Label
	{

		public static readonly BindableProperty FontNameProperty =
					BindableProperty.Create(nameof(FontName), typeof(string), typeof(ExtendedLabel), default(string));

		public string FontName
		{
			get
			{
				return (string)GetValue(FontNameProperty);
			}
			set
			{
				SetValue(FontNameProperty, value);
			}
		}

		public static readonly BindableProperty FriendlyFontNameProperty =
			BindableProperty.Create(nameof(FriendlyFontName), typeof(string), typeof(ExtendedLabel), default(string));

		public string FriendlyFontName
		{
			get
			{
				return (string)GetValue(FriendlyFontNameProperty);
			}
			set
			{
				SetValue(FriendlyFontNameProperty, value);
			}
		}

		public static readonly BindableProperty IsUnderlineProperty =
					BindableProperty.Create(nameof(IsUnderline), typeof(bool), typeof(ExtendedLabel), false);

		public bool IsUnderline
		{
			get
			{
				return (bool)GetValue(IsUnderlineProperty);
			}
			set
			{
				SetValue(IsUnderlineProperty, value);
			}
		}

		public static readonly BindableProperty IsStrikeThroughProperty =
					BindableProperty.Create(nameof(IsStrikeThrough), typeof(bool), typeof(ExtendedLabel), false);


		public bool IsStrikeThrough
		{
			get
			{
				return (bool)GetValue(IsStrikeThroughProperty);
			}
			set
			{
				SetValue(IsStrikeThroughProperty, value);
			}
		}

		public static readonly BindableProperty IsDropShadowProperty =
					BindableProperty.Create(nameof(IsDropShadow), typeof(bool), typeof(ExtendedLabel), false);

		public bool IsDropShadow
		{
			get
			{
				return (bool)GetValue(IsDropShadowProperty);
			}
			set
			{
				SetValue(IsDropShadowProperty, value);
			}
		}

		public static readonly BindableProperty DropShadowColorProperty =
					BindableProperty.Create(nameof(DropShadowColor), typeof(Color), typeof(ExtendedLabel), default(Color));

		public Color DropShadowColor
		{
			get
			{
				return (Color)GetValue(DropShadowColorProperty);
			}
			set
			{
				SetValue(DropShadowColorProperty, value);
			}
		}

		public static readonly BindableProperty PlaceholderProperty =
					BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(ExtendedLabel), default(string));

		public string Placeholder
		{
			get { return (string)GetValue(PlaceholderProperty); }
			set
			{
				SetValue(FormattedPlaceholderProperty, null);
				SetValue(PlaceholderProperty, value);
			}
		}

		public static readonly BindableProperty FormattedPlaceholderProperty =
		BindableProperty.Create(nameof(FormattedPlaceholder), typeof(FormattedString), typeof(ExtendedLabel), default(FormattedString));

		public FormattedString FormattedPlaceholder
		{
			get { return (FormattedString)GetValue(FormattedPlaceholderProperty); }
			set
			{
				SetValue(PlaceholderProperty, null);
				SetValue(FormattedPlaceholderProperty, value);
			}
		}

		public static readonly BindableProperty LinesProperty =
		BindableProperty.Create(nameof(Lines), typeof(int), typeof(ExtendedLabel), -1);
		public int Lines
		{
			get { return (int)GetValue(LinesProperty); }
			set { SetValue(LinesProperty, value); }
		}
	}
}
