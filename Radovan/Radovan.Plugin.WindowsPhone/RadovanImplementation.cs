using Radovan.FormsPlugin.Abstractions;
using System;
using Xamarin.Forms;
using Radovan.FormsPlugin.WindowsPhone;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(Radovan.FormsPlugin.Abstractions.RadovanControl), typeof(RadovanRenderer))]
namespace Radovan.FormsPlugin.WindowsPhone
{
    /// <summary>
    /// Radovan Renderer
    /// </summary>
    public class RadovanRenderer //: TRender (replace with renderer type
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }
    }
}
