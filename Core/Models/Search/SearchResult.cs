namespace Vulns.Core;
public class SearchResult<T> where T : class
{
    public long Total { get; set; }
    public IEnumerable<T> Results { get; set; }
    public SearchResult(IEnumerable<T> results, long total) => (Results, Total) = (results, total);
}