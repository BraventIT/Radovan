using System;
using System.Linq.Expressions;
using Radovan.Plugin.Core.Enums;
using Xamarin.Forms;

namespace Radovan.Plugin.Core.Controls
{
	public class ImageButton : Button
	{
		public static readonly BindableProperty SourceProperty = BindableProperty.Create(
			(Expression<Func<ImageButton, ImageSource>>)(w => w.Source),
			null,
			BindingMode.OneWay,
			null,
			(bindable, oldvalue, newvalue) => ((VisualElement)bindable).ToString());

		public static readonly BindableProperty DisabledSourceProperty = BindableProperty.Create(
			(Expression<Func<ImageButton, ImageSource>>)(w => w.DisabledSource),
			null,
			BindingMode.OneWay,
			null,
			(bindable, oldvalue, newvalue) => ((VisualElement)bindable).ToString());

		public static readonly BindableProperty ImageWidthRequestProperty =
			BindableProperty.Create(
					nameof(ImageWidthRequest), typeof(int), typeof(ImageButton), default(int));

		public static readonly BindableProperty ImageHeightRequestProperty =
			BindableProperty.Create(
					nameof(ImageHeightRequest), typeof(int), typeof(ImageButton), default(int));

		public static readonly BindableProperty OrientationProperty =
			BindableProperty.Create(
					nameof(Orientation), typeof(ImageOrientation), typeof(ImageButton), ImageOrientation.ImageToLeft);

		public static readonly BindableProperty ImageTintColorProperty =
			BindableProperty.Create(
					nameof(ImageTintColor), typeof(Color), typeof(ImageButton), Color.Transparent);

		public static readonly BindableProperty DisabledImageTintColorProperty =
			BindableProperty.Create(
					nameof(DisabledImageTintColor), typeof(Color), typeof(ImageButton), Color.Transparent);

		[TypeConverter(typeof(ImageSourceConverter))]
		public ImageSource Source
		{
			get { return (ImageSource)GetValue(SourceProperty); }
			set { SetValue(SourceProperty, value); }
		}

		[TypeConverter(typeof(ImageSourceConverter))]
		public ImageSource DisabledSource
		{
			get { return (ImageSource)GetValue(DisabledSourceProperty); }
			set { SetValue(DisabledSourceProperty, value); }
		}

		public ImageOrientation Orientation
		{
			get { return (ImageOrientation)GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}

		public int ImageHeightRequest
		{
			get { return (int)GetValue(ImageHeightRequestProperty); }
			set { SetValue(ImageHeightRequestProperty, value); }
		}

		public int ImageWidthRequest
		{
			get { return (int)GetValue(ImageWidthRequestProperty); }
			set { SetValue(ImageWidthRequestProperty, value); }
		}

		public Color ImageTintColor
		{
			get { return (Color)GetValue(ImageTintColorProperty); }
			set { SetValue(ImageTintColorProperty, value); }
		}

		public Color DisabledImageTintColor
		{
			get { return (Color)GetValue(DisabledImageTintColorProperty); }
			set { SetValue(DisabledImageTintColorProperty, value); }
		}
	}
}
