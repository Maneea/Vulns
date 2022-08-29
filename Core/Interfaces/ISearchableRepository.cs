namespace Vulns.Core;
public interface ISearchableRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    // TODO: Add parameters using the Expressions API, and make generic result objects
    // public Task<object> SearchAsync(object searchQuery, CancellationToken token);
    public Task<SearchResult<TEntity>> TypeaheadAsync(string? phrase, int size, CancellationToken token);
    // public Task<object> AggregateAsync(object aggs, object? query, CancellationToken token);
}