using System;
using System.Collections.Generic;
using System.Linq;
using Radovan.Plugin.Core.Behaviors;
using Radovan.Plugin.Core.Enums;
using Xamarin.Forms;

namespace Radovan.Plugin.Core.Controls
{
	public class GesturesContentView : ContentView
	{

		public static BindableProperty AccuracyProperty = BindableProperty.Create(nameof(Accuracy), typeof(float), typeof(GesturesContentView), 5.0f, BindingMode.OneWay, (bo, val) => (float)val >= 5 && (float)val <= 25);

		public static BindableProperty MinimumSwipeLengthProperty = BindableProperty.Create(nameof(MinimumSwipeLength), typeof(float), typeof(GesturesContentView), 25, BindingMode.OneWay, (bo, val) => (float)val >= 10);

		public static BindableProperty ExcludeChildrenProperty = BindableProperty.Create(nameof(ExcludeChildren), typeof(bool), typeof(GesturesContentView), true);

		public bool ExcludeChildren
		{
			get { return (bool)GetValue(ExcludeChildrenProperty); }
			set { SetValue(ExcludeChildrenProperty, value); }
		}

		public float MinimumSwipeLength
		{
			get { return (float)GetValue(MinimumSwipeLengthProperty); }
			set { SetValue(MinimumSwipeLengthProperty, value); }
		}

		public float Accuracy
		{
			get { return (float)GetValue(AccuracyProperty); }
			set { SetValue(AccuracyProperty, value); }
		}

		public event EventHandler<GestureResult> GestureRecognized;

		private readonly List<ViewInterest> _viewInterests = new List<ViewInterest>();

		internal void RegisterInterests(View view, IEnumerable<GestureInterest> interestedin)
		{
			var vi = _viewInterests.FirstOrDefault(x => x.View == view);
			if (vi == null)
			{
				vi = new ViewInterest { View = view };
				_viewInterests.Add(vi);
			}
			vi.Interests = new List<GestureInterest>(interestedin.ToList());
			BindInterests(vi);
		}

		internal void RemoveInterestsFor(View view)
		{
			_viewInterests.RemoveAll(x => x.View == view);
		}

		private void BindInterests(ViewInterest vi)
		{
			var bc = FindBindingContext(vi.View);
			foreach (var interest in vi.Interests)
			{
				if (interest.BindingContext == null)
					interest.BindingContext = bc;
			}
		}

		private object FindBindingContext(View view)
		{
			var bc = view.BindingContext;
			if (bc != null) return bc;
			var parent = view.Parent;
			while (parent != null)
			{
				bc = parent.BindingContext;
				if (bc != null) break;
				parent = parent.Parent;
			}
			return bc;
		}

		public bool ProcessGesture(GestureResult gesture)
		{
			//Check the view stack first
			if (ExcludeChildren && gesture.ViewStack != null && gesture.ViewStack.Count != 0 && _viewInterests.All(x => x.View != gesture.ViewStack[0])) return false;//The innermost (source) is not an actual interested view
			var interestedview = InterestedView(gesture.Origin);
			if (interestedview == null) return false;
			gesture.StartView = interestedview.View;

			//Check for perfect matches first
			var interest = interestedview.Interests.Where(x => x.GestureType == gesture.GestureType &&
														   (
															   (x.Direction & Directionality.HorizontalMask) == (gesture.Direction & Directionality.HorizontalMask) &&
															   (x.Direction & Directionality.VerticalMask) == (gesture.Direction & Directionality.VerticalMask))
															).ToList();

			if (!interest.Any())
			{
				//Check for match on the dominant axis
				var horizontaldirection = gesture.HorizontalDistance < gesture.VerticalDistance
					? Directionality.None : gesture.Direction & Directionality.HorizontalMask;

				var verticaldirection = gesture.HorizontalDistance < gesture.VerticalDistance
											  ? gesture.Direction & Directionality.VerticalMask
											  : Directionality.None;

				//Swap in the new direction so the user knows what the final match was
				gesture.Direction = horizontaldirection | verticaldirection;

				interest = interestedview.Interests.Where(x => x.GestureType == gesture.GestureType &&
																			   (x.Direction & Directionality.HorizontalMask) == horizontaldirection &&
																			   (x.Direction & Directionality.VerticalMask) == verticaldirection).ToList();
			}
			//Winnow out the swipe gestures to match on either a perfect direction match (ie Up,Left) or based on the dominant axis


			//Is there one or more interest int this gesture?
			if (!interest.Any()) return false;
			var final = interest.First();
			//Finish setting up our gestureresult
			gesture.Origin = new Point(Math.Max(gesture.Origin.X - interestedview.View.X, 0), Math.Max(gesture.Origin.Y - interestedview.View.Y, 0));
			SatisfyInterest(final, gesture);
			return true;
		}

