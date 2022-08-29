namespace Vulns.Jobs.Vulnerabilities;
public partial record NvdCveProblemType
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdCveProblemType(IList<NvdCveProblemTypeData> problemTypeData)
    {
        this.ProblemTypeData = problemTypeData;
    }

    [Newtonsoft.Json.JsonProperty("problemtype_data", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required]
    public IList<NvdCveProblemTypeData> ProblemTypeData { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }
}
