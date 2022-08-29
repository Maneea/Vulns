namespace Vulns.Web;
public class ErrorSet<T> : IResponse where T : class
{
    public int Count { get => Errors.Count(); }
    public IEnumerable<T> Errors { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public ErrorSet(IEnumerable<T> errors) => Errors = errors;
    public ErrorSet(T error) => Errors = new T[] { error };
}