		private ViewInterest InterestedView(Point point)
		{
			//Smallest view that contains the origin point wins
			//In most cases smallest will be the innermost
			//TODO:Check to see if RaiseView and LowerView have an effect on this
			var originview = _viewInterests.Where(v => v.View.Bounds.Contains(point)).OrderBy(v => v.View.Bounds.Width * v.View.Bounds.Height).FirstOrDefault();
			if (originview == null)
			{
				//No result Check for interescection based on Accuracy
				var range = Accuracy;
				var inflaterect = new Rectangle(point.X - range, point.Y - range, point.X + range, point.Y + range);
				var candidates = _viewInterests.Where(v => v.View.Bounds.IntersectsWith(inflaterect)).ToList();
				if (candidates.Any())
				{
					originview = candidates.Count() == 1 ? candidates.First() : candidates.OrderBy(v => DistanceToClosestEdge(v.View.Bounds, point)).First();
				}
			}
			//check the originview for noninterested children that contain the point
			//var child=originview.View.
			return originview;
		}

		private double DistanceToClosestEdge(Rectangle r, Point pt)
		{
			//Distance from the top edge of the rectangle
			// ReSharper disable InconsistentNaming
			var distAB = DistanceToEdge(pt, new Point(r.Left, r.Top), new Point(r.Left + r.Width, r.Top));
			//Distance from the left edge of the rectangle
			var distAC = DistanceToEdge(pt, new Point(r.Left, r.Top), new Point(r.Left, r.Top + r.Height));
			//Distance from the bottom edge of the rectangle
			var distCD = DistanceToEdge(pt, new Point(r.Left, r.Top + r.Height), new Point(r.Left + r.Width, r.Top + r.Height));
			//Distance from the right edge of the rectable
			var distBD = DistanceToEdge(pt, new Point(r.Left + r.Width, r.Top), new Point(r.Left + r.Width, r.Top + r.Height));
			// ReSharper restore InconsistentNaming

			return Math.Min(distAB, Math.Min(distAC, Math.Min(distCD, distBD)));
		}

		private double DistanceToEdge(Point originPoint, Point vertex1Point, Point vertex2Point)
		{
			// normalize points
			var cn = new Point(vertex2Point.X - originPoint.X, vertex2Point.Y - originPoint.Y);
			var bn = new Point(vertex1Point.X - originPoint.X, vertex1Point.Y - originPoint.Y);

			var angle = Math.Atan2(bn.Y, bn.X) - Math.Atan2(cn.Y, cn.X);
			var abLength = Math.Sqrt(bn.X * bn.X + bn.Y * bn.Y);

			return Math.Sin(angle) * abLength;
		}


		private void SatisfyInterest(GestureInterest gi, GestureResult args)
		{
			var commandparam = gi.GestureParameter ?? args.StartView.BindingContext ?? BindingContext;
			if (gi.GestureCommand != null && gi.GestureCommand.CanExecuteGesture(args, gi.GestureParameter))
				gi.GestureCommand.ExecuteGesture(args, commandparam);
			var handler = GestureRecognized;
			if (handler != null)
			{
				handler(args.StartView, args);
			}

		}

		private class ViewInterest
		{
			public View View { get; set; }
			public List<GestureInterest> Interests { get; set; }
		}
	}
}
