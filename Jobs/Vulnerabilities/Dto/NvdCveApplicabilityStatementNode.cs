namespace Vulns.Jobs.Vulnerabilities;
/// <summary>
/// Defines a node or sub-node in an NVD applicability statement.
/// </summary>
public partial record NvdCveApplicabilityStatementNode
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdCveApplicabilityStatementNode(
        IList<NvdCveApplicabilityStatementNode> children,
        IList<NvdCveCpeMatch> cpeMathc,
        bool negate,
        string @operator)
    {
        this.Operator = @operator;
        this.Negate = negate;
        this.Children = children;
        this.CpeMatch = cpeMathc;
    }

    [Newtonsoft.Json.JsonProperty("operator", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string Operator { get; init; }

    [Newtonsoft.Json.JsonProperty("negate", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public bool Negate { get; init; }

    [Newtonsoft.Json.JsonProperty("children", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public IList<NvdCveApplicabilityStatementNode> Children { get; init; }

    [Newtonsoft.Json.JsonProperty("cpe_match", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public IList<NvdCveCpeMatch> CpeMatch { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }
}
