﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace Radovan.Plugin.Core.Converters
{
    public class BooleanToColorConverter : IValueConverter
    {

        public BooleanToColorConverter() : this(Color.Black, Color.Transparent)
        {

        }

        public BooleanToColorConverter(Color trueColor, Color falseColor)
        {
            ColorToTrue = trueColor;
            ColorToFalse = falseColor;
        }

        public Color ColorToTrue { get; set; }

        public Color ColorToFalse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                bool negateValue = false;
                if (parameter != null && parameter is string)
                    Boolean.TryParse(parameter as string, out negateValue);

                bool b = (bool)value;
                if (negateValue ^ b)
                    return ColorToTrue;
                else
                    return ColorToFalse;
            }
            catch
            {
                return ColorToFalse;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

