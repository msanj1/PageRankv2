using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PageRank.Core.Interfaces;
using PageRank.Core.SearchEngines;

namespace PageRank.Infrastructure.Dependency
{
    public class Factories
    {
        public static GoogleSearchEngine GoogleSearchEngineService(IServiceProvider serviceProvider)
        {
            var dataService = serviceProvider.GetRequiredService<ISearchEngineDataService>();
            var parserFactory = serviceProvider.GetRequiredService<ISearchResultParserFactory>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var key = "GoogleUrl";
            var url = configuration[key];

            if (string.IsNullOrEmpty(url)) throw new Exception($"Key {key} was not found");
            return new GoogleSearchEngine(dataService, parserFactory, url);
        }

        public static BingSearchEngine BingSearchEngineService(IServiceProvider serviceProvider)
        {
            var dataService = serviceProvider.GetRequiredService<ISearchEngineDataService>();
            var parserFactory = serviceProvider.GetRequiredService<ISearchResultParserFactory>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var key = "BingUrl";
            var url = configuration[key];

            if (string.IsNullOrEmpty(url)) throw new Exception($"Key {key} was not found");
            return new BingSearchEngine(dataService, parserFactory, url);
        }
    }
}