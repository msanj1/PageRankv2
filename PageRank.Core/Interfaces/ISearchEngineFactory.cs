namespace PageRank.Core.Interfaces
{
    public interface ISearchEngineFactory
    {
        ISearchEngine Create(string type);
    }
}