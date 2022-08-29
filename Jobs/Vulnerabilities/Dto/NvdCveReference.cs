namespace Vulns.Jobs.Vulnerabilities;
public partial record NvdCveReference
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdCveReference(string name, string refsource, IList<string> tags, string url)
    {
        this.Url = url;
        this.Name = name;
        this.Refsource = refsource;
        this.Tags = tags;
    }

    [Newtonsoft.Json.JsonProperty("url", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.StringLength(500)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^(ftp|http)s?://\S+$")]
    public string Url { get; init; }

    [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string Name { get; init; }

    [Newtonsoft.Json.JsonProperty("refsource", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string Refsource { get; init; }

    [Newtonsoft.Json.JsonProperty("tags", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public IList<string> Tags { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }
}
