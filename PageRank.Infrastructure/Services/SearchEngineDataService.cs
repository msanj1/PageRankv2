using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using PageRank.Core.Interfaces;

namespace PageRank.Infrastructure.Services
{
    public class SearchEngineDataService : ISearchEngineDataService, IDisposable
    {
        private readonly IMemoryCache _cache;
        private readonly HttpClient _client;

        public SearchEngineDataService(IMemoryCache cache)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _client = new HttpClient();
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public async Task<string> GetHtmlContentAsync(string url)
        {
            string htmlContent;
            var hasCachedResult =
                GetCachedContent(url,
                    out htmlContent); //this caching strategy is ok for a simple POC, but may not be applicable for an actual web scraper
            if (hasCachedResult) return htmlContent;

            var result = await _client.GetAsync(url);
            result.EnsureSuccessStatusCode();

            htmlContent = await result.Content.ReadAsStringAsync();
            SetCache(url, htmlContent);
            return htmlContent;
        }

        private bool GetCachedContent(string key, out string content)
        {
            content = string.Empty;
            return _cache != null && _cache.TryGetValue(key, out content);
        }

        private void SetCache(string key, string htmlContent)
        {
            if (_cache != null && !_cache.TryGetValue(key, out string cacheEntry))
            {
                // Key not in cache, so get data.
                cacheEntry = htmlContent;

                // Save data in cache and set the relative expiration time to one day
                _cache.Set(key, cacheEntry, TimeSpan.FromDays(1));
            }
        }
    }
}