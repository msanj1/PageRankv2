using System;
using Microsoft.Extensions.DependencyInjection;
using PageRank.Core.Interfaces;
using PageRank.Core.Models;
using PageRank.Infrastructure.Parsers;

namespace PageRank.Infrastructure.Factories
{
    public class SearchResultParserFactory: ISearchResultParserFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SearchResultParserFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public ISearchResultParser Create(string searchEngineType)
        {
            switch (searchEngineType)
            {
                case SearchEngineType.Bing:
                    return (ISearchResultParser) _serviceProvider.GetRequiredService(typeof(BingHapParser));
                case SearchEngineType.Google:
                    return (ISearchResultParser) _serviceProvider.GetRequiredService(typeof(GoogleHapParser));
                //case SearchEngineType.Bing:
                //    return (ISearchResultParser)_serviceProvider.GetRequiredService(typeof(BingRegexParser));
                //case SearchEngineType.Google:
                //    return (ISearchResultParser)_serviceProvider.GetRequiredService(typeof(GoogleRegexParser));
                default:
                    throw new Exception($"No implementation found for {searchEngineType}");
            }
        }
    }
}