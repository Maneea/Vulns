namespace Vulns.App;
public class ProductSearchCriteria : SearchCriteria
{
    public String? ProductType { get; set; }
    public String? Vendor { get; set; }
    public String? Product { get; set; }
    public String? Version { get; set; }

    public bool HasQuery()
        => !string.IsNullOrEmpty(ProductType) ||
        !string.IsNullOrEmpty(Vendor) ||
        !string.IsNullOrEmpty(Product) ||
        !string.IsNullOrEmpty(Version);
}