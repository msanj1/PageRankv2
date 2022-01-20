using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PageRank.Core.Interfaces;

namespace PageRank.Core.Features.Search.SearchUrlPositions
{
    public class SearchUrlPositionsQuery : IRequest<SearchUrlPositionsResponse>
    {
        public SearchUrlPositionsQuery(string url, string type, string title)
        {
            Url = url == string.Empty ? throw new ArgumentNullException(url) : url;
            SearchEngineType = type == string.Empty ? throw new ArgumentNullException(type) : type;
            Title = title;
        }

        public string Url { get; set; }
        public string SearchEngineType { get; set; }
        public string Title { get; set; }
    }

    public class SearchUrlPositionQueryHandler : IRequestHandler<SearchUrlPositionsQuery, SearchUrlPositionsResponse>
    {
        private readonly ISearchEngineFactory _searchEngineFactory;
        private readonly IUrlHelper _urlHelper;

        public SearchUrlPositionQueryHandler(ISearchEngineFactory searchEngineFactory, IUrlHelper urlHelper)
        {
            _searchEngineFactory = searchEngineFactory ?? throw new ArgumentNullException(nameof(searchEngineFactory));
            _urlHelper = urlHelper ?? throw new ArgumentNullException(nameof(urlHelper));
        }

        public async Task<SearchUrlPositionsResponse> Handle(SearchUrlPositionsQuery request,
            CancellationToken cancellationToken)
        {
            var searchEngine = _searchEngineFactory.Create(request.SearchEngineType);
            var results = new SearchUrlPositionsResponse();
            var hostName = _urlHelper.GetHostName(request.Url);

            var searchResults = await searchEngine.GetSearchResultsAsync(request.Title);
            foreach (var searchResult in searchResults
            ) //inefficient algorithm for large data sets. Needs to be refactored if the expected data-set is large.
                if (searchResult.Urls.Any(r => r.Contains(hostName)))
                    results.SearchPositions.Add(searchResult.Page);

            if (results.SearchPositions.Count == 0) results.SearchPositions.Add(0);

            return results;
        }
    }
}