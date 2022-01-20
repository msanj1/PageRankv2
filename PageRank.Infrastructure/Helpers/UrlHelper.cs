using System;
using PageRank.Core.Interfaces;

namespace PageRank.Infrastructure.Helpers
{
    public class UrlHelper : IUrlHelper
    {
        public string GetHostName(string url)
        {
            return new Uri(url).Host;
        }
    }
}