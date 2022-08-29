namespace Vulns.Core;
public class ProductReference
{
    public string Id { get; set; }
    public ProductReferenceTag Tag { get; set; } = ProductReferenceTag.Unspecified;
    public string Url { get; set; } = string.Empty;

    public ProductReference() => Id = String.Empty;
    public ProductReference(String id, ProductReferenceTag tag, string url) => (Id, Tag, Url) = (id, tag, url);
}