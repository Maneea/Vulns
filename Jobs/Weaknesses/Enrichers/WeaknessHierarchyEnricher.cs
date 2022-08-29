using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Vulns.Core;
using Vulns.Jobs.Base;

namespace Vulns.Jobs.Weaknesses;
public class WeaknessHierarchyEnricher : IEnricher<Weakness>
{
    private readonly ILogger<WeaknessHierarchyEnricher> _logger;
    public int ExecutionOrder { get => 0; }
    public string Name => nameof(WeaknessHierarchyEnricher);

    public WeaknessHierarchyEnricher(ILogger<WeaknessHierarchyEnricher> logger) => _logger = logger;

    public IEnumerable<Weakness> Enrich(IEnumerable<Weakness> weaknesses, IServiceScope scope)
    {
        _logger.LogInformation($"Enriching with {nameof(WeaknessHierarchyEnricher)}.{nameof(Enrich)}");
        foreach (var weakness in weaknesses)
        {
            if (weakness.Parent != null && weaknesses.Where(_ => _.Id == weakness.Parent.Id).Any())
                weakness.Parent = weaknesses.Where(_ => _.Id == weakness.Parent.Id).First();

            foreach (var possibleChild in weaknesses)
                if (possibleChild.Parent != null && possibleChild.Parent.Id == weakness.Id)
                    weakness.Children.Add(possibleChild);
        }
        return weaknesses;
    }
}