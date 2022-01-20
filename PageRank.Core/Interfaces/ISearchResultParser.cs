using System.Collections.Generic;

namespace PageRank.Core.Interfaces
{
    public interface ISearchResultParser
    {
        List<string> ExtractUrls(string htmlContent);
    }
}