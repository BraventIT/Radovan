using System;
using Radovan.Plugin.Core.Enums;
using Xamarin.Forms;

namespace Radovan.Plugin.Core.Behaviors
{
	public class GestureInterest : BindableObject
	{

		public static BindableProperty NotifcationProperty = BindableProperty.Create(nameof(Notification), typeof(GestureNotification), typeof(GestureInterest), GestureNotification.None);

		public static BindableProperty GestureTypeProperty = BindableProperty.Create(nameof(GestureType), typeof(GestureType), typeof(GestureInterest), GestureType.Unknown);

		public static BindableProperty DirectionProperty = BindableProperty.Create(nameof(Direction), typeof(Directionality), typeof(GestureInterest), Directionality.None);

		public static BindableProperty GestureCommandProperty = BindableProperty.Create(nameof(GestureCommand), typeof(IGesture), typeof(GestureInterest), default(IGesture));

		public static BindableProperty GestureParameterProperty = BindableProperty.Create(nameof(GestureParameter), typeof(object), typeof(GestureInterest), default(object));

		public GestureNotification Notification
		{
			get { return (GestureNotification)GetValue(NotifcationProperty); }
			set { SetValue(NotifcationProperty, value); }
		}

		public GestureType GestureType
		{
			get { return (GestureType)GetValue(GestureTypeProperty); }
			set { SetValue(GestureTypeProperty, value); }
		}

		public Directionality Direction
		{
			get { return (Directionality)GetValue(DirectionProperty); }
			set { SetValue(DirectionProperty, value); }
		}

		public IGesture GestureCommand
		{
			get { return (IGesture)GetValue(GestureCommandProperty); }
			set { SetValue(GestureCommandProperty, value); }
		}

		public object GestureParameter
		{
			get { return GetValue(GestureParameterProperty); }
			set { SetValue(GestureParameterProperty, value); }
		}
	}
}
