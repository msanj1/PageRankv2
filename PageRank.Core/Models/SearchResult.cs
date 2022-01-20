using System.Collections.Generic;

namespace PageRank.Core.Models
{
    public class SearchResult
    {
        public SearchResult(int page)
        {
            Urls = new List<string>();
            Page = page;
        }

        public List<string> Urls { get; }
        public int Page { get; set; }
    }
}