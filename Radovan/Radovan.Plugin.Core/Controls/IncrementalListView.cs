using Radovan.Plugin.Abstractions.Incremental;
using Radovan.Plugin.Core.Incremental;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace Radovan.Plugin.Core.Controls
{
    public class IncrementalListView : ExtendedListView
    {
        int lastPosition;
        IList itemsSource;
        ISupportIncrementalLoading incrementalLoading;

        public IncrementalListView() : this(  ListViewCachingStrategy.RecycleElement)
        {
        }

        public IncrementalListView(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy)
        {
            ItemAppearing += OnItemAppearing;
        }

        protected override async void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ItemsSourceProperty.PropertyName)
            {
                itemsSource = ItemsSource as IList;
                if (itemsSource == null)
                {
                    throw new Exception($"{nameof(IncrementalListView)} requires that {nameof(itemsSource)} be of type IList");
                }
                incrementalLoading = ItemsSource as ISupportIncrementalLoading;
                if (incrementalLoading == null)
                {
                    throw new Exception($"{nameof(IncrementalListView)} requires that {nameof(itemsSource)} be of type ISupportIncrementalLoading");
                }

                await incrementalLoading.LoadMoreItemsAsync();
            }
        }

        private async void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (itemsSource == null || incrementalLoading == null)
                return;

            int position = itemsSource.IndexOf(e.Item);

            if (PreloadCount <= 0)
                PreloadCount = 1;

            int preloadIndex = Math.Max(itemsSource.Count - PreloadCount, 0);

            if ((position > lastPosition || (position == itemsSource.Count - 1)) && (position >= preloadIndex))
            {
                lastPosition = position;

                if (!incrementalLoading.IsLoadingIncrementally
                    && !IsRefreshing
                    && incrementalLoading.HasMoreItems)
                {
                    await incrementalLoading.LoadMoreItemsAsync();
                }
            }
        }

        public int PreloadCount
        {
            get { return (int)GetValue(PreloadCountProperty); }
            set { SetValue(PreloadCountProperty, value); }
        }

        public static readonly BindableProperty PreloadCountProperty =
            BindableProperty.Create("PreloadCount", typeof(int), typeof(IncrementalListView), 0);


    }
}
