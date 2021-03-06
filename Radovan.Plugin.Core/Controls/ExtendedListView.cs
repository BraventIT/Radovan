﻿using System.Windows.Input;
using Xamarin.Forms;

namespace Radovan.Plugin.Core.Controls
{
    public class ExtendedListView : ListView
    {
        public ExtendedListView() : this(ListViewCachingStrategy.RecycleElement)
        {
        }

        public ExtendedListView(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy)
        {
            ItemTapped += OnItemTapped;
        }

        #region ItemSelectedCommand Bindable Property
        public ICommand ItemSelectedCommand
        {
            get { return (ICommand)GetValue(ItemSelectedCommandProperty); }
            set { SetValue(ItemSelectedCommandProperty, value); }
        }

        public static readonly BindableProperty ItemSelectedCommandProperty = BindableProperty.Create(
            propertyName: nameof(ItemSelectedCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(ExtendedListView),
            defaultValue: default(ICommand));
        #endregion

        #region Scrollable Bindable Property
        public bool IsScrollable
        {
            get { return (bool)GetValue(IsScrollableProperty); }
            set { SetValue(IsScrollableProperty, value); }
        }

        public static readonly BindableProperty IsScrollableProperty = BindableProperty.Create(
            propertyName: nameof(IsScrollable),
            returnType: typeof(bool),
            declaringType: typeof(ExtendedListView),
            defaultValue: true);
        #endregion

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item != null && ItemSelectedCommand != null && ItemSelectedCommand.CanExecute(e.Item))
            {
                ItemSelectedCommand.Execute(e.Item);
            }

            SelectedItem = null;
        }
    }
}
