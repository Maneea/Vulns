namespace Vulns.Web;
public record WeaknessConsequence(ICollection<string> Scope, string Likelihood, ICollection<string> Impact, string? Note);