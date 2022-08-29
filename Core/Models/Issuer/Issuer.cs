namespace Vulns.Core;

public class Issuer : Entity
{
    public override string Id { get; set; } = string.Empty;
    public override DateTime CreatedAt { get; set; }
    public override DateTime? ModifiedAt { get; set; }
    public String Organization { get; set; } = null!;
    public String ShortName { get; set; } = null!;
    public String Scope { get; set; } = null!;
    public String? Email { get; set; }
    public String Country { get; set; } = null!;
    public ICollection<Vulnerability> PublishedVulnerabilities {get; set;} = null!;
}