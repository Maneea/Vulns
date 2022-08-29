using System.Text.Json.Serialization;

namespace Vulns.Core;
[JsonConverter(typeof(SmartEnumNameConverter<ProductReferenceTag, int>))]
public class ProductReferenceTag : SmartEnum<ProductReferenceTag>
{
    public static readonly ProductReferenceTag Unspecified = new(nameof(Unspecified), 0);
    public static readonly ProductReferenceTag Advisory = new(nameof(Advisory), 1);
    public static readonly ProductReferenceTag ChangeLog = new("Change Log", 2);
    public static readonly ProductReferenceTag Version = new(nameof(Version), 3);
    public static readonly ProductReferenceTag Product = new(nameof(Product), 4);
    public static readonly ProductReferenceTag Project = new(nameof(Project), 5);
    public static readonly ProductReferenceTag Vendor = new(nameof(Vendor), 6);

    public ProductReferenceTag(string name, int value) : base(name, value) { }
    public ProductReferenceTag() : base(nameof(Unspecified), 0) { }
}