using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PageRank.Core.Models;

namespace PageRank.Core.Interfaces
{
    public interface ISearchEngine
    {
        public Task<List<SearchResult>> GetSearchResultsAsync(string title);
    }
}