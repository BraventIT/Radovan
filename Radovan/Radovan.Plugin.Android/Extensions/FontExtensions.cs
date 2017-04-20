using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Renderscripts;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Radovan.Plugin.Android.Extensions
{
	public interface ITypefaceCache
	{
		void StoreTypeface(string key, Typeface typeface);

		void RemoveTypeface(string key);

		Typeface RetrieveTypeface(string key);
	}

	public static class TypefaceCache
	{
		private static ITypefaceCache sharedCache;

		public static ITypefaceCache SharedCache
		{
			get
			{
				if (sharedCache == null)
				{
					sharedCache = new DefaultTypefaceCache();
				}
				return sharedCache;
			}
			set
			{
				if (sharedCache != null && sharedCache.GetType() == typeof(DefaultTypefaceCache))
				{
					((DefaultTypefaceCache)sharedCache).PurgeCache();
				}
				sharedCache = value;
			}
		}
	}


	internal class DefaultTypefaceCache : ITypefaceCache
	{
		private Dictionary<string, Typeface> _cacheDict;

		public DefaultTypefaceCache()
		{
			_cacheDict = new Dictionary<string, Typeface>();
		}


		public Typeface RetrieveTypeface(string key)
		{
			if (_cacheDict.ContainsKey(key))
			{
				return _cacheDict[key];
			}
			else
			{
				return null;
			}
		}

		public void StoreTypeface(string key, Typeface typeface)
		{
			_cacheDict[key] = typeface;
		}

		public void RemoveTypeface(string key)
		{
			_cacheDict.Remove(key);
		}

		public void PurgeCache()
		{
			_cacheDict = new Dictionary<string, Typeface>();
		}
	}



	public static class FontExtensions
	{

		public static Typeface ToExtendedTypeface(this Xamarin.Forms.Font font, Context context)
		{
			Typeface typeface = null;

			//1. Lookup in the cache
			var hashKey = font.ToHasmapKey();
			typeface = TypefaceCache.SharedCache.RetrieveTypeface(hashKey);
#if DEBUG
			if (typeface != null)
				Console.WriteLine("Typeface for font {0} found in cache", font);
#endif

			//2. If not found, try custom asset folder
			if (typeface == null && !string.IsNullOrEmpty(font.FontFamily))
			{
				string filename = font.FontFamily;
				//if no extension given then assume and add .ttf
				if (filename.LastIndexOf(".", System.StringComparison.Ordinal) != filename.Length - 4)
				{
					filename = string.Format("{0}.ttf", filename);
				}
				try
				{
					var path = "fonts/" + filename;
#if DEBUG
					Console.WriteLine("Lookking for font file: {0}", path);
#endif
					typeface = Typeface.CreateFromAsset(context.Assets, path);
#if DEBUG
					Console.WriteLine("Found in assets and cached.");
#endif
#pragma warning disable CS0168 // Variable is declared but never used
				}
				catch (Exception ex)
				{
#if DEBUG
					Console.WriteLine("not found in assets. Exception: {0}", ex);
					Console.WriteLine("Trying creation from file");
#endif
					try
					{
						typeface = Typeface.CreateFromFile("fonts/" + filename);


#if DEBUG
						Console.WriteLine("Found in file and cached.");
#endif
					}
					catch (Exception ex1)
#pragma warning restore CS0168 // Variable is declared but never used
					{
#if DEBUG
						Console.WriteLine("not found by file. Exception: {0}", ex1);
						Console.WriteLine("Trying creation using Xamarin.Forms implementation");
#endif

					}
				}

			}
			//3. If not found, fall back to default Xamarin.Forms implementation to load system font
			if (typeface == null)
			{
				typeface = font.ToTypeface();
			}

			if (typeface == null)
			{
#if DEBUG
				Console.WriteLine("Falling back to default typeface");
#endif
				typeface = Typeface.Default;
			}
			//Store in cache
			TypefaceCache.SharedCache.StoreTypeface(hashKey, typeface);

			return typeface;

		}

		private static string ToHasmapKey(this Xamarin.Forms.Font font)
		{
			return string.Format("{0}.{1}.{2}.{3}", font.FontFamily, font.FontSize, font.NamedSize, (int)font.FontAttributes);
		}
	}
}
