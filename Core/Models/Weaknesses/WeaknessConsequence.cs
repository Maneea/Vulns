namespace Vulns.Core;
public class WeaknessConsequence
{
    public string? Id { get; set; }
    public ICollection<WeaknessConsequenceScope> Scope { get; set; } = new List<WeaknessConsequenceScope>() ;
    public ICollection<WeaknessConsequenceImpact> Impact { get; set; } = new List<WeaknessConsequenceImpact>();
    public WeaknessConsequenceLikelihood Likelihood { get; set; } = WeaknessConsequenceLikelihood.Unspecified;
    public String? Note { get; set; }
}