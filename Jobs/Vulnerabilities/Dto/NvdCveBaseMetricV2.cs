namespace Vulns.Jobs.Vulnerabilities;
public partial record NvdCveBaseMetricV2
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdCveBaseMetricV2(
        bool acInsufInfo,
        NvdCveCvss2 cvssV2,
        double exploitabilityScore,
        double impactScore,
        bool obtainAllPrivilege,
        bool obtainOtherPrivilege,
        bool obtainUserPrivilege,
        string severity,
        bool userInteractionRequired)
    {
        this.CvssV2 = cvssV2;
        this.Severity = severity;
        this.ExploitabilityScore = exploitabilityScore;
        this.ImpactScore = impactScore;
        this.AcInsufInfo = acInsufInfo;
        this.ObtainAllPrivilege = obtainAllPrivilege;
        this.ObtainUserPrivilege = obtainUserPrivilege;
        this.ObtainOtherPrivilege = obtainOtherPrivilege;
        this.UserInteractionRequired = userInteractionRequired;
    }

    [Newtonsoft.Json.JsonProperty("cvssV2", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public NvdCveCvss2 CvssV2 { get; init; }

    [Newtonsoft.Json.JsonProperty("severity", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public string Severity { get; init; }

    [Newtonsoft.Json.JsonProperty("exploitabilityScore", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [System.ComponentModel.DataAnnotations.Range(0D, 10D)]
    public double ExploitabilityScore { get; init; }

    [Newtonsoft.Json.JsonProperty("impactScore", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [System.ComponentModel.DataAnnotations.Range(0D, 10D)]
    public double ImpactScore { get; init; }

    [Newtonsoft.Json.JsonProperty("acInsufInfo", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public bool AcInsufInfo { get; init; }

    [Newtonsoft.Json.JsonProperty("obtainAllPrivilege", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public bool ObtainAllPrivilege { get; init; }

    [Newtonsoft.Json.JsonProperty("obtainUserPrivilege", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public bool ObtainUserPrivilege { get; init; }

    [Newtonsoft.Json.JsonProperty("obtainOtherPrivilege", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public bool ObtainOtherPrivilege { get; init; }

    [Newtonsoft.Json.JsonProperty("userInteractionRequired", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    public bool UserInteractionRequired { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }
}
