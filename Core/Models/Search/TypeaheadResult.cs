namespace Vulns.Core;
public class TypeaheadResult<T> where T : Entity
{
    public IEnumerable<T> Results { get; set; }
    public TypeaheadResult(IEnumerable<T> results) => Results = results;
}