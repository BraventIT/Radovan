using System;
using Xamarin.Forms;

namespace Radovan.Plugin.Core.Controls
{
	public class ExtendedEditor : Editor
	{
		#region Placeholder Bindable Property
		public static readonly BindableProperty PlaceholderProperty =
			BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(ExtendedEditor), string.Empty, BindingMode.OneWay);

		public string Placeholder
		{
			get { return (string)GetValue(PlaceholderProperty); }
			set { SetValue(PlaceholderProperty, value); }
		}
		#endregion

		#region BorderColor Bindable Property
		public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(ExtendedEditor), Color.Default, BindingMode.OneWay);

        public Color BorderColor
		{
            get { return (Color)GetValue(BorderColorProperty); }
			set { SetValue(BorderColorProperty, value); }
		}
        #endregion

        #region BackgroundColor Bindable Property
        public static readonly BindableProperty ExtendedBackgroundColorProperty =
            BindableProperty.Create(nameof(ExtendedBackgroundColor), typeof(Color), typeof(ExtendedEditor), Color.Default, BindingMode.OneWay);

        public Color ExtendedBackgroundColor 
        {
            get { return (Color)GetValue(ExtendedBackgroundColorProperty); }
            set { SetValue(ExtendedBackgroundColorProperty, value); }
        }
        #endregion

        #region HintTextColor Bindable Property
        public static readonly BindableProperty HintTextColorProperty =
            BindableProperty.Create(nameof(HintTextColorProperty), typeof(Color), typeof(ExtendedEditor), Color.FromHex("#d9d9d9"), BindingMode.OneWay);

        public Color HintTextColor
        {
            get { return (Color)GetValue(HintTextColorProperty); }
            set { SetValue(HintTextColorProperty, value); }
        }
        #endregion

        
    }
}
