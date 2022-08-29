using Vulns.Core;
namespace Vulns.App;
public interface ISearchService<TEntity> where TEntity : Entity
{
    public Task<IEnumerable<TEntity>> TypeaheadAsync(string? phrase, int size, CancellationToken token);
}