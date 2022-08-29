namespace Vulns.Core;
public class Weakness : Entity
{
    public override string Id { get; set; } = string.Empty;
    public override DateTime CreatedAt { get; set; }
    public override DateTime? ModifiedAt { get; set; }
    public string Name { get; set; } = null!;
    public String? Description { get; set; }
    public WeaknessType Type { get; set; } = WeaknessType.Undefined;
    public WeaknessExploitationLikelihood ExploitationLikelihood { get; set; } = WeaknessExploitationLikelihood.NotApplicable;
    public ICollection<WeaknessAffectedResources> AffectedResources { get; set; } = new List<WeaknessAffectedResources>();
    public ICollection<WeaknessPlatform> Platforms { get; set; } = new List<WeaknessPlatform>();
    public ICollection<WeaknessConsequence> Consequences { get; set; } = new List<WeaknessConsequence>();
    public ICollection<WeaknessDetection> DetectionMethods { get; set; } = new List<WeaknessDetection>();
    public Weakness? Parent { get; set; }
    public ICollection<Weakness> Children { get; set; } = new List<Weakness>();
    public ICollection<Vulnerability>? Vulnerabilities { get; set; }

    public IEnumerable<Weakness> AllParents()
        => Parent != null ? new List<Weakness>() { Parent }.Concat(Parent.AllParents()) : new List<Weakness>();

    public IEnumerable<Weakness> AllChildren()
    {
        var children = new List<Weakness>();
        foreach (var child in Children)
        {
            children.Add(child);
            children = children.Concat(child.AllChildren()).ToList();
        }
        return children.DistinctBy(cwe => cwe.Id);
    }
}