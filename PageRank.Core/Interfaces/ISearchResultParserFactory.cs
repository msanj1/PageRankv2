namespace PageRank.Core.Interfaces
{
    public interface ISearchResultParserFactory
    {
        ISearchResultParser Create(string searchEngineType);
    }
}