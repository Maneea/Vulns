using Nest;
namespace Vulns.Infrastructure;

[ElasticsearchType(RelationName = "weakness")]
public class WeaknessDocument
{
    [Keyword]
    public String? Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    [Text]
    public String? Name { get; set; }
    [Text]
    public String? Description { get; set; }
    [Keyword]
    public String? Type { get; set; }
    [Keyword]
    public String? ExploitationLikelihood { get; set; }
    [Keyword]
    public ICollection<String> AffectedResources { get; set; } = new List<string>();
    [Keyword]
    public ICollection<String> Platforms { get; set; } = new List<string>();
    [Nested]
    public ICollection<WeaknessConsequenceDocument>? Consequences { get; set; }
    [Nested]
    public ICollection<WeaknessDetectionDocument>? DetectionMethods { get; set; }
    [Keyword]
    public String? DirectParent { get; set; }
    [Keyword]
    public ICollection<String>? AllParents { get; set; }
    [Keyword]
    public ICollection<String>? DirectChildren { get; set; }
    [Keyword]
    public ICollection<String>? AllChildren { get; set; }
}