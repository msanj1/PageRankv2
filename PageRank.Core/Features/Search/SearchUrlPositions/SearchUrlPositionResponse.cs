using System.Collections.Generic;

namespace PageRank.Core.Features.Search.SearchUrlPositions
{
    public class SearchUrlPositionsResponse
    {
        public SearchUrlPositionsResponse()
        {
            SearchPositions = new List<int>();
        }

        public List<int> SearchPositions { get; set; }
    }
}