using Microsoft.Extensions.DependencyInjection;
using PageRank.Core.Interfaces;
using PageRank.Infrastructure.Factories;
using PageRank.Infrastructure.Helpers;
using PageRank.Infrastructure.Parsers;
using PageRank.Infrastructure.Services;

namespace PageRank.Infrastructure.Dependency
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<HapParser>();
            services.AddScoped<GoogleHapParser>();
            services.AddScoped<BingHapParser>();

            services.AddScoped<GoogleRegexParser>();
            services.AddScoped<BingRegexParser>();

            services.AddScoped<ISearchEngineFactory, SearchEngineFactory>();
            services.AddScoped<IUrlHelper, UrlHelper>();
            services.AddScoped<ISearchEngineDataService, SearchEngineDataService>();
            services.AddScoped<ISearchResultParserFactory, SearchResultParserFactory>();

            services.AddScoped(Factories.GoogleSearchEngineService);
            services.AddScoped(Factories.BingSearchEngineService);
            return services;
        }
    }
}