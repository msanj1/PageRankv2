using System.Collections.Generic;
using PageRank.Infrastructure.Parsers;
using PageRank.UnitTests.Helpers;
using Xunit;

namespace PageRank.UnitTests
{
    public class BingHapParserUnitTests
    {
        private readonly string _emptyHtmlPage;
        private readonly string _fakeBingResultsPageContent;

        public BingHapParserUnitTests()
        {
            _fakeBingResultsPageContent = Utilities.GetFakeBingFileContent();
            _emptyHtmlPage = Utilities.GetEmptyHtmlPage();
        }

        [Theory]
        [InlineData(
            "https://www.business.qld.gov.au/industries/building-property-development/titles-property-surveying/titles-property/searches-copies#:~:text=You%20can%20buy%20title%20searches%20and%20copies%20of,255%20750%20or%2013%20QGOV%20%2813%2074%2068%29.")]
        [InlineData("https://www.sa.gov.au/topics/planning-and-property/certificates-of-title")]
        public void ShouldExtractExpectedUrlsWhenThereAreResults(string expectedUrl)
        {
            //Arrange
            var hapParser = new HapParser();
            var sut = new BingHapParser(hapParser);

            //Act
            var result = sut.ExtractUrls(_fakeBingResultsPageContent);

            //Assert
            Assert.IsType<List<string>>(result);
            Assert.Contains(result, r => r == expectedUrl);
        }

        [Fact]
        public void ShouldExtract8UrlsWhenThereAreResults()
        {
            //Arrange
            var hapParser = new HapParser();
            var sut = new BingHapParser(hapParser);
            var expectedUrlCount = 8;

            //Act
            var result = sut.ExtractUrls(_fakeBingResultsPageContent);

            //Assert
            Assert.IsType<List<string>>(result);
            Assert.Equal(expectedUrlCount, result.Count);
        }

        [Fact]
        public void ShouldExtract0UrlsWhenThereAreNoResults()
        {
            //Arrange
            var hapParser = new HapParser();
            var sut = new BingHapParser(hapParser);
            var expectedUrlCount = 0;

            //Act
            var result = sut.ExtractUrls(_emptyHtmlPage);

            //Assert
            Assert.IsType<List<string>>(result);
            Assert.Equal(expectedUrlCount, result.Count);
        }
    }
}