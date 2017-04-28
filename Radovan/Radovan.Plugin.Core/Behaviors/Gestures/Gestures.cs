using Radovan.Plugin.Core.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace Radovan.Plugin.Core.Behaviors
{
    /// <summary>
    /// Provides Gesture attached properties for the GesturesContentView
    /// This class has no involvement beyond setting up the interests
    /// It is here simply to make the users xaml a bit 
    /// more readable
    /// </summary>
    public class Gestures : BindableObject
    {
        /// <summary>
        /// Definition for the attachable Interests Property
        /// </summary>
        public static BindableProperty InterestsProperty = BindableProperty.CreateAttached(nameof(InterestsProperty), typeof(GestureCollection), typeof(Gestures),
                                                            null,
                                                            BindingMode.OneWay,
                                                            null,
                                                            InterestsChanged);

        /// <summary>
        /// ctor guarenttess that <see cref="Interests"/> is not null
        /// </summary>
        public Gestures()
        {
            Interests = new GestureCollection();
        }

        /// <summary>
        /// The set of interests for this view
        /// </summary>
        public GestureCollection Interests
        {
            get { return (GestureCollection)GetValue(InterestsProperty); }
            set { SetValue(InterestsProperty, value); }
        }

        private static void InterestsChanged(BindableObject bo, object oldvalue, object newvalue)
        {
            var view = bo as View;
            //if (view == null)
            //	throw new InvalidBindableException(bo, typeof(View));

            var gcv = FindContentViewParent(view, false);
            if (gcv == null)
            {
                PendingInterestParameters.Add(new PendingInterestParams { View = view, Interests = (GestureCollection)newvalue });
                view.PropertyChanged += ViewPropertyChanged;

            }
            else
                gcv.RegisterInterests(view, (GestureCollection)newvalue);
        }

        private static void ViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Unfortunately the Parent property doesn't signal a change
            //However when the renderer is set it should have it's parent
            if (e.PropertyName == "Renderer")
            {
                var view = sender as View;
                if (view == null) return;
                var gcv = FindContentViewParent(view);
                var pending = PendingInterestParameters.Where(x => x.View == view).ToList();
                foreach (var pendingparam in pending)
                {
                    gcv.RegisterInterests(view, pendingparam.Interests);
                    PendingInterestParameters.Remove(pendingparam);
                }
                view.PropertyChanged -= ViewPropertyChanged;
            }
        }

        /// <summary>
        /// Utility function to find the first containing <see cref="GesturesContentView"/>
        /// </summary>
        /// <param name="view">View to find the parent from.</param>
        /// <param name="throwException">True to throw an excpetion if the parent is not found</param>
        /// <returns></returns>
        private static GesturesContentView FindContentViewParent(View view, bool throwException = true)
        {
            var history = new List<string>();
            var viewParent = view as GesturesContentView;
            if (viewParent != null) return viewParent;

            history.Add(view.GetType().Name);
            var parent = view.Parent;
            while (parent != null && !(parent is GesturesContentView))
            {
                history.Add(parent.GetType().Name);
                parent = parent.Parent;
            }

            if (parent == null && throwException)
            {
                //throw new InvalidNestingException(typeof(Gestures), typeof(GesturesContentView), history);
            }

            return (GesturesContentView)parent;
        }

        private static readonly List<PendingInterestParams> PendingInterestParameters = new List<PendingInterestParams>();

        private class PendingInterestParams
        {
            public View View { get; set; }
            public GestureCollection Interests { get; set; }
        }
    }
}
