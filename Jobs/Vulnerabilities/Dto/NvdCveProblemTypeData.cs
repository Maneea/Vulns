namespace Vulns.Jobs.Vulnerabilities;
public partial record NvdCveProblemTypeData
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdCveProblemTypeData(IList<NvdCveLanguageString> description)
    {
        this.Description = description;
    }

    [Newtonsoft.Json.JsonProperty("description", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required]
    public IList<NvdCveLanguageString> Description { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }
}
