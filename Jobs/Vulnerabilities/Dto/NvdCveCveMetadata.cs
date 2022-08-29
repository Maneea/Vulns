namespace Vulns.Jobs.Vulnerabilities;
public partial record NvdCveCveMetadata
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdCveCveMetadata(string assigner, string id, string state)
    {
        this.Id = id;
        this.Assigner = assigner;
        this.State = state;
    }

    [Newtonsoft.Json.JsonProperty("ID", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^CVE-[0-9]{4}-[0-9]{4,}$")]
    public string Id { get; init; }

    [Newtonsoft.Json.JsonProperty("ASSIGNER", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$")]
    public string Assigner { get; init; }

    [Newtonsoft.Json.JsonProperty("STATE", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string State { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }
}
