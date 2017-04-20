using System;
using System.ComponentModel;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.Text;
using Android.Text.Method;
using Android.Util;
using Android.Views;
using Radovan.Plugin.Android.Extensions;
using Radovan.Plugin.Android.Renderers;
using Radovan.Plugin.Core.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace Radovan.Plugin.Android.Renderers
{
	public class ExtendedEntryRenderer : EntryRenderer
	{
		private const int MinDistance = 10;

		private float downX, downY, upX, upY;

		private Drawable originalBackground;

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			var view = (ExtendedEntry)Element;

			if (Control != null && e.NewElement != null && e.NewElement.IsPassword)
			{
				Control.SetTypeface(Typeface.Default, TypefaceStyle.Normal);
				Control.TransformationMethod = new PasswordTransformationMethod();
			}

			if (originalBackground == null)
				originalBackground = Control.Background;

			SetFont(view);
			SetTextAlignment(view);
			//SetBorder(view);
			SetPlaceholderTextColor(view);
			SetMaxLength(view);

			if (e.NewElement == null)
			{
				this.Touch -= HandleTouch;
			}

			if (e.OldElement == null)
			{
				this.Touch += HandleTouch;
			}
		}

		void HandleTouch(object sender, TouchEventArgs e)
		{
			var element = (ExtendedEntry)this.Element;
			switch (e.Event.Action)
			{
				case MotionEventActions.Down:
					this.downX = e.Event.GetX();
					this.downY = e.Event.GetY();
					return;
				case MotionEventActions.Up:
				case MotionEventActions.Cancel:
				case MotionEventActions.Move:
					this.upX = e.Event.GetX();
					this.upY = e.Event.GetY();

					float deltaX = this.downX - this.upX;
					float deltaY = this.downY - this.upY;

					// swipe horizontal?
					if (Math.Abs(deltaX) > Math.Abs(deltaY))
					{
						if (Math.Abs(deltaX) > MinDistance)
						{
							if (deltaX < 0)
							{
								element.OnRightSwipe(this, EventArgs.Empty);
								return;
							}

							if (deltaX > 0)
							{
								element.OnLeftSwipe(this, EventArgs.Empty);
								return;
							}
						}
						else
						{
							Log.Info("ExtendedEntry", "Horizontal Swipe was only " + Math.Abs(deltaX) + " long, need at least " + MinDistance);
							return; // We don't consume the event
						}
					}
					return;
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var view = (ExtendedEntry)Element;

			if (e.PropertyName == ExtendedEntry.FontProperty.PropertyName)
			{
				SetFont(view);
			}
			else if (e.PropertyName == ExtendedEntry.XAlignProperty.PropertyName)
			{
				SetTextAlignment(view);
			}
			else if (e.PropertyName == ExtendedEntry.HasBorderProperty.PropertyName)
			{
				//return;   
			}
			else if (e.PropertyName == ExtendedEntry.PlaceholderTextColorProperty.PropertyName)
			{
				SetPlaceholderTextColor(view);
			}
			else
			{
				base.OnElementPropertyChanged(sender, e);
				if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
				{
					this.Control.SetBackgroundColor(view.BackgroundColor.ToAndroid());
				}
			}
		}

		private void SetBorder(ExtendedEntry view)
		{
			if (view.HasBorder == false)
			{
				var shape = new ShapeDrawable(new RectShape());
				shape.Paint.Alpha = 0;
				shape.Paint.SetStyle(Paint.Style.Stroke);
				Control.SetBackgroundDrawable(shape);
			}
			else
			{
				Control.SetBackground(originalBackground);
			}
		}

		private void SetTextAlignment(ExtendedEntry view)
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

		private void SetFont(ExtendedEntry view)
		{
			if (view.Font != Font.Default)
			{
				Control.TextSize = view.Font.ToScaledPixel();
				Control.Typeface = view.Font.ToExtendedTypeface(Context);
			}
		}

		private void SetPlaceholderTextColor(ExtendedEntry view)
		{
			if (view.PlaceholderTextColor != Xamarin.Forms.Color.Default)
			{
				Control.SetHintTextColor(view.PlaceholderTextColor.ToAndroid());
			}
		}

		private void SetMaxLength(ExtendedEntry view)
		{
			Control.SetFilters(new IInputFilter[] { new InputFilterLengthFilter(view.MaxLength) });
		}
	}
}
