using System.Threading.Tasks;

namespace Radovan.Plugin.Abstractions.Incremental
{
    public interface IIncrementalRepository<T>
    {
        bool HasMoreItems { get; set; }
        Task<IncrementalResult<T>> GetMoreItemsAsync(string page);
    }
}
