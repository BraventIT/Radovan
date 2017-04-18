using System.Threading.Tasks;

namespace Radovan.Plugin.Abstractions.Incremental
{
    public interface ISupportIncrementalLoading
    {
        int PageSize { get; set; }
        bool HasMoreItems { get; set; }
        bool IsLoadingIncrementally { get; set; }
        Task LoadMoreItemsAsync();
    }
}
