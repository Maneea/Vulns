namespace Vulns.Web;
public class PaginationSet<T> : IResponse where T : class
{
    public int Page { get; set; }
    public int Size { get => Results.Count(); }
    public long Total { get; set; }
    public IEnumerable<T> Results { get; set; }
    public DateTime Timestamp {get; set;} = DateTime.UtcNow;

    public PaginationSet(IEnumerable<T> results, int page, long total)
        => (Results, Page, Total) = (results, page, total);
}