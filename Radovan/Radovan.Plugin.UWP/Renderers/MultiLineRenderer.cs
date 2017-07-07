
using Radovan.Plugin.Core.Controls;
using Radovan.Plugin.UWP.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(MultiLineLabel), typeof(MultiLineRenderer))]

namespace Radovan.Plugin.UWP.Renderers
{

    public class MultiLineRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            MultiLineLabel multiLineLabel = (MultiLineLabel)this.Element;

            if (multiLineLabel != null && multiLineLabel.Lines != -1)
            {
                this.Control.MaxLines = multiLineLabel.Lines;
            }
        }
    }
}