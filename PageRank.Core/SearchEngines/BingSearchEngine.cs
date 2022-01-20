using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PageRank.Core.Interfaces;
using PageRank.Core.Models;

namespace PageRank.Core.SearchEngines
{
    /// <summary>
    /// A wrapper class or a base class can be used to merge GoogleSearchEngine and BingSearchEngine features together.
    /// I intentionally left these classes as separate to convey that each search engine will have separate implementations and their implementation can be
    /// different in a real world scenario.
    /// </summary>
    public class BingSearchEngine : ISearchEngine
    {
        private readonly string _baseUrl;
        private readonly ISearchResultParserFactory _parserFactory;
        private readonly ISearchEngineDataService _searchEngineDataService;

        public BingSearchEngine(ISearchEngineDataService searchEngineDataService,
            ISearchResultParserFactory parserFactory,
            string baseUrl)
        {
            _searchEngineDataService = searchEngineDataService ??
                                       throw new ArgumentNullException(nameof(searchEngineDataService));
            _parserFactory = parserFactory ?? throw new ArgumentNullException(nameof(parserFactory));
            _baseUrl = string.IsNullOrEmpty(baseUrl) ? throw new ArgumentNullException(nameof(baseUrl)) : baseUrl;
        }

        public async Task<List<SearchResult>> GetSearchResultsAsync(string title)
        {
            var output = new List<SearchResult>();
            var htmlParser = _parserFactory.Create(SearchEngineType.Bing);
            for (var i = 0; i < 5; i++)
            {
                var currentPage = i + 1;

                var htmlContent =
                    await _searchEngineDataService.GetHtmlContentAsync($"{_baseUrl}/Page{currentPage:00}.html");
                var searchResult = new SearchResult(currentPage);

                var urls = htmlParser.ExtractUrls(htmlContent);
                foreach (var url in urls) searchResult.Urls.Add(url);

                if (urls.Count == 0 
                ) //we have no more pages as this page returned no search results. This business logic is quite simple and needs to be adapted for a proper system.
                    break;
                output.Add(searchResult);
            }

            return output;
        }
    }
}