namespace Vulns.Web;
public class ProductDetails
{
    public string Id { get; set; } = DtoConstants.Unspecified;
    public string Title { get; set; } = DtoConstants.Unspecified;
    public string Type { get; set; } = DtoConstants.Unspecified;
    public string Vendor { get; set; } = DtoConstants.Unspecified;
    public string Product { get; set; } = DtoConstants.Unspecified;
    public string Version { get; set; } = DtoConstants.Unspecified;
    public string Update { get; set; } = DtoConstants.Unspecified;
    public string Edition { get; set; } = DtoConstants.Unspecified;
    public string SoftwareEdition { get; set; } = DtoConstants.Unspecified;
    public string TargetSoftware { get; set; } = DtoConstants.Unspecified;
    public string TargetHardware { get; set; } = DtoConstants.Unspecified;
    public string Language { get; set; } = DtoConstants.Unspecified;
    public string Other { get; set; } = DtoConstants.Unspecified;
    public ICollection<string>? Vulnerabilities { get; set; }
    public virtual ICollection<ProductReference>? References { get; set; }
}