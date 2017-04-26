using System;
using System.IO;
using System.Threading.Tasks;
using Radovan.Plugin.Abstractions.Helpers;

#if __IOS__
using System.Drawing;
using UIKit;
using CoreGraphics;
#endif

#if __ANDROID__
using Android.Graphics;
#endif

#if WINDOWS_PHONE
using Microsoft.Phone;
using System.Windows.Media.Imaging;
#endif

#if WINDOWS_PHONE_APP
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
#endif

#if WINDOWS_UWP
using Windows.Storage.Streams;
using Windows.Graphics.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
#endif

namespace $rootnamespace$.Helpers
{
    public class ImageResizer : IImageResizer
    {
        public async Task<byte[]> ResizeImage(byte[] imageData, float width, float height)
        {
#if __IOS__
			return ResizeImageIOS(imageData, width, height);
#endif
#if __ANDROID__
            return ResizeImageAndroid(imageData, width, height);
#endif
#if WINDOWS_PHONE
			return ResizeImageWinPhone ( imageData, width, height );
#endif
#if WINDOWS_PHONE_APP
            return await ResizeImageWindows(imageData, width, height);
#endif
#if WINDOWS_APP
            return null;
#endif
#if WINDOWS_UWP
            return await ResizeImageWindows(imageData, width, height);
#else
            return null;
#endif
        }

        public async Task<byte[]> ResizeImage(byte[] imageData, float maxDimesion)
        {
#if __IOS__
			return ResizeImageIOS(imageData, maxDimesion);
#endif
#if __ANDROID__
            return ResizeImageAndroid(imageData, maxDimesion);
#endif
#if WINDOWS_PHONE
			return ResizeImageWinPhone ( imageData, maxDimesion);
#endif
#if WINDOWS_PHONE_APP
            return await ResizeImageWindows(imageData,maxDimesion);
#endif
#if WINDOWS_APP
            return null;
#endif
#if WINDOWS_UWP
            return await ResizeImageWindows(imageData,maxDimesion);
#else
            return null;
#endif
        }

        private class Size
        {
            public float Width { get; set; }
            public float Height { get; set; }
        }

        Size NewSize(float width, float height, float maxDimesion)
        {
            float ow = width;
            float oh = height;

            float ar = 0.0f;
            if (oh < ow) //Horizontal
            {
                ar = oh / ow;
                return new Size { Width = maxDimesion, Height = ar * maxDimesion };
            }
            else
            {
                ar = ow / oh;
                return new Size { Width = ar * maxDimesion, Height = maxDimesion };
            }
        }

#if __IOS__
		byte[] ResizeImageIOS(byte[] imageData, float width, float height)
		{
			UIImage originalImage = ImageFromByteArray(imageData);
            UIImageOrientation orientation = originalImage.Orientation;

			//create a 24bit RGB image
			using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
												 (int)width, (int)height, 8,
												 (int)(4 * width), CGColorSpace.CreateDeviceRGB(),
												 CGImageAlphaInfo.PremultipliedFirst))
			{

				RectangleF imageRect = new RectangleF(0, 0, width, height);

				// draw the image
				context.DrawImage(imageRect, originalImage.CGImage);

				UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage(), 0, orientation);

				// save the image as a jpeg
				return resizedImage.AsJPEG().ToArray();
			}
		}

        byte[] ResizeImageIOS(byte[] imageData, float maxDimension)
        {
            UIImage originalImage = ImageFromByteArray(imageData);

            var size = NewSize((float)originalImage.Size.Width, (float)originalImage.Size.Height, maxDimension);
			var width = Math.Round(size.Width, 0);
			var height = Math.Round( size.Height, 0);

            UIImageOrientation orientation = originalImage.Orientation;

            //create a 24bit RGB image
            using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
                                                 (int)width, (int)height, 8,
                                                 (int)(4 * width), CGColorSpace.CreateDeviceRGB(),
                                                 CGImageAlphaInfo.PremultipliedFirst))
            {

				RectangleF imageRect = new RectangleF(0, 0, (float)width, (float)height);

                // draw the image
                context.DrawImage(imageRect, originalImage.CGImage);

                UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage(), 0, orientation);

                // save the image as a jpeg
                return resizedImage.AsJPEG().ToArray();
            }
        }

        UIKit.UIImage ImageFromByteArray(byte[] data)
		{
			if (data == null)
			{
				return null;
			}

			UIKit.UIImage image;
			try
			{
				image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
			}
			catch (Exception e)
			{
				Console.WriteLine("Image load failed: " + e.Message);
				return null;
			}
			return image;
		}
#endif

#if __ANDROID__

        byte[] ResizeImageAndroid(byte[] imageData, float width, float height)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }

        byte[] ResizeImageAndroid(byte[] imageData, float maxDimension)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            var size = NewSize(originalImage.Width, originalImage.Height, maxDimension);

            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)size.Width, (int)size.Height, false);

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 100, ms);
                return ms.ToArray();
            }
        }

