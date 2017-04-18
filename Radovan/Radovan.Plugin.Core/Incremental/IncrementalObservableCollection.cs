using Radovan.Plugin.Abstractions.Incremental;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Radovan.Plugin.Core.Incremental
{
    public class IncrementalObservableCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
       where T : class
    {
        private IIncrementalRepository<T> repository;
        private string currentPage = string.Empty;

        public IncrementalObservableCollection(IIncrementalRepository<T> source)
        {
            repository = source;
            HasMoreItems = true;
            repository.HasMoreItems = true;
        }

        public int PageSize { get; set; }
        public bool HasMoreItems { get; set; }
        public bool IsLoadingIncrementally { get; set; }

        public async Task LoadMoreItemsAsync()
        {
            bool checkMoreItems = repository.HasMoreItems;

            if (checkMoreItems)
            {
                var result = await repository.GetMoreItemsAsync(currentPage);
                if (result != null)
                {
                    if (result.Items != null)
                    {
                        currentPage = result.NewPage;
                        foreach (var item in result.Items)
                        {
                            Add(item);
                        }

                    }
                    HasMoreItems = repository.HasMoreItems;
                }
            }
            else
            { HasMoreItems = false; }
        }
    }

}
