using System;
using System.Globalization;
using Xamarin.Forms;

namespace Radovan.Plugin.Core.Converters
{
    public class ImagePlatformConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value?.ToString()))
            {
                return value;
            }

            if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
            {
                var pathUwp = $"Assets/{value}.png";
                return pathUwp;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
