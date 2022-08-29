using System.Linq.Expressions;

namespace Vulns.Core;
public interface IRepository<T> where T : Entity
{
    public Task AddManyAsync(IEnumerable<T> entity, CancellationToken token);
    public Task<int> CountAsync(CancellationToken token);
    public Task<bool> IsEmptyAsync(CancellationToken token);
    public Task<DateTime> LastTimestampAsync(CancellationToken token);
    public Task<IEnumerable<string>> GetExistingValuesByIdAsync(IEnumerable<string> ids, CancellationToken token);
    public Task<T?> GetAsync(string id, CancellationToken token);
    public Task<IEnumerable<T>> GetManyAsync(IEnumerable<string> ids, CancellationToken token);
    public Task UpdateManyAsync(IEnumerable<T> entities, CancellationToken token);
    public Task<IEnumerable<T>> GetLatestAsync<TKey>(int offset, int size, Expression<Func<T, TKey>> sortBy, CancellationToken token);
}