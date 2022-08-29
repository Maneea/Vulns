using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Nest;

using Vulns.Core;

namespace Vulns.Infrastructure;

public class Repository<T> : Core.IRepository<T> where T : Entity
{
    protected readonly IElasticClient elastic;
    protected readonly AppDbContext context;

    public Repository(IElasticClient elastic, AppDbContext context)
        => (this.elastic, this.context) = (elastic, context);

    public virtual async Task AddManyAsync(IEnumerable<T> entities, CancellationToken token)
    {
        if (entities == null || !entities.Any()) return;
        foreach (var entity in entities)
            if (entity.CreatedAt == default) entity.CreatedAt = entity.ModifiedAt ?? DateTime.UtcNow;
        await context.AddRangeAsync(entities, token);
        await context.SaveChangesAsync(token);
        foreach (var entity in entities)
            context.Entry(entity).State = EntityState.Detached;
    }

    public async Task<int> CountAsync(CancellationToken token)
        => await context.Set<T>().CountAsync(token);

    public async virtual Task<T?> GetAsync(string id, CancellationToken token)
        => await context.Set<T>().Where(_ => _.Id == id).FirstOrDefaultAsync(token);

    public virtual async Task<IEnumerable<string>> GetExistingValuesByIdAsync(IEnumerable<string> ids, CancellationToken token)
        => await context.Set<T>().Where(_ => ids.Contains(_.Id)).Select(_ => _.Id).ToArrayAsync(token);

    public async Task<bool> IsEmptyAsync(CancellationToken token)
        => !await context.Set<T>().AnyAsync(token);

    public async Task<DateTime> LastTimestampAsync(CancellationToken token)
        => await context.Set<T>().SortedPaginate(0, 1, _ => _.ModifiedAt ?? _.CreatedAt).Select(_ => _.ModifiedAt ?? _.CreatedAt).FirstAsync(token);

    public virtual async Task UpdateManyAsync(IEnumerable<T> entities, CancellationToken token)
    {
        if (entities == null || !entities.Any()) return;
        context.Set<T>().AttachRange(entities);
        context.Set<T>().UpdateRange(entities);
        await context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<T>> GetManyAsync(IEnumerable<string> ids, CancellationToken token)
    {
        var queryable = context.Set<T>().AsQueryable();
        return await queryable.Where(_ => ids.Contains(_.Id)).ToArrayAsync(token);
    }

    public async Task<IEnumerable<T>> GetLatestAsync<TKey>(int offset, int size, Expression<Func<T, TKey>> sortBy, CancellationToken token = default)
        => await context.Set<T>().AsNoTracking().SortedPaginate(offset, size, sortBy).ToListAsync(token);
}