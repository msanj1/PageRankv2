using System.Collections.Generic;
using PageRank.Infrastructure.Parsers;
using PageRank.UnitTests.Helpers;
using Xunit;

namespace PageRank.UnitTests
{
    public class GoogleHapParserUnitTests
    {
        private readonly string _emptyHtmlPage;
        private readonly string _fakeGoogleResultsPageContent;

        public GoogleHapParserUnitTests()
        {
            _fakeGoogleResultsPageContent = Utilities.GetFakeGoogleFileContent();
            _emptyHtmlPage = Utilities.GetEmptyHtmlPage();
        }

        [Theory]
        [InlineData("http://cat.lib.unimelb.edu.au/search/y")]
        [InlineData("https://www0.landgate.wa.gov.au/property-reports/single-address-report/property-interest-reports")]
        public void ShouldExtractExpectedUrlsWhenThereAreResults(string expectedUrl)
        {
            //Arrange
            var hapParser = new HapParser();
            var sut = new GoogleHapParser(hapParser);

            //Act
            var result = sut.ExtractUrls(_fakeGoogleResultsPageContent);

            //Assert
            Assert.IsType<List<string>>(result);
            Assert.Contains(result, r => r == expectedUrl);
        }

        [Fact]
        public void ShouldExtract10UrlsWhenThereAreResults()
        {
            //Arrange
            var hapParser = new HapParser();
            var sut = new GoogleHapParser(hapParser);
            var expectedUrlCount = 10;

            //Act
            var result = sut.ExtractUrls(_fakeGoogleResultsPageContent);

            //Assert
            Assert.IsType<List<string>>(result);
            Assert.Equal(expectedUrlCount, result.Count);
        }

        [Fact]
        public void ShouldExtract0UrlsWhenThereAreNoResults()
        {
            //Arrange
            var hapParser = new HapParser();
            var sut = new GoogleHapParser(hapParser);
            var expectedUrlCount = 0;

            //Act
            var result = sut.ExtractUrls(_emptyHtmlPage);

            //Assert
            Assert.IsType<List<string>>(result);
            Assert.Equal(expectedUrlCount, result.Count);
        }
    }
}