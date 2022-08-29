using System.Text.RegularExpressions;

namespace Vulns.Core;

public class ProductUri
{
    public ProductUriType ProductType { get; set; }
    public string? Vendor { get; set; }
    public string? Product { get; set; }
    public string? Version { get; set; }
    public string? Update { get; set; }
    public string? Edition { get; set; }
    public string? SoftwareEdition { get; set; }
    public string? TargetSoftware { get; set; }
    public string? TargetHardware { get; set; }
    public string? Language { get; set; }
    public string? Other { get; set; }

    public string? FormattedVendor { get; set; }
    public string? FormattedProduct { get; set; }
    public string? FormattedVersion { get; set; }
    public string? FormattedUpdate { get; set; }
    public string? FormattedEdition { get; set; }
    public string? FormattedSoftwareEdition { get; set; }
    public string? FormattedTargetSoftware { get; set; }
    public string? FormattedTargetHardware { get; set; }
    public string? FormattedLanguage { get; set; }
    public string? FormattedOther { get; set; }

    private static readonly Regex _cpeUnquoteRegex = new Regex(@"\\(.)");
    private static readonly Regex _cpeSpaceRestorationRegex = new Regex(@"(?<=[^\\])_");

    public ProductUri() { }
    public ProductUri(string uri)
    {
        // Validate and assert that the cpeFormat is correct or return null
        // if (!Regex.IsMatch(cpeUri, @"^cpe:2.3:[aho](:(\\:|[^:])+){10}$"))
        // return;
        var truncatedCpeUri = uri;
        string schema, schemaVersion, part;
        var extractFirstAttributeAndTruncateIt = (string uri) =>
        {
            var match = Regex.Match(uri, @"^(\\:|[^:])+");
            return match.Success ?
                (match.Value, uri.Substring(Math.Min(match.Length + 1, truncatedCpeUri.Length))) :
                (string.Empty, string.Empty);
        };

        (schema, truncatedCpeUri) = extractFirstAttributeAndTruncateIt(truncatedCpeUri);
        (schemaVersion, truncatedCpeUri) = extractFirstAttributeAndTruncateIt(truncatedCpeUri);
        (part, truncatedCpeUri) = extractFirstAttributeAndTruncateIt(truncatedCpeUri);
        (Vendor, truncatedCpeUri) = extractFirstAttributeAndTruncateIt(truncatedCpeUri);
        (Product, truncatedCpeUri) = extractFirstAttributeAndTruncateIt(truncatedCpeUri);
        (Version, truncatedCpeUri) = extractFirstAttributeAndTruncateIt(truncatedCpeUri);
        (Update, truncatedCpeUri) = extractFirstAttributeAndTruncateIt(truncatedCpeUri);
        (Edition, truncatedCpeUri) = extractFirstAttributeAndTruncateIt(truncatedCpeUri);
        (Language, truncatedCpeUri) = extractFirstAttributeAndTruncateIt(truncatedCpeUri);
        (SoftwareEdition, truncatedCpeUri) = extractFirstAttributeAndTruncateIt(truncatedCpeUri);
        (TargetSoftware, truncatedCpeUri) = extractFirstAttributeAndTruncateIt(truncatedCpeUri);
        (TargetHardware, truncatedCpeUri) = extractFirstAttributeAndTruncateIt(truncatedCpeUri);
        (Other, truncatedCpeUri) = extractFirstAttributeAndTruncateIt(truncatedCpeUri);
        switch (part.ElementAt(0))
        {
            case 'a': ProductType = ProductUriType.Software; break;
            case 'h': ProductType = ProductUriType.Hardware; break;
            case 'o': ProductType = ProductUriType.OS; break;
        }

        ApplyFormatting(string.Empty);
    }

    public void ApplyFormatting(string title)
    {
        FormattedVendor = Unquote(Vendor).HumanizeFrom(title);
        FormattedProduct = Unquote(Product).HumanizeFrom(title);
        FormattedVersion = Unquote(Version).HumanizeFrom(title);
        FormattedUpdate = Unquote(Update).HumanizeFrom(title);
        FormattedEdition = Unquote(Edition).HumanizeFrom(title);
        FormattedSoftwareEdition = Unquote(SoftwareEdition).HumanizeFrom(title);
        FormattedTargetSoftware = Unquote(TargetSoftware).HumanizeFrom(title);
        FormattedTargetHardware = Unquote(TargetHardware).HumanizeFrom(title);
        FormattedLanguage = Unquote(Language).HumanizeFrom(title);
        FormattedOther = Unquote(Other).HumanizeFrom(title);
    }

    private string Unquote(string? quoted)
    {
        if (string.IsNullOrEmpty(quoted)) return ProductUriLogicalValues.Any;
        else if (quoted == ProductUriLogicalValues.Any) return nameof(ProductUriLogicalValues.Any);
        else if (quoted == ProductUriLogicalValues.NotApplicable) return nameof(ProductUriLogicalValues.NotApplicable).Humanize();
        else return _cpeUnquoteRegex.Replace(_cpeSpaceRestorationRegex.Replace(quoted, " "), @"$1");
    }
}

public enum ProductUriType
{
    Undefined,
    Hardware,
    Software,
    OS
}

public static class ProductUriLogicalValues
{
    public static readonly string Any = "*";
    public static readonly string NotApplicable = "-";
}