namespace Vulns.Jobs.Vulnerabilities;
public partial record NvdCveItemDetails
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdCveItemDetails(
        NvdCveCveMetadata CveMetadata,
        NvdCveDescription description,
        NvdCveProblemType problemType,
        NvdCveReferences references)
    {
        this.CveMetadata = CveMetadata;
        this.ProblemType = problemType;
        this.References = references;
        this.Description = description;
    }

    [Newtonsoft.Json.JsonProperty("CVE_data_meta", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required]
    public NvdCveCveMetadata CveMetadata { get; init; }

    [Newtonsoft.Json.JsonProperty("problemtype", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required]
    public NvdCveProblemType ProblemType { get; init; }

    [Newtonsoft.Json.JsonProperty("references", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required]
    public NvdCveReferences References { get; init; }

    [Newtonsoft.Json.JsonProperty("description", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required]
    public NvdCveDescription Description { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }
}
