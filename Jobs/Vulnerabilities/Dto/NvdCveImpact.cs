namespace Vulns.Jobs.Vulnerabilities;
/// <summary>
/// Impact scores for a vulnerability as found on NVD.
/// </summary>
public partial record NvdCveImpact
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdCveImpact(NvdCveBaseMetricV2 baseMetricV2, NvdCveBaseMetricV3 baseMetricV3)
    {
        this.BaseMetricV3 = baseMetricV3;
        this.BaseMetricV2 = baseMetricV2;
    }

    /// <summary>
    /// CVSS V3.x score.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("baseMetricV3", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public NvdCveBaseMetricV3 BaseMetricV3 { get; init; }

    /// <summary>
    /// CVSS V2.0 score.
    /// </summary>
    [Newtonsoft.Json.JsonProperty("baseMetricV2", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public NvdCveBaseMetricV2 BaseMetricV2 { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }
}
