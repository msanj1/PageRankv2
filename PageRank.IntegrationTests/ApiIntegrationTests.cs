using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PageRank.Api;
using Xunit;

namespace PageRank.IntegrationTests
{
    public class ApiIntegrationTests : IDisposable
    {
        private readonly HttpClient _client;
        private readonly TestServer _server;

        public ApiIntegrationTests()
        {
            _server = new TestServer(new WebHostBuilder()
                .ConfigureAppConfiguration(cb => { cb.AddJsonFile("./appsettings.json", false); })
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        public void Dispose()
        {
            _server.Dispose();
            _client.Dispose();
        }

        [Fact]
        public async Task ShouldReturnSuccessStatusCodeGivenUrlEngineTypeAndTitle()
        {
            // Arrange

            var endPoint = "api/v1/pagerank";
            var urlToSearch = "https://easytitlesearch.com";
            var searchEngineType = "bing";
            var keyboard = "Online Title Search";
            var url = $"{endPoint}?url={urlToSearch}&searchEngineType={searchEngineType}&titleSearch={keyboard}";

            //act
            var response = await _client.GetAsync(url);

            //assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData("https://six.nsw.gov.au", "bing", "Online Title Search")]
        [InlineData("https://www.vicroads.vic.gov.au", "google", "Online Title Search")]
        public async Task ShouldReturnAResultGivenUrlEngineTypeAndTitle(string urlToSearch,
            string searchEngineType, string keyboard)
        {
            // Arrange
            var endPoint = "api/v1/pagerank";
            var url = $"{endPoint}?url={urlToSearch}&searchEngineType={searchEngineType}&titleSearch={keyboard}";

            //act
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<int>>(responseString);

            //assert
            Assert.True(result.Count > 0);
        }
    }
}