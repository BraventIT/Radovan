using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Radovan.Plugin.Core.Controls
{
    /// <summary>
	///  Extended DatePicker for Nullable Values
	///  Via: https://forums.xamarin.com/discussion/20028/datepicker-possible-to-bind-to-nullable-date-value
	///  Via: https://github.com/XLabs/Xamarin-Forms-Labs/wiki/ExtendedEntry
	/// </summary>
	public class ExtendedDatePicker : DatePicker
    {
        #region Font Bindable Property
        public Font Font
        {
            get { return (Font)GetValue(FontProperty); }
            set { SetValue(FontProperty, value); }
        }

        public static readonly BindableProperty FontProperty = BindableProperty.Create(
            propertyName: nameof(Font),
            returnType: typeof(Font),
            declaringType: typeof(ExtendedDatePicker),
            defaultValue: default(Font));
        #endregion

        #region NullableDate Bindable Property
        public DateTime? NullableDate
        {
            get { return (DateTime?)GetValue(NullableDateProperty); }
            set
            {
                SetValue(NullableDateProperty, value);
            }
        }

        /// <summary>
        /// The NullableDate property
        /// </summary>
        public static readonly BindableProperty NullableDateProperty = BindableProperty.Create(
            propertyName: nameof( NullableDate),
            returnType: typeof(DateTime?),
            declaringType: typeof(ExtendedDatePicker),
            defaultValue: null, 
            defaultBindingMode:BindingMode.TwoWay,
            propertyChanged: OnNullableDateChanged);

        private static void OnNullableDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var datePicker = bindable as ExtendedDatePicker;

            datePicker?.UpdateDate();
        }

        #endregion

        /// <summary>
        /// The XAlign property
        /// </summary>
        public static readonly BindableProperty XAlignProperty =
            BindableProperty.Create("XAlign", typeof(TextAlignment), typeof(ExtendedDatePicker),
            TextAlignment.Start);

        /// <summary>
        /// The HasBorder property
        /// </summary>
        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create("HasBorder", typeof(bool), typeof(ExtendedDatePicker), true);

        /// <summary>
        /// The Placeholder property
        /// </summary>
        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create("Placeholder", typeof(string), typeof(ExtendedDatePicker), string.Empty, BindingMode.OneWay);

        /// <summary>
        /// The PlaceholderTextColor property
        /// </summary>
        public static readonly BindableProperty PlaceholderTextColorProperty =
            BindableProperty.Create("PlaceholderTextColor", typeof(Color), typeof(ExtendedDatePicker), Color.Default);

		/// <summary>
		/// Get or sets the NullableDate
		/// </summary>

		/// <summary>
		/// The TextColor property
		/// </summary>
		public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create("TextColor", typeof(Color), typeof(ExtendedDatePicker), Color.Default);

		/// <summary>
		/// The FontSize propert
		/// </summary>
		public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create("FontSize", typeof(int), typeof(ExtendedDatePicker), 0);

        /// <summary>
		/// The margin top tablet propert
		/// </summary>
		public static readonly BindableProperty MarginTopTabletProperty =
            BindableProperty.Create("MarginTopTablet", typeof(int), typeof(ExtendedDatePicker), 0);

        /// <summary>
        /// The background transparent propert
        /// </summary>
        public static readonly BindableProperty BackgroundTransparentProperty =
            BindableProperty.Create("BackgroundTransparent", typeof(bool), typeof(ExtendedDatePicker), true);

        /// <summary>
        /// Gets or sets the X alignment of the text
        /// </summary>
        public TextAlignment XAlign
        {
            get { return (TextAlignment)GetValue(XAlignProperty); }
            set { SetValue(XAlignProperty, value); }
        }


        /// <summary>
        /// Gets or sets if the border should be shown or not
        /// </summary>
        public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }

        /// <summary>
        /// Get or sets the PlaceHolder
        /// </summary>
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        /// <summary>
        /// Sets color for placeholder text
        /// </summary>
        public Color PlaceholderTextColor
        {
            get { return (Color)GetValue(PlaceholderTextColorProperty); }
            set { SetValue(PlaceholderTextColorProperty, value); }
        }

		/// <summary>
        /// Sets color for text
        /// </summary>
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

		/// <summary>
		/// Sets FontSize for text
		/// </summary>
        public int FontSize
		{
            get { return (int)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
		}

        /// <summary>
		/// Sets magin top tablet
		/// </summary>
        public int MarginTopTablet
        {
            get { return (int)GetValue(MarginTopTabletProperty); }
            set { SetValue(MarginTopTabletProperty, value); }
        }

        /// <summary>
		/// Sets Background Transparent
		/// </summary>
        public bool BackgroundTransparent
        {
            get { return (bool)GetValue(BackgroundTransparentProperty); }
            set { SetValue(BackgroundTransparentProperty, value); }
        }

        

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateDate();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            Device.OnPlatform(() =>
            {
                if (propertyName == IsFocusedProperty.PropertyName)
                {
                    if (IsFocused)
                    {
                        if (!NullableDate.HasValue)
                        {
                            Date = (DateTime)DateProperty.DefaultValue;
                        }
                    }
                    else
                    {
                        OnPropertyChanged(DateProperty.PropertyName);
                    }
                }
            });

            if (propertyName == DateProperty.PropertyName)
            {
                NullableDate = Date;
            }

            if (propertyName == NullableDateProperty.PropertyName)
            {
                if (NullableDate.HasValue)
                {
                    Date = NullableDate.Value;
                }
            }
        }

        private void UpdateDate()
        {
            Date = (NullableDate.HasValue)?
            NullableDate.Value : (DateTime)DateProperty.DefaultValue;
        }
    }
}
