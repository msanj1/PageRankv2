using System.Collections.Generic;
using System.Text.RegularExpressions;
using PageRank.Core.Interfaces;

namespace PageRank.Infrastructure.Parsers
{
    public class BingRegexParser : ISearchResultParser
    {
        public List<string> ExtractUrls(string htmlContent)
        {
            var urls = new List<string>();
            var lookup =
                "(<h2><a href=\\\")((https?:\\/\\/(?:www\\.|(?!www[d])|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\\.[^\\s]{2,}|www\\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\\.[^\\s]{2,}|https?:\\/\\/(?:www\\.|(?!www[d])|(?!www))[a-zA-Z0-9]+\\.[^\\s]{2,}|www\\.[a-zA-Z0-9]+\\.[^\\s]{2,}))";
            var matches = Regex.Matches(htmlContent,
                lookup);

            for (var b = 0;
                b < matches.Count;
                b++)
            {
                var match = matches[b].Groups[2].Value;
                urls.Add(match);
            }

            return urls;
        }
    }
}