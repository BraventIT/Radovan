using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Radovan.Plugin.Android.Extensions;
using Radovan.Plugin.Android.Renderers;
using Radovan.Plugin.Core.Controls;
using Radovan.Plugin.Core.Enums;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;
using Color = Xamarin.Forms.Color;
using Widgets = Android.Widget;

[assembly: ExportRenderer(typeof(ImageButton), typeof(ImageButtonRenderer))]
namespace Radovan.Plugin.Android.Renderers
{
	public partial class ImageButtonRenderer : ButtonRenderer
	{
		private static float _density = float.MinValue;

		private ImageButton ImageButton
		{
			get { return (ImageButton)Element; }
		}

		protected async override void OnElementChanged(ElementChangedEventArgs<Button> e)
		{
			base.OnElementChanged(e);

			_density = Resources.DisplayMetrics.Density;

			var targetButton = Control;
			if (targetButton != null) targetButton.SetOnTouchListener(TouchListener.Instance.Value);

			if (Element != null && Element.Font != Font.Default && targetButton != null) targetButton.Typeface = Element.Font.ToExtendedTypeface(Context);

			if (Element != null && ImageButton.Source != null) await SetImageSourceAsync(targetButton, ImageButton).ConfigureAwait(false);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing && Control != null)
			{
				Control.Dispose();
			}
		}

		private async Task SetImageSourceAsync(Widgets.Button targetButton, ImageButton model)
		{
			if (targetButton == null || targetButton.Handle == IntPtr.Zero || model == null) return;

			// const int Padding = 10;
			var source = model.IsEnabled ? model.Source : model.DisabledSource ?? model.Source;

			using (var bitmap = await GetBitmapAsync(source).ConfigureAwait(false))
			{
				if (bitmap == null)
					targetButton.SetCompoundDrawables(null, null, null, null);
				else
				{
					var drawable = new BitmapDrawable(bitmap);
					var tintColor = model.IsEnabled ? model.ImageTintColor : model.DisabledImageTintColor;
					if (tintColor != Color.Transparent)
					{
						drawable.SetTint(tintColor.ToAndroid());
						drawable.SetTintMode(PorterDuff.Mode.SrcIn);
					}

					using (var scaledDrawable = GetScaleDrawable(drawable, GetWidth(model.ImageWidthRequest), GetHeight(model.ImageHeightRequest)))
					{
						Drawable left = null;
						Drawable right = null;
						Drawable top = null;
						Drawable bottom = null;
						//System.Diagnostics.Debug.WriteLine($"SetImageSourceAsync intptr{targetButton.Handle}");
						int padding = 10; // model.Padding
						targetButton.CompoundDrawablePadding = RequestToPixels(padding);
						switch (model.Orientation)
						{
							case ImageOrientation.ImageToLeft:
								targetButton.Gravity = GravityFlags.Left | GravityFlags.CenterVertical;
								left = scaledDrawable;
								break;
							case ImageOrientation.ImageToRight:
								targetButton.Gravity = GravityFlags.Right | GravityFlags.CenterVertical;
								right = scaledDrawable;
								break;
							case ImageOrientation.ImageOnTop:
								targetButton.Gravity = GravityFlags.Top | GravityFlags.CenterHorizontal;
								top = scaledDrawable;
								break;
							case ImageOrientation.ImageOnBottom:
								targetButton.Gravity = GravityFlags.Bottom | GravityFlags.CenterHorizontal;
								bottom = scaledDrawable;
								break;
							case ImageOrientation.ImageCentered:
								targetButton.Gravity = GravityFlags.Center; // | GravityFlags.Fill;
								top = scaledDrawable;
								break;
						}

						targetButton.SetCompoundDrawables(left, top, right, bottom);
					}
				}
			}
		}

		private async Task<Bitmap> GetBitmapAsync(ImageSource source)
		{
			var handler = GetHandler(source);
			var returnValue = (Bitmap)null;

			if (handler != null)
				returnValue = await handler.LoadImageAsync(source, Context).ConfigureAwait(false);

			return returnValue;
		}

		protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == ImageButton.SourceProperty.PropertyName ||
				e.PropertyName == ImageButton.DisabledSourceProperty.PropertyName ||
				e.PropertyName == VisualElement.IsEnabledProperty.PropertyName ||
				e.PropertyName == ImageButton.ImageTintColorProperty.PropertyName ||
				e.PropertyName == ImageButton.DisabledImageTintColorProperty.PropertyName)
			{
				await SetImageSourceAsync(Control, ImageButton).ConfigureAwait(false);
			}
		}

		private Drawable GetScaleDrawable(Drawable drawable, int width, int height)
		{
			var returnValue = new ScaleDrawable(drawable, 0, 100, 100).Drawable;

			returnValue.SetBounds(0, 0, RequestToPixels(width), RequestToPixels(height));

			return returnValue;
		}

		public int RequestToPixels(int sizeRequest)
		{
			if (_density == float.MinValue)
			{
				if (Resources.Handle == IntPtr.Zero || Resources.DisplayMetrics.Handle == IntPtr.Zero)
					_density = 1.0f;
				else
					_density = Resources.DisplayMetrics.Density;
			}

			return (int)(sizeRequest * _density);
		}

		private static IImageSourceHandler GetHandler(ImageSource source)
		{
			IImageSourceHandler returnValue = null;
			if (source is UriImageSource)
			{
				returnValue = new ImageLoaderSourceHandler();
			}
			else if (source is FileImageSource)
			{
				returnValue = new FileImageSourceHandler();
			}
			else if (source is StreamImageSource)
			{
				returnValue = new StreamImagesourceHandler();
			}
			return returnValue;
		}

		private int GetWidth(int requestedWidth)
		{
			const int DefaultWidth = 50;
			return requestedWidth <= 0 ? DefaultWidth : requestedWidth;
		}

		private int GetHeight(int requestedHeight)
		{
			const int DefaultHeight = 50;
			return requestedHeight <= 0 ? DefaultHeight : requestedHeight;
		}
	}

	class TouchListener : Java.Lang.Object, View.IOnTouchListener
	{
		public static readonly Lazy<TouchListener> Instance = new Lazy<TouchListener>(() => new TouchListener());

		private TouchListener()
		{ }

		public bool OnTouch(View v, MotionEvent e)
		{
			var buttonRenderer = v.Tag as ButtonRenderer;
			if (buttonRenderer != null && e.Action == MotionEventActions.Down) buttonRenderer.Control.Text = buttonRenderer.Element.Text;

			return false;
		}
	}
}
