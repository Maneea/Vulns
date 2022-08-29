using Nest;
namespace Vulns.Infrastructure;
[ElasticsearchType(RelationName = "product")]
public class ProductDocument
{
    public string? Title { get; set; }
    [Keyword]
    public string? Id { get; set; }
    [Keyword]
    public string? ProductType { get; set; }
    [Keyword]
    public string? Vendor { get; set; }
    [Keyword]
    public string? Product { get; set; }
    [Version]
    public string? Version { get; set; }
    [Keyword]
    public string? Update { get; set; }
    [Keyword]
    public string? Edition { get; set; }
    [Keyword]
    public string? SoftwareEdition { get; set; }
    [Keyword]
    public string? TargetSoftware { get; set; }
    [Keyword]
    public string? TargetHardware { get; set; }
    [Keyword]
    public string? Language { get; set; }
    [Keyword]
    public string? Other { get; set; }
}
