using Radovan.Plugin.Abstractions.Helpers;

namespace Radovan.Plugin.Core.Helpers
{
    public class ImageResizerManager
    {
        public static IImageResizer ImageResize { get; internal set; }

        public static void Init(IImageResizer imageResize)
        {
            ImageResize = imageResize;
        }
    }
}
