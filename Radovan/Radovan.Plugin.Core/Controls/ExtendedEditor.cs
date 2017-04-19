using System;
using Xamarin.Forms;

namespace Radovan.Plugin.Core.Controls
{
	public class ExtendedEditor : Editor
	{
		#region Placeholder Bindable Property
		public static readonly BindableProperty PlaceholderProperty =
			BindableProperty.Create("Placeholder", typeof(string), typeof(ExtendedEditor), string.Empty, BindingMode.OneWay);

		public string Placeholder
		{
			get { return (string)GetValue(PlaceholderProperty); }
			set { SetValue(PlaceholderProperty, value); }
		}
		#endregion
	}
}
