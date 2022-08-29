namespace Vulns.Jobs.Vulnerabilities;
public partial record NvdFeedRoot
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdFeedRoot(
        string format,
        string itemsCount,
        string timestamp,
        string dataType,
        string version,
        IList<NvdCveItem> items)
    {
        this.DataType = dataType;
        this.Format = format;
        this.Version = version;
        this.ItemsCount = itemsCount;
        this.Timestamp = timestamp;
        this.Items = items;
    }

    [Newtonsoft.Json.JsonProperty("CVE_data_type", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    public string DataType { get; init; }

    [Newtonsoft.Json.JsonProperty("CVE_data_format", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    public string Format { get; init; }

    [Newtonsoft.Json.JsonProperty("CVE_data_version", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    public string Version { get; init; }

    /// <summary>
    /// NVD adds number of CVE in this feed
    /// </summary>
    [Newtonsoft.Json.JsonProperty("CVE_data_numberOfCVEs", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string ItemsCount { get; init; }

    /// <summary>
    /// NVD adds feed date timestamp
    /// </summary>
    [Newtonsoft.Json.JsonProperty("CVE_data_timestamp", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string Timestamp { get; init; }

    /// <summary>
    /// NVD feed array of CVE
    /// </summary>
    [Newtonsoft.Json.JsonProperty("CVE_Items", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required]
    public IList<NvdCveItem> Items { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }
}
