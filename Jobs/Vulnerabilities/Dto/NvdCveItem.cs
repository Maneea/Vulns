namespace Vulns.Jobs.Vulnerabilities;
/// <summary>
/// Defines a vulnerability in the NVD data feed.
/// </summary>
public partial record NvdCveItem
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdCveItem(
        NvdCveApplicabilityStatements configurations,
        NvdCveItemDetails cve,
        NvdCveImpact impact,
        string lastModifiedDate,
        string publishedDate)
    {
        this.Cve = cve;
        this.Configurations = configurations;
        this.Impact = impact;
        this.PublishedDate = publishedDate;
        this.LastModifiedDate = lastModifiedDate;
    }

    [Newtonsoft.Json.JsonProperty("cve", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required]
    public NvdCveItemDetails Cve { get; init; }

    [Newtonsoft.Json.JsonProperty("configurations", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public NvdCveApplicabilityStatements Configurations { get; init; }

    [Newtonsoft.Json.JsonProperty("impact", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public NvdCveImpact Impact { get; init; }

    [Newtonsoft.Json.JsonProperty("publishedDate", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string PublishedDate { get; init; }

    [Newtonsoft.Json.JsonProperty("lastModifiedDate", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string LastModifiedDate { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }
}
