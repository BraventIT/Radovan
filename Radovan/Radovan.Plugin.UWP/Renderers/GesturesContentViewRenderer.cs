using Radovan.Plugin.Core.Behaviors;
using Radovan.Plugin.Core.Controls;
using Radovan.Plugin.Core.Enums;
using Radovan.Plugin.UWP.Renderers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(GesturesContentView), typeof(GesturesContentViewRenderer))]
namespace Radovan.Plugin.UWP.Renderers
{
    public class GesturesContentViewRenderer : ViewRenderer<GesturesContentView, FrameworkElement>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<GesturesContentView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
            {
                Tapped -= GesturesContentViewRenderer_Tapped;
                Holding -= GesturesContentViewRenderer_Holding;
            }
            if (e.OldElement != null) return;
            Tapped += GesturesContentViewRenderer_Tapped;
            Holding += GesturesContentViewRenderer_Holding;
        }

        private void GesturesContentViewRenderer_Holding(object sender, HoldingRoutedEventArgs e)
        {
            Element.ProcessGesture(new GestureResult
            {
                ViewStack = null,
                GestureType = GestureType.LongPress,
                Direction = Directionality.None,
            });
        }

        private void GesturesContentViewRenderer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Element.ProcessGesture(new GestureResult
            {
                ViewStack = null,
                GestureType = GestureType.SingleTap,
                Direction = Directionality.None,
            });
        }
    }
}
