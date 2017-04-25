using System;
using Xamarin.Forms;
using Radovan.Plugin.Core.Extensions;
using Radovan.Plugin.Core.Helpers;

namespace Radovan.Plugin.Core.Controls
{
	public class ExtendedCheckBox : View
	{

		public static readonly BindableProperty CheckedProperty =
			BindableProperty.Create(
				nameof(Checked), typeof(bool), typeof(ExtendedCheckBox), default(bool), BindingMode.TwoWay, propertyChanged: OnCheckedPropertyChanged);

		public static readonly BindableProperty CheckedTextProperty =
			BindableProperty.Create(
				nameof(CheckedText), typeof(string), typeof(ExtendedCheckBox), string.Empty, BindingMode.TwoWay);


		public static readonly BindableProperty UncheckedTextProperty =
			BindableProperty.Create(
				nameof(UncheckedText), typeof(string), typeof(ExtendedCheckBox), string.Empty);

		public static readonly BindableProperty DefaultTextProperty =
			BindableProperty.Create(
				nameof(Text), typeof(string), typeof(ExtendedCheckBox), string.Empty);

		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create(
				nameof(TextColor), typeof(Color), typeof(ExtendedCheckBox), Color.Default);

		public static readonly BindableProperty FontSizeProperty =
			BindableProperty.Create(
				nameof(FontSize), typeof(double), typeof(ExtendedCheckBox), -1);

		public static readonly BindableProperty FontNameProperty =
			BindableProperty.Create(
				nameof(FontName), typeof(string), typeof(ExtendedCheckBox), string.Empty);


		public event EventHandler<EventArgs<bool>> CheckedChanged;

		public bool Checked
		{
			get
			{
				return this.GetValue<bool>(CheckedProperty);
			}

			set
			{
				if (this.Checked != value)
				{
					this.SetValue(CheckedProperty, value);
					this.CheckedChanged.Invoke(this, value);
				}
			}
		}

		public string CheckedText
		{
			get
			{
				return this.GetValue<string>(CheckedTextProperty);
			}

			set
			{
				this.SetValue(CheckedTextProperty, value);
			}
		}

		public string UncheckedText
		{
			get
			{
				return this.GetValue<string>(UncheckedTextProperty);
			}

			set
			{
				this.SetValue(UncheckedTextProperty, value);
			}
		}

		public string DefaultText
		{
			get
			{
				return this.GetValue<string>(DefaultTextProperty);
			}

			set
			{
				this.SetValue(DefaultTextProperty, value);
			}
		}

		public Color TextColor
		{
			get
			{
				return this.GetValue<Color>(TextColorProperty);
			}

			set
			{
				this.SetValue(TextColorProperty, value);
			}
		}

		public double FontSize
		{
			get
			{
				return (double)GetValue(FontSizeProperty);
			}
			set
			{
				SetValue(FontSizeProperty, value);
			}
		}

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

		public string Text
		{
			get
			{
				return this.Checked
					? (string.IsNullOrEmpty(this.CheckedText) ? this.DefaultText : this.CheckedText)
						: (string.IsNullOrEmpty(this.UncheckedText) ? this.DefaultText : this.UncheckedText);
			}
		}

		private static void OnCheckedPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
		{
			var checkBox = (ExtendedCheckBox)bindable;
			checkBox.Checked = (bool)newvalue;
		}
	}
}
