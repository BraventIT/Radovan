using System;
using System.Globalization;
using Xamarin.Forms;

namespace Radovan.Plugin.Core.Converters
{
	public class CapitalLettersConverter : IValueConverter
	{

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string resource = string.Empty;
			if (value != null)
			{
				resource = value.ToString();
				resource = resource.ToUpper();
			}
			return resource;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

