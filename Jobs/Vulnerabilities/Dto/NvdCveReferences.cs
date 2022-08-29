namespace Vulns.Jobs.Vulnerabilities;
public partial record NvdCveReferences
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdCveReferences(IList<NvdCveReference> references)
    {
        this.References = references;
    }

    [Newtonsoft.Json.JsonProperty("reference_data", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required]
    [System.ComponentModel.DataAnnotations.MaxLength(500)]
    public IList<NvdCveReference> References { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }
}
