

using Radovan.Plugin.Core.Controls;
using Radovan.Plugin.UWP.Renderers;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(ExtendedEditor), typeof(ExtendedEditorRenderer))]

namespace Radovan.Plugin.UWP.Renderers
{
    public class ExtendedEditorRenderer : EditorRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            var element = this.Element as ExtendedEditor;

            if (Control != null && element != null)
            {
                Control.BorderThickness = new Windows.UI.Xaml.Thickness(1);
                var color = element.BorderColor;
                Windows.UI.Color wpColor = Windows.UI.Color.FromArgb(
                            (byte)(color.A * 255),
                            (byte)(color.R * 255),
                            (byte)(color.G * 255),
                            (byte)(color.B * 255));

                Control.BorderBrush = new SolidColorBrush(wpColor);
            }
        }
    }
}