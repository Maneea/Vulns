namespace Vulns.Jobs.Vulnerabilities;
/// <summary>
/// Defines the set of product configurations for a NVD applicability statement.
/// </summary>
public partial record NvdCveApplicabilityStatements
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdCveApplicabilityStatements(string cveDataVersion, IList<NvdCveApplicabilityStatementNode> nodes)
    {
        this.CveDataVersion = cveDataVersion;
        this.Nodes = nodes;
    }

    [Newtonsoft.Json.JsonProperty("CVE_data_version", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    public string CveDataVersion { get; init; }

    [Newtonsoft.Json.JsonProperty("nodes", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public IList<NvdCveApplicabilityStatementNode> Nodes { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }
}
