using Nest;
namespace Vulns.Infrastructure;

[ElasticsearchType(RelationName = "weakness-consequence")]
public class WeaknessConsequenceDocument
{
    [Keyword]
    public ICollection<String> Scope { get; set; } = new List<string>();
    [Keyword]
    public ICollection<String> Impact { get; set; } = new List<string>();
    [Keyword]
    public String? Likelihood { get; set; }
}