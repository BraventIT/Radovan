using Xamarin.Forms;

namespace Radovan.Plugin.Core.Controls
{
    public class ExtendedImage : Image
    {
        #region SourcePlatform Bindable Property
        public ImageSource SourcePlatform
        {
            get { return (ImageSource)GetValue(SourcePlatformProperty); }
            set { SetValue(SourcePlatformProperty, value); }
        }

        public static readonly BindableProperty SourcePlatformProperty = BindableProperty.Create(
            propertyName: nameof(SourcePlatform),
            returnType: typeof(ImageSource),
            declaringType: typeof(ExtendedImage),
            defaultValue: default(ImageSource),
            propertyChanged: OnSourcePlatformChanged);

        private static void OnSourcePlatformChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var SourcePlatform = newValue as FileImageSource;
            string path = string.Empty;
            switch (Device.OS)
            {
                case TargetPlatform.WinPhone:
                case TargetPlatform.Windows:
                    path = $"Assets/{SourcePlatform.File}.png"; break;
                default:
                    path = SourcePlatform.File; break;
            }
            SourcePlatform.File = path;
            bindable.SetValue(SourceProperty, SourcePlatform);
        }
        #endregion
    }
}
