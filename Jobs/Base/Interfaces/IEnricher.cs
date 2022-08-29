using Microsoft.Extensions.DependencyInjection;

namespace Vulns.Jobs.Base;
public interface IEnricher<T> where T : Core.Entity
{
    public IEnumerable<T> Enrich(IEnumerable<T> entities, IServiceScope scope);
    public abstract int ExecutionOrder { get; }
    public abstract string Name { get; }
}