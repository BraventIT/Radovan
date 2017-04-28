using Radovan.Plugin.Core.Controls;
using Radovan.Plugin.UWP.Helper;
using Radovan.Plugin.UWP.Renderers;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(ExtendedListView), typeof(ExtendedListViewRender))]
namespace Radovan.Plugin.UWP.Renderers
{
    public class ExtendedListViewRender : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);

            var listView = Control as ListView;

            if (e.NewElement != null && !((ExtendedListView)Element).IsScrollable)
            {
                listView.Loaded += OnListViewLoaded;
            }
        }

        private void OnListViewLoaded(object sender, RoutedEventArgs e)
        {
            var listView = sender as ListView;

            listView.Loaded -= OnListViewLoaded;

            var scroll = listView.FindFirstChild<ScrollViewer>();
            if (scroll != null)
            {
                scroll.VerticalScrollMode = ScrollMode.Disabled;
            }
        }
    }
}
