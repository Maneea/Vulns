using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Vulns.Core;
using Vulns.Jobs.Base;

namespace Vulns.Jobs.Weaknesses;
public class WeaknessAttributePropagationEnricher : IEnricher<Weakness>
{
    private readonly ILogger<WeaknessAttributePropagationEnricher> _logger;

    public int ExecutionOrder { get => 1; }

    public string Name => nameof(WeaknessAttributePropagationEnricher);

    public WeaknessAttributePropagationEnricher(ILogger<WeaknessAttributePropagationEnricher> logger) => _logger = logger;

    public IEnumerable<Weakness> Enrich(IEnumerable<Weakness> weaknesses, IServiceScope scope)
    {
        _logger.LogInformation($"Enriching with {nameof(WeaknessAttributePropagationEnricher)}.{nameof(Enrich)}");
        foreach (var weakness in weaknesses.Where(_ => _.Parent == null))
            Propagator(weakness);
        return weaknesses;
    }

    private void Propagator(Weakness weakness)
    {
        foreach (var child in weakness.Children)
        {
            PropagateAffectedResources(weakness, child);
            PropagatePlatforms(weakness, child);
            Propagator(child);
        }
    }

    private void PropagateAffectedResources(Weakness parent, Weakness child)
    {
        foreach (var affectedResource in parent.AffectedResources)
            if (!child.AffectedResources.Contains(affectedResource))
                child.AffectedResources.Add(affectedResource);
    }

    private void PropagatePlatforms(Weakness parent, Weakness child)
    {
        var childPlatformsList = child.Platforms.ToList();
        foreach (var platform in parent.Platforms)
            if (NonIndependentValidName(platform.Name) && !childPlatformsList.Exists(p => p.Name == platform.Name))
                child.Platforms.Add(new()
                {
                    Id = $"{child.Id}-{platform.Name}",
                    Type = platform.Type,
                    Name = platform.Name,
                    Prelavence = platform.Prelavence
                });
    }

    private bool NonIndependentValidName(string? name)
    {
        return !string.IsNullOrEmpty(name) && !name.EndsWith("Independent");
    }
}