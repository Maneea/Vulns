namespace Vulns.Jobs.Base;
public abstract class JobConfiguration<T> where T : Core.Entity
{
    public bool SupportsPartialFetching { get => PartialFetchingParts != null && PartialFetchingParts.Any(); }
    public virtual TimeSpan UpdateInterval { get; set; } = TimeSpan.FromHours(4);
    public virtual int MaxEntitiesPerTransaction { get; set; } = 10_000;
    public virtual IEnumerable<object>? PartialFetchingParts { get; set; }
    public abstract string Name { get; set; }
}