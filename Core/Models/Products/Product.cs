namespace Vulns.Core;

public class Product : Entity
{
    public override string Id { get; set; } = string.Empty;
    public override DateTime CreatedAt { get; set; }
    public override DateTime? ModifiedAt { get; set; }
    public string? Title { get; set; }
    public ProductUri Uri { get; set; }
    public ICollection<Vulnerability> Vulnerabilities { get; set; } = new List<Vulnerability>();
    public virtual ICollection<ProductReference> References { get; set; }

    public Product() => (References, Uri, ModifiedAt) = (new List<ProductReference>(), new(), DateTime.UtcNow);
    public Product(string cpeUri) => (Id, References, Uri) = (cpeUri, new List<ProductReference>(), new(cpeUri));
}