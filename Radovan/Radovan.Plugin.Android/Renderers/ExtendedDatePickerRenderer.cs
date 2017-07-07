using Android.Views;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Radovan.Plugin.Core.Controls;
using Radovan.Plugin.Android.Renderers;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(ExtendedDatePicker), typeof(ExtendedDatePickerRenderer))]
namespace Radovan.Plugin.Android.Renderers
{
    /// <summary>
    ///  Extended DatePicker Renderer for Nullable Values
    ///  Via: https://forums.xamarin.com/discussion/20028/datepicker-possible-to-bind-to-nullable-date-value
    ///  Via: https://github.com/XLabs/Xamarin-Forms-Labs/wiki/ExtendedEntry
    /// </summary>
    public class ExtendedDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            var view = Element as ExtendedDatePicker;

            if (view != null)
            {
                SetFont(view);
                SetTextAlignment(view);
                // SetBorder(view);
                SetNullableText(view);
                SetPlaceholder(view);
                SetPlaceholderTextColor(view);

				//Control?.SetBackgroundColor(Android.Graphics.Color.Transparent);
                this.Control.SetTextColor(view.TextColor.ToAndroid());
                this.Control.TextSize = view.FontSize;

			}
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (ExtendedDatePicker)Element;

            if (e.PropertyName == ExtendedDatePicker.FontProperty.PropertyName)
                SetFont(view);
            else if (e.PropertyName == ExtendedDatePicker.XAlignProperty.PropertyName)
                SetTextAlignment(view);
            // else if (e.PropertyName == ExtendedDatePicker.HasBorderProperty.PropertyName)
            //  SetBorder(view);
            else if (e.PropertyName == ExtendedDatePicker.NullableDateProperty.PropertyName)
                SetNullableText(view);
            else if (e.PropertyName == ExtendedDatePicker.PlaceholderProperty.PropertyName)
                SetPlaceholder(view);
            else if (e.PropertyName == ExtendedDatePicker.PlaceholderTextColorProperty.PropertyName)
                SetPlaceholderTextColor(view);

        }

        /// <summary>
        /// Sets the text alignment.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetTextAlignment(ExtendedDatePicker view)
        {
            switch (view.XAlign)
            {
                case Xamarin.Forms.TextAlignment.Center:
                    Control.Gravity = GravityFlags.CenterHorizontal;
                    break;
                case Xamarin.Forms.TextAlignment.End:
                    Control.Gravity = GravityFlags.End;
                    break;
                case Xamarin.Forms.TextAlignment.Start:
                    Control.Gravity = GravityFlags.Start;
                    break;
            }
        }

        /// <summary>
        /// Sets the font.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetFont(ExtendedDatePicker view)
        {
            if (view.Font != Font.Default)
            {
                Control.TextSize = view.Font.ToScaledPixel();
                //Control.Typeface = view.Font.ToExtendedTypeface(Context);
            }
        }

        /// <summary>
        /// Set text based on nullable value
        /// </summary>
        /// <param name="view"></param>
        private void SetNullableText(ExtendedDatePicker view)
        {
            if (view.NullableDate == null)
                Control.Text = string.Empty;
        }

        /// <summary>
        /// Set the placeholder
        /// </summary>
        /// <param name="view"></param>
        private void SetPlaceholder(ExtendedDatePicker view)
        {
            Control.Hint = view.Placeholder;
        }

        /// <summary>
        /// Sets the color of the placeholder text.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetPlaceholderTextColor(ExtendedDatePicker view)
        {
            if (view.PlaceholderTextColor != Color.Default)
            {
                Control.SetHintTextColor(view.PlaceholderTextColor.ToAndroid());
            }
        }
    }
}