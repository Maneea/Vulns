namespace Vulns.Jobs.Vulnerabilities;
public partial record NvdCveBaseMetricV3
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdCveBaseMetricV3(NvdCveCvss3 cvssV3, double exploitabilityScore, double impactScore)
    {
        this.CvssV3 = cvssV3;
        this.ExploitabilityScore = exploitabilityScore;
        this.ImpactScore = impactScore;
    }

    [Newtonsoft.Json.JsonProperty("cvssV3", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public NvdCveCvss3 CvssV3 { get; init; }

    [Newtonsoft.Json.JsonProperty("exploitabilityScore", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [System.ComponentModel.DataAnnotations.Range(0D, 10D)]
    public double ExploitabilityScore { get; init; }

    [Newtonsoft.Json.JsonProperty("impactScore", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [System.ComponentModel.DataAnnotations.Range(0D, 10D)]
    public double ImpactScore { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }
}
