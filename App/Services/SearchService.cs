using Vulns.Core;

namespace Vulns.App;

public class SearchService<TEntity> where TEntity : Entity
{
    private readonly ISearchableRepository<TEntity> _repo;
    public SearchService(ISearchableRepository<TEntity> repo) => _repo = repo;

    public async Task<TEntity?> GetById(string id, CancellationToken token) => await _repo.GetAsync(id, token);

    public async Task<SearchResult<TEntity>> TypeaheadAsync(string? phrase, int size, CancellationToken token)
        => await _repo.TypeaheadAsync(phrase, size, token);

    public async Task<IEnumerable<TEntity>> GetLatestAsync(int offset, int size, SortFields sortField, CancellationToken token = default)
    {
        switch (sortField)
        {
            case SortFields.PublishDate: return await _repo.GetLatestAsync(offset, size, _ => _.CreatedAt, token);
            case SortFields.ModificationDate: return await _repo.GetLatestAsync(offset, size, _ => _.ModifiedAt, token);
            default: return await _repo.GetLatestAsync(offset, size, _ => _.ModifiedAt ?? _.CreatedAt, token);

        }
    }
}