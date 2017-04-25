using System;
using System.Collections.Generic;
using System.Linq;
using Radovan.Plugin.Android.Renderers;
using Radovan.Plugin.Core.Controls;
using Android.Graphics;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Point = Xamarin.Forms.Point;
using View = Xamarin.Forms.View;
using Radovan.Plugin.Core.Behaviors;
using Radovan.Plugin.Core.Enums;
using AndViews = Android.Views;

[assembly: ExportRenderer(typeof(GesturesContentView), typeof(GesturesContentViewRenderer))]
namespace Radovan.Plugin.Android.Renderers
{
	public class GesturesContentViewRenderer : ViewRenderer<GesturesContentView, AndViews.View>, GestureDetector.IOnGestureListener, GestureDetector.IOnDoubleTapListener
	{

		private readonly GestureDetector _detector;

		public GesturesContentViewRenderer()
		{
			_detector = new GestureDetector(this);
		}

		protected override void OnElementChanged(ElementChangedEventArgs<GesturesContentView> e)
		{

			base.OnElementChanged(e);
			if (e.NewElement == null)
			{
				GenericMotion -= HandleGenericMotion;
				Touch -= HandleTouch;
			}
			if (e.OldElement != null) return;
			Element.GestureRecognizers.Clear();
			GenericMotion += HandleGenericMotion;
			Touch += HandleTouch;
		}

		private void HandleTouch(object sender, TouchEventArgs e)
		{
			_detector.OnTouchEvent(e.Event);

			// Notify app for all UP events.  This can be used to know that a Pinch or Move has ended. Otherwise it can be ignored.
			if (e.Event.Action == MotionEventActions.Up
				|| e.Event.Action == MotionEventActions.Cancel
				|| e.Event.Action == MotionEventActions.PointerUp
				|| e.Event.Action == MotionEventActions.Pointer1Up
				|| e.Event.Action == MotionEventActions.Pointer2Up
				|| e.Event.Action == MotionEventActions.Pointer3Up)
			{
				// we don't return here since up events are needed for general gesture processing
				Element.ProcessGesture(new GestureResult
				{
					ViewStack = null,
					GestureType = GestureType.Up,
					Direction = Directionality.None,
				});
			}
		}

		private void HandleGenericMotion(object sender, GenericMotionEventArgs e)
		{
			_detector.OnTouchEvent(e.Event);
		}

		private List<View> ViewsContaing(Point pt)
		{
			var ret = new List<View>();
			return ViewsContainingImpl(pt, ret, ViewGroup).OrderBy(x => x.Bounds.Width * x.Bounds.Height).ToList();
		}

		private IEnumerable<View> ViewsContainingImpl(Point pt, List<View> views, ViewGroup root)
		{
			for (var i = 0; i < root.ChildCount; i++)
			{
				var child = root.GetChildAt(i);
				var bounds = new Rect();
				child.GetHitRect(bounds);
				var ver = child as IVisualElementRenderer;
				if (ver == null || !(ver.Element is View) || !bounds.Contains(Convert.ToInt32(pt.X), Convert.ToInt32(pt.Y)))
					continue;
				views.Add(ver.Element as View);
				//check to see if this containing child has any other containing children
				ViewsContainingImpl(pt, views, ver.ViewGroup);
			}
			return views;
		}
		#region Gesture Events

		public bool OnDown(MotionEvent e)
		{
			Element.ProcessGesture(
				new GestureResult
				{
					ViewStack = null,
					GestureType = GestureType.Down,
					Direction = Directionality.None,
					Origin = new Point(ConvertPixelsToDp(e.GetX(0)), ConvertPixelsToDp(e.GetY(0))),
				});

			return true;
		}

		public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
		{

			var e1X = ConvertPixelsToDp(e1.GetX());
			var e2X = ConvertPixelsToDp(e2.GetX());
			var e1Y = ConvertPixelsToDp(e1.GetY());
			var e2Y = ConvertPixelsToDp(e2.GetY());

			var distance = Math.Abs(Math.Sqrt(Math.Pow(e1X - e2X, 2) + Math.Pow(e1Y - e2Y, 2)));
			if (distance < Element.MinimumSwipeLength) return true;

			Element.ProcessGesture(new GestureResult
			{
				GestureType = GestureType.Swipe,
				Direction = (Math.Abs(e1X - e2X) < 3 ? Directionality.None : e1X < e2X ? Directionality.Right : Directionality.Left)
												| (Math.Abs(e1Y - e2Y) < 3 ? Directionality.None : e1Y < e2Y ? Directionality.Down : Directionality.Up),
				Origin = new Point(e1X, e1Y),
				VerticalDistance = Math.Abs(e1Y - e2Y),
				HorizontalDistance = Math.Abs(e1X - e2X),
				Length = distance,
				ViewStack = null
			});
			return false;
		}

		private int ConvertPixelsToDp(float pixelValue)
		{
			var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
			return dp;
		}

		public void OnLongPress(MotionEvent e)
		{
			Element.ProcessGesture(new GestureResult
			{
				ViewStack = null,
				GestureType = GestureType.LongPress,
				Direction = Directionality.None,
				Origin = new Point(ConvertPixelsToDp(e.GetX()), ConvertPixelsToDp(e.GetY()))
			});
		}

		public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
		{
			// we need to check if e2 has 2 finger touch and if yes, do callback.  Then in the app we can disable scrolling and process these events....
			if (e2.Action == MotionEventActions.Move)
			{
				if (e2.PointerCount == 2)
				{
					return !Element.ProcessGesture(
						new GestureResult
						{
							ViewStack = null,
							GestureType = GestureType.Pinch,
							Direction = Directionality.None,
							Origin = new Point(ConvertPixelsToDp(e2.GetX(0)), ConvertPixelsToDp(e2.GetY(0))),
							Origin2 = new Point(ConvertPixelsToDp(e2.GetX(1)), ConvertPixelsToDp(e2.GetY(1))),
						});
				}
				else if (e2.PointerCount == 1)
				{
					return !Element.ProcessGesture(
						new GestureResult
						{
							ViewStack = null,
							GestureType = GestureType.Move,
							Direction = Directionality.None,
							Origin = new Point(ConvertPixelsToDp(e2.GetX(0)), ConvertPixelsToDp(e2.GetY(0))),
						});
				}
			}

			return true;
		}

		public void OnShowPress(MotionEvent e) { }

		public bool OnSingleTapUp(MotionEvent e) { return true; }

		public bool OnDoubleTap(MotionEvent e)
		{
			return !Element.ProcessGesture(new GestureResult
			{
				ViewStack = null,
				GestureType = GestureType.DoubleTap,
				Direction = Directionality.None,
				Origin = new Point(ConvertPixelsToDp(e.GetX()), ConvertPixelsToDp(e.GetY()))
			});
		}

		public bool OnDoubleTapEvent(MotionEvent e) { return true; }

		public bool OnSingleTapConfirmed(MotionEvent e)
		{
			return !Element.ProcessGesture(new GestureResult
			{
				ViewStack = null,
				GestureType = GestureType.SingleTap,
				Direction = Directionality.None,
				Origin = new Point(ConvertPixelsToDp(e.GetX()), ConvertPixelsToDp(e.GetY()))
			});
		}
		#endregion

	}

}