#endif

#if WINDOWS_PHONE
		
        byte[] ResizeImageWinPhone (byte[] imageData, float width, float height)
        {
            byte[] resizedData;

            using (MemoryStream streamIn = new MemoryStream (imageData))
            {
                WriteableBitmap bitmap = PictureDecoder.DecodeJpeg (streamIn, (int)width, (int)height);

                using (MemoryStream streamOut = new MemoryStream ())
                {
                    bitmap.SaveJpeg(streamOut, (int)width, (int)height, 0, 100);
                    resizedData = streamOut.ToArray();
                }
            }
            return resizedData;
        }
        
#endif

#if WINDOWS_PHONE_APP

        async Task<byte[]> ResizeImageWindows(byte[] imageData, float width, float height)
        {
            byte[] resizedData;

            using (var streamIn = new MemoryStream(imageData))
            {
                using (var imageStream = streamIn.AsRandomAccessStream())
                {
                    var decoder = await BitmapDecoder.CreateAsync(imageStream);
                    var resizedStream = new InMemoryRandomAccessStream();
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(resizedStream, decoder);
                    encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Linear;
                    encoder.BitmapTransform.ScaledHeight = (uint)height;
                    encoder.BitmapTransform.ScaledWidth = (uint)width;
                    await encoder.FlushAsync();
                    resizedStream.Seek(0);
                    resizedData = new byte[resizedStream.Size];
                    await resizedStream.ReadAsync(resizedData.AsBuffer(), (uint)resizedStream.Size, InputStreamOptions.None);                  
                }                
            }

            return resizedData;
        }

        async Task<byte[]> ResizeImageWindows(byte[] imageData, float maxDimension)
        {
            byte[] resizedData;

            using (var streamIn = new MemoryStream(imageData))
            {
                using (var imageStream = streamIn.AsRandomAccessStream())
                {
                    var decoder = await BitmapDecoder.CreateAsync(imageStream);
                    var size = NewSize(decoder.PixelWidth, decoder.PixelHeight, maxDimension);
                    var resizedStream = new InMemoryRandomAccessStream();
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(resizedStream, decoder);
                    encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Linear;
                    encoder.BitmapTransform.ScaledHeight = (uint)size.Height;
                    encoder.BitmapTransform.ScaledWidth = (uint)size.Width;
                    await encoder.FlushAsync();
                    resizedStream.Seek(0);
                    resizedData = new byte[resizedStream.Size];
                    await resizedStream.ReadAsync(resizedData.AsBuffer(), (uint)resizedStream.Size, InputStreamOptions.None);
                }
            }

            return resizedData;
        }

#endif

#if WINDOWS_UWP

        async Task<byte[]> ResizeImageWindows(byte[] imageData, float width, float height)
        {
            byte[] resizedData;

            using (var streamIn = new MemoryStream(imageData))
            {
                using (var imageStream = streamIn.AsRandomAccessStream())
                {
                    var decoder = await BitmapDecoder.CreateAsync(imageStream);
                    var resizedStream = new InMemoryRandomAccessStream();
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(resizedStream, decoder);
                    encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Linear;
                    encoder.BitmapTransform.ScaledHeight = (uint)height;
                    encoder.BitmapTransform.ScaledWidth = (uint)width;
                    await encoder.FlushAsync();
                    resizedStream.Seek(0);
                    resizedData = new byte[resizedStream.Size];
                    await resizedStream.ReadAsync(resizedData.AsBuffer(), (uint)resizedStream.Size, InputStreamOptions.None);                  
                }                
            }

            return resizedData;
        }

        async Task<byte[]> ResizeImageWindows(byte[] imageData, float maxDimension)
        {
            byte[] resizedData;

            using (var streamIn = new MemoryStream(imageData))
            {
                using (var imageStream = streamIn.AsRandomAccessStream())
                {
                    var decoder = await BitmapDecoder.CreateAsync(imageStream);
                    var size = NewSize(decoder.PixelWidth, decoder.PixelHeight, maxDimension);
                    var resizedStream = new InMemoryRandomAccessStream();
                    var encoder = await BitmapEncoder.CreateForTranscodingAsync(resizedStream, decoder);
                    encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Linear;
                    encoder.BitmapTransform.ScaledHeight = (uint)size.Height;
                    encoder.BitmapTransform.ScaledWidth = (uint)size.Width;
                    await encoder.FlushAsync();
                    resizedStream.Seek(0);
                    resizedData = new byte[resizedStream.Size];
                    await resizedStream.ReadAsync(resizedData.AsBuffer(), (uint)resizedStream.Size, InputStreamOptions.None);
                }
            }

            return resizedData;
        }

#endif
    }
}

