using System;
using Microsoft.Extensions.DependencyInjection;
using PageRank.Core.Interfaces;
using PageRank.Core.Models;
using PageRank.Core.SearchEngines;

namespace PageRank.Infrastructure.Factories
{
    public class SearchEngineFactory : ISearchEngineFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SearchEngineFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public ISearchEngine Create(string type)
        {
            switch (type)
            {
                case SearchEngineType.Bing:
                    return (ISearchEngine) _serviceProvider.GetRequiredService(typeof(BingSearchEngine));
                case SearchEngineType.Google:
                    return (ISearchEngine) _serviceProvider.GetRequiredService(typeof(GoogleSearchEngine));
                default:
                    throw new Exception($"No implementation found for {type}");
            }
        }
    }
}