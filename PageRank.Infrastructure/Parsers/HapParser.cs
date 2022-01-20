using System.Collections.Generic;
using HtmlAgilityPack;

namespace PageRank.Infrastructure.Parsers
{
    public class HapParser
    {
        public List<string> ExtractSearchResultUrls(string htmlContent, string xPath)
        {
            var output = new List<string>();
            var googleDoc = new HtmlDocument();
            var xPathCommand = xPath;
            googleDoc.LoadHtml(htmlContent);
            var anchorTags = googleDoc.DocumentNode.SelectNodes(xPathCommand);
            if (anchorTags != null)
                foreach (var anchorTag in anchorTags)
                {
                    var url = anchorTag.GetAttributeValue("href", string.Empty);
                    if (!string.IsNullOrEmpty(url)) output.Add(url);
                }

            return output;
        }
    }
}