using System.Threading.Tasks;

namespace Radovan.Plugin.Abstractions.Helpers
{
    public interface IImageResizer
    {
        Task<byte[]> ResizeImage(byte[] imageData, float width, float height);
        Task<byte[]> ResizeImage(byte[] imageData, float maxDimesion);
    }
}
