using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Moq;
using PageRank.Core.Interfaces;
using PageRank.Core.Models;

namespace PageRank.UnitTests.Helpers
{
    public static class Utilities
    {
        public static string GetFakeGoogleFileContent()
        {
            return File.ReadAllText("./Resources/FakeGooglePage.html");
        }

        public static string GetFakeBingFileContent()
        {
            return File.ReadAllText("./Resources/FakeBingPage.html");
        }

        public static string GetEmptyHtmlPage()
        {
            return File.ReadAllText("./Resources/EmptyHtmlPage.html");
        }

        public static List<SearchResult> Get3FakeResultCollections()
        {
            var output = new List<SearchResult>();
            var searchResult1 = "https://www0.site.wa.gov.au/property-reports/single-address-report/property-interest-reports";
            var searchResult2 = "http://www.site.com.au/some-page";
            var searchResult3 = "https://www.citylegal.com.au/nsw-land-titles/";

            var searchResult4 = "https://info.australia.gov.au/information-and-services/family-and-community/housing-and-property/land-titles";
            var searchResult5 = "http://www.site.com.au/another-page";
            var searchResult6 = "https://www.website.com.au/solutions/searches-certificates/";

            var searchResult7 = "https://docs.microsoft.com/en-us/sharepoint/manage-search-schema";
            var searchResult8 = "https://searchfirst.com.au/";
            var searchResult9 = "http://www.site.com.au/another-page";

            var searchResults1 = new SearchResult(1);
            searchResults1.Urls.Add(searchResult1);
            searchResults1.Urls.Add(searchResult2);
            searchResults1.Urls.Add(searchResult3);

            var searchResults2 = new SearchResult(2);
            searchResults2.Urls.Add(searchResult4);
            searchResults2.Urls.Add(searchResult5);
            searchResults2.Urls.Add(searchResult6);

            var searchResults3 = new SearchResult(3);
            searchResults3.Urls.Add(searchResult7);
            searchResults3.Urls.Add(searchResult8);
            searchResults3.Urls.Add(searchResult9);

            output.Add(searchResults1);
            output.Add(searchResults2);
            output.Add(searchResults3);
            return output;
        }

        public static Mock<ISearchEngine> GetMockSearchEngine(string title, List<SearchResult> searchResults)
        {
            var searchEngine = new Mock<ISearchEngine>();
            searchEngine.Setup(s => s.GetSearchResultsAsync(title)).Returns(Task.FromResult(searchResults));

            return searchEngine;
        }
    }
}
