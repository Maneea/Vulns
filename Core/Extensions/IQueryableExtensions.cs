using System.Linq.Expressions;

namespace Vulns.Core;
public static class IQueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, int offset, int size)
        => queryable.Skip(offset).Take(size);

    public static IQueryable<T> SortedPaginate<T, TKey>(
        this IQueryable<T> queryable,
        int offset,
        int size,
        Expression<Func<T, TKey>> sortBy)
        => queryable.OrderByDescending(sortBy).Paginate(offset, size);


}