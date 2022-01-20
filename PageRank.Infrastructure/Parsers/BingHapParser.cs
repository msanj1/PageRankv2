using System;
using System.Collections.Generic;
using PageRank.Core.Interfaces;

namespace PageRank.Infrastructure.Parsers
{
    public class BingHapParser : ISearchResultParser
    {
        private readonly HapParser _parser;

        public BingHapParser(HapParser parser)
        {
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
        }

        public List<string> ExtractUrls(string htmlContent)
        {
            return _parser.ExtractSearchResultUrls(htmlContent, "//*[@class='b_algo']//h2/a[@href]");
        }
    }
}