using Radovan.FormsPlugin.Abstractions;
using System;
using Xamarin.Forms;
using Radovan.FormsPlugin.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Radovan.FormsPlugin.Abstractions.RadovanControl), typeof(RadovanRenderer))]
namespace Radovan.FormsPlugin.Android
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