using System;
using Android.Graphics;
using Android.Widget;
using Radovan.Plugin.Android.Extensions;
using Radovan.Plugin.Android.Renderers;
using Radovan.Plugin.Core.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedLabel), typeof(ExtendedLabelRenderer))]
namespace Radovan.Plugin.Android.Renderers
{
	public class ExtendedLabelRenderer : LabelRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			var view = (ExtendedLabel)Element;
			var control = Control;

			UpdateUi(view, control);

		}

		void UpdateUi(ExtendedLabel view, TextView control)
		{
			if (!string.IsNullOrEmpty(view.FontName))
			{
				string filename = view.FontName;
				//if no extension given then assume and add .ttf
				if (filename.LastIndexOf(".", System.StringComparison.Ordinal) != filename.Length - 4)
				{
					filename = string.Format("{0}.ttf", filename);
				}
				control.Typeface = TrySetFont(filename);
			}

			else if (view.Font != Font.Default)
			{
				control.Typeface = view.Font.ToExtendedTypeface(Context);
			}

			if (view.FontSize > 0)
			{
				control.TextSize = (float)view.FontSize;
			}

			if (view.IsUnderline)
			{
				control.PaintFlags = control.PaintFlags | PaintFlags.UnderlineText;
			}

			if (view.IsStrikeThrough)
			{
				control.PaintFlags = control.PaintFlags | PaintFlags.StrikeThruText;
			}

			if (view.Lines != -1)
			{
				Control.SetSingleLine(false);
				Control.SetLines(view.Lines);
			}

		}

		private Typeface TrySetFont(string fontName)
		{
			try
			{
				return Typeface.CreateFromAsset(Context.Assets, "fonts/" + fontName);
			}
			catch (Exception ex)
			{
				Console.WriteLine("not found in assets. Exception: {0}", ex);
				try
				{
					return Typeface.CreateFromFile("fonts/" + fontName);
				}
				catch (Exception ex1)
				{
					Console.WriteLine("not found by file. Exception: {0}", ex1);

					return Typeface.Default;
				}
			}
		}
	}
}
