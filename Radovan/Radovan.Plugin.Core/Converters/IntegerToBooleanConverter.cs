using System;
using System.Globalization;
using Xamarin.Forms;

namespace Radovan.Plugin.Core.Converters
{
    public class IntegerToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int order;
            int.TryParse(value.ToString(), out order);
            bool negateValue = false;

            if (parameter != null && parameter is string)
                bool.TryParse(parameter as string, out negateValue);
            if (!negateValue)
            {
                if (order == 0)
                    return true;
                else
                    return false;
            }
            else
            {
                if (order == 0)
                    return false;
                else
                    return true;
            }



        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
