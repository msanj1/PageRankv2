using System;
using Moq;
using PageRank.Core.Models;
using PageRank.Infrastructure.Factories;
using PageRank.Infrastructure.Parsers;
using Xunit;

namespace PageRank.UnitTests
{
    public class SearchResultParserFactoryUnitTests
    {
        [Fact]
        public void ShouldCreateGoogleHapParser()
        {
            //arrange
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(s => s.GetService(typeof(GoogleHapParser)))
                .Returns(new GoogleHapParser(new HapParser()));
            var sut = new SearchResultParserFactory(serviceProvider.Object);
            var engineType = SearchEngineType.Google;

            //act
            var result = sut.Create(engineType);

            //assert
            serviceProvider.Verify(s => s.GetService(typeof(GoogleHapParser)));
            Assert.IsType<GoogleHapParser>(result);
            Assert.IsAssignableFrom<GoogleHapParser>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldCreateBingHapParser()
        {
            //arrange
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(s => s.GetService(typeof(BingHapParser))).Returns(new BingHapParser(new HapParser()));
            var sut = new SearchResultParserFactory(serviceProvider.Object);
            var engineType = SearchEngineType.Bing;

            //act
            var result = sut.Create(engineType);

            //assert
            serviceProvider.Verify(s => s.GetService(typeof(BingHapParser)));
            Assert.IsType<BingHapParser>(result);
            Assert.IsAssignableFrom<BingHapParser>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldThrowExceptionWhenEngineTypeIsInvalid()
        {
            //arrange
            var serviceProvider = new Mock<IServiceProvider>();
            var sut = new SearchResultParserFactory(serviceProvider.Object);
            var engineType = "InvalidEngine";

            //act

            //assert
            Assert.Throws<Exception>(() => sut.Create(engineType));
        }
    }
}