using Radovan.Plugin.Core.Controls;
using Radovan.Plugin.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedListView), typeof(ExtendedListViewRender))]
namespace Radovan.Plugin.iOS.Renderers
{
    public class ExtendedListViewRender: ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null && !((ExtendedListView)Element).Scrollable)
            {
                Control.ScrollEnabled = false;
            }
        }
    }
}
