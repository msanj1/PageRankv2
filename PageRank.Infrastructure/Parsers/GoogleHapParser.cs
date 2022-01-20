using System;
using System.Collections.Generic;
using PageRank.Core.Interfaces;

namespace PageRank.Infrastructure.Parsers
{
    public class GoogleHapParser : ISearchResultParser
    {
        private readonly HapParser _parser;

        public GoogleHapParser(HapParser parser)
        {
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
        }

        public List<string> ExtractUrls(string htmlContent)
        {
            return _parser.ExtractSearchResultUrls(htmlContent, "//div[@class='g']//div[@class='r']/a[@href]");
        }
    }
}