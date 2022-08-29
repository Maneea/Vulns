namespace Vulns.Jobs.Vulnerabilities;
/// <summary>
/// CPE match string or range
/// </summary>
public partial record NvdCveCpeMatch
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdCveCpeMatch(
        string cpe22Uri,
        string cpe23Uri,
        string versionEndExcluding,
        string versionEndIncluding,
        string versionStartExcluding,
        string versionStartIncluding,
        bool vulnerable)
    {
        this.Vulnerable = vulnerable;
        this.Cpe22Uri = cpe22Uri;
        this.Cpe23Uri = cpe23Uri;
        this.VersionStartExcluding = versionStartExcluding;
        this.VersionStartIncluding = versionStartIncluding;
        this.VersionEndExcluding = versionEndExcluding;
        this.VersionEndIncluding = versionEndIncluding;
    }

    [Newtonsoft.Json.JsonProperty("vulnerable", Required = Newtonsoft.Json.Required.Always)]
    public bool Vulnerable { get; init; }

    [Newtonsoft.Json.JsonProperty("cpe22Uri", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string Cpe22Uri { get; init; }

    [Newtonsoft.Json.JsonProperty("cpe23Uri", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    public string Cpe23Uri { get; init; }

    [Newtonsoft.Json.JsonProperty("versionStartExcluding", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string VersionStartExcluding { get; init; }

    [Newtonsoft.Json.JsonProperty("versionStartIncluding", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string VersionStartIncluding { get; init; }

    [Newtonsoft.Json.JsonProperty("versionEndExcluding", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string VersionEndExcluding { get; init; }

    [Newtonsoft.Json.JsonProperty("versionEndIncluding", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string VersionEndIncluding { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }
}
