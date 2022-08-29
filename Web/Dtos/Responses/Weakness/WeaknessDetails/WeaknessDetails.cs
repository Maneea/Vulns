namespace Vulns.Web;
public class WeaknessDetails
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Type { get; set; } = DtoConstants.Unspecified;
    public string ExploitationLikelihood { get; set; } = DtoConstants.NotApplicable;
    public ICollection<string>? AffectedResources { get; set; }
    public ICollection<WeaknessPlatform>? Platforms { get; set; }
    public ICollection<WeaknessConsequence> Consequences { get; set; } = new List<WeaknessConsequence>();
    public ICollection<WeaknessDetection> DetectionMethods { get; set; } = new List<WeaknessDetection>();
    public WeaknessFragment? Parent { get; set; }
    public ICollection<WeaknessFragment>? Children { get; set; }
}