using System.Threading.Tasks;
using Xamarin.Forms;

namespace Radovan.Plugin.Core.Controls
{
    public class ExtendedToolbarItem : ToolbarItem
    {
        bool _isloaded;

        #region IsVisible Bindable Property
        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        public static BindableProperty IsVisibleProperty =
            BindableProperty.Create(
               propertyName: nameof(IsVisible),
               returnType: typeof(bool),
              declaringType: typeof(ExtendedToolbarItem),
              defaultValue: true,
              propertyChanged: OnIsVisibleChanged);

        static void OnIsVisibleChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as ExtendedToolbarItem;

            if (control.Parent == null)
                return;

            if (control._isloaded)
                control.UpdateIsVisible();
        }
        #endregion

        protected override async void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            await Task.Delay(100);
            UpdateIsVisible();
            _isloaded = true;
        }

        private void UpdateIsVisible()
        {
            var page = Parent as Page;

            if (page == null)
                return;

            var items = page.ToolbarItems;

            if (IsVisible)
            {
                if (!items.Contains(this))
                    items.Add(this);
            }
            else
            {
                if (items.Contains(this))
                    items.Remove(this);
            }
        }
    }
}