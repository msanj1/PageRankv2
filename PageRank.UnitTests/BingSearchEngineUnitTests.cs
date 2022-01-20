using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using PageRank.Core.Interfaces;
using PageRank.Core.Models;
using PageRank.Core.SearchEngines;
using Xunit;

namespace PageRank.UnitTests
{
    public class BingSearchEngineUnitTests
    {
        [Fact]
        public async Task ShouldReturnExpectedSearchResults()
        {
            //arrange
            var url1 = "http://wwww.bla.com.au";
            var url2 = "http://wwww.bla2.com.au";
            var url3 = "http://wwww.bla3.com.au";

            var expectedSearchResults = new List<SearchResult>();
            var title = "online title search";
            var result1 = new SearchResult(1);
            result1.Urls.Add(url1);
            result1.Urls.Add(url2);
            expectedSearchResults.Add(result1);
            var result2 = new SearchResult(2);
            result2.Urls.Add(url3);
            expectedSearchResults.Add(result2);

            var htmlContentToReturn1 = $"<div>{url1}</div><div>{url2}</div>";
            var htmlContentToReturn2 = $"<div>{url3}</div>";
            var htmlContentToReturn3 = "<div></div>";

            var baseUrl = "https://wwww.bing.com.au";
            var searchEnginePage1ToScrape = $"{baseUrl}/Page{1:00}.html";
            var searchEnginePage2ToScrape = $"{baseUrl}/Page{2:00}.html";
            var searchEnginePage3ToScrape = $"{baseUrl}/Page{3:00}.html";

            var searchEngineDataService = new Mock<ISearchEngineDataService>();
            searchEngineDataService.Setup(s => s.GetHtmlContentAsync(searchEnginePage1ToScrape))
                .Returns(Task.FromResult(htmlContentToReturn1));
            searchEngineDataService.Setup(s => s.GetHtmlContentAsync(searchEnginePage2ToScrape))
                .Returns(Task.FromResult(htmlContentToReturn2));
            searchEngineDataService.Setup(s => s.GetHtmlContentAsync(searchEnginePage3ToScrape))
                .Returns(Task.FromResult(htmlContentToReturn3));

            var webScraper = new Mock<ISearchResultParser>();
            webScraper.Setup(s => s.ExtractUrls(htmlContentToReturn1))
                .Returns(new List<string> { url1, url2 });
            webScraper.Setup(s => s.ExtractUrls(htmlContentToReturn2))
                .Returns(new List<string> { url3 });
            webScraper.Setup(s => s.ExtractUrls(htmlContentToReturn3))
                .Returns(new List<string>());

            var webScraperFactory = new Mock<ISearchResultParserFactory>();
            webScraperFactory.Setup(s => s.Create(SearchEngineType.Bing)).Returns(webScraper.Object);

            var sut = new BingSearchEngine(searchEngineDataService.Object, webScraperFactory.Object, baseUrl);

            //act
            var result = await sut.GetSearchResultsAsync(title);

            //assert
            Assert.IsType<List<SearchResult>>(result);
            Assert.IsAssignableFrom<List<SearchResult>>(result);

            searchEngineDataService.Verify(s => s.GetHtmlContentAsync(It.IsAny<string>()), Times.Exactly(3));
            webScraper.Verify(w => w.ExtractUrls(It.IsAny<string>()), Times.Exactly(3));

            Assert.Equal(expectedSearchResults.Count, result.Count);
            Assert.Equal(expectedSearchResults.Select(s => s.Page), result.Select(s => s.Page));
            Assert.Equal(expectedSearchResults.Select(s => s.Urls), result.Select(r => r.Urls));
        }

        [Fact]
        public async Task ShouldReturnZeroSearchResult()
        {
            //arrange
            var baseUrl = "https://wwww.google.com.au";
            var title = "online title search";

            var searchEngineDataService = new Mock<ISearchEngineDataService>();
            var searchEnginePage1ToScrape = $"{baseUrl}/Page{1:00}.html";
            var emptyHtmlResultToReturn = "";
            var expectedSearchResultCount = 0;

            searchEngineDataService.Setup(s => s.GetHtmlContentAsync(searchEnginePage1ToScrape))
                .Returns(Task.FromResult(emptyHtmlResultToReturn));

            var webScraper = new Mock<ISearchResultParser>();
            var webScraperFactory = new Mock<ISearchResultParserFactory>();
            webScraperFactory.Setup(s => s.Create(SearchEngineType.Bing)).Returns(webScraper.Object);
            webScraper.Setup(s => s.ExtractUrls(emptyHtmlResultToReturn))
                .Returns(new List<string>());

            var sut = new BingSearchEngine(searchEngineDataService.Object, webScraperFactory.Object, baseUrl);

            //act
            var result = await sut.GetSearchResultsAsync(title);

            //assert
            webScraperFactory.Verify(f => f.Create(SearchEngineType.Bing));
            webScraper.Verify(w => w.ExtractUrls(emptyHtmlResultToReturn), Times.Exactly(1));
            searchEngineDataService.Verify(w => w.GetHtmlContentAsync(searchEnginePage1ToScrape), Times.Exactly(1));

            Assert.IsType<List<SearchResult>>(result);
            Assert.IsAssignableFrom<List<SearchResult>>(result);
            Assert.Equal(expectedSearchResultCount, result.Count);
        }

        [Fact]
        public async Task ShouldReturn5SearchResults()
        {
            //arrange
            var url = "http://wwww.bla.com.au";

            var baseUrl = "https://wwww.bing.com.au";
            var title = "online title search";

            var searchEngineDataService = new Mock<ISearchEngineDataService>();
            var expectedSearchResultCount = 5;

            searchEngineDataService.Setup(s => s.GetHtmlContentAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(It.IsAny<string>()));

            var webScraper = new Mock<ISearchResultParser>();
            var webScraperFactory = new Mock<ISearchResultParserFactory>();
            webScraperFactory.Setup(s => s.Create(SearchEngineType.Bing)).Returns(webScraper.Object);
            webScraper.Setup(s => s.ExtractUrls(It.IsAny<string>()))
                .Returns(new List<string> { url });

            var sut = new BingSearchEngine(searchEngineDataService.Object, webScraperFactory.Object, baseUrl);

            //act
            var result = await sut.GetSearchResultsAsync(title);

            //assert
            webScraper.Verify(w => w.ExtractUrls(It.IsAny<string>()), Times.Exactly(expectedSearchResultCount));
            searchEngineDataService.Verify(w => w.GetHtmlContentAsync(It.IsAny<string>()), Times.Exactly(expectedSearchResultCount));

            Assert.IsType<List<SearchResult>>(result);
            Assert.IsAssignableFrom<List<SearchResult>>(result);
            Assert.Equal(expectedSearchResultCount, result.Count);
        }

    }
}