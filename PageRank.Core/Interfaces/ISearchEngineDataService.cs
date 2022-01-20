using System.Threading.Tasks;

namespace PageRank.Core.Interfaces
{
    public interface ISearchEngineDataService
    {
        Task<string> GetHtmlContentAsync(string resourceIdentifier);
    }
}