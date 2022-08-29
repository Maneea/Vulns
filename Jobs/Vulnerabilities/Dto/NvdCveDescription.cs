namespace Vulns.Jobs.Vulnerabilities;
public partial record NvdCveDescription
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdCveDescription(IList<NvdCveLanguageString> descriptionData)
    {
        this.Data = descriptionData;
    }

    [Newtonsoft.Json.JsonProperty("description_data", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required]
    public IList<NvdCveLanguageString> Data { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }
}
