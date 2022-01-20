using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using PageRank.Core.Features.Search.SearchUrlPositions;
using PageRank.Core.Interfaces;
using PageRank.Core.Models;
using PageRank.Infrastructure.Helpers;
using PageRank.UnitTests.Helpers;
using Xunit;

namespace PageRank.UnitTests
{
    public class SearchUrlPositionQueryHandlerUnitTests
    {
        [Fact]
        public async Task ShouldReturnExpectedUrlPositionsWhenUrlFound()
        {
            //Arrange
            var urlToSearch = "http://www.site.com.au";
            var titleToSearch = "some title";
            IUrlHelper urlHelper = new UrlHelper();
            var searchEngineFactory = new Mock<ISearchEngineFactory>();
            var searchEngine = Utilities.GetMockSearchEngine(titleToSearch,Utilities.Get3FakeResultCollections());
            var expectedPositions = new List<int> { 1, 2, 3 };

            searchEngineFactory.Setup(e => e.Create(It.IsAny<string>())).Returns(searchEngine.Object);

            var sut = new SearchUrlPositionQueryHandler(searchEngineFactory.Object, urlHelper);

            //act
            var result = await sut.Handle(new SearchUrlPositionsQuery(urlToSearch, "fake-engine", titleToSearch),
                It.IsAny<CancellationToken>());

            //assert
            Assert.IsType<SearchUrlPositionsResponse>(result);
            Assert.IsAssignableFrom<SearchUrlPositionsResponse>(result);
            searchEngineFactory.Verify(s => s.Create(It.IsAny<string>()));
            Assert.NotNull(result.SearchPositions);
            Assert.Equal(expectedPositions, result.SearchPositions);
        }

        [Fact]
        public async Task ShouldReturnSearchPositionZeroWhenUrlIsNotFound()
        {
            //Arrange
            var urlToSearch = "http://www.bla.com.au";
            var titleToSearch = "some title";
            IUrlHelper urlHelper = new UrlHelper();
            var searchEngineFactory = new Mock<ISearchEngineFactory>();
            var searchEngine = Utilities.GetMockSearchEngine(titleToSearch, Utilities.Get3FakeResultCollections());
            var expectedPositions = new List<int> { 0 };

            searchEngineFactory.Setup(e => e.Create(It.IsAny<string>())).Returns(searchEngine.Object);

            var sut = new SearchUrlPositionQueryHandler(searchEngineFactory.Object, urlHelper);

            //act
            var result =
                await sut.Handle(new SearchUrlPositionsQuery(urlToSearch, "fake-engine", titleToSearch), It.IsAny<CancellationToken>());

            //assert
            Assert.IsType<SearchUrlPositionsResponse>(result);
            Assert.IsAssignableFrom<SearchUrlPositionsResponse>(result);
            searchEngineFactory.Verify(s => s.Create(It.IsAny<string>()));
            Assert.NotNull(result.SearchPositions);
            Assert.Equal(expectedPositions, result.SearchPositions);
        }
    }
}