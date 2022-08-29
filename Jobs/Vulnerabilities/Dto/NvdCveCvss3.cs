namespace Vulns.Jobs.Vulnerabilities;
public partial record NvdCveCvss3
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdCveCvss3(
        NvdCveCvss3AttackComplexity attackComplexity,
        NvdCveCvss3AttackVector attackVector,
        NvdCveCvss3CiaImpact availabilityImpact,
        NvdCveCvssCiaRequirement availabilityRequirement,
        double baseScore,
        NvdCveCvss3Severity baseSeverity,
        NvdCveCvss3CiaImpact confidentialityImpact,
        NvdCveCvssCiaRequirement confidentialityRequirement,
        double environmentalScore,
        NvdCveCvss3Severity environmentalSeverity,
        NvdCveCvss3ExploitCodeMaturity exploitCodeMaturity,
        NvdCveCvss3CiaImpact integrityImpact,
        NvdCveCvssCiaRequirement integrityRequirement,
        NvdCveCvss3ModifiedAttackComplexity modifiedAttackComplexity,
        NvdCveCvss3ModifiedAttackVector modifiedAttackVector,
        NvdCveCvss3ModifiedCiaImpact modifiedAvailabilityImpact,
        NvdCveCvss3ModifiedCiaImpact modifiedConfidentialityImpact,
        NvdCveCvss3ModifiedCiaImpact modifiedIntegrityImpact,
        NvdCveCvss3ModifiedPrivilegesRequired modifiedPrivilegesRequired,
        NvdCveCvss3ModifiedScope modifiedScope,
        NvdCveCvss3ModifiedUserInteraction modifiedUserInteraction,
        NvdCveCvss3PrivilegesRequired privilegesRequired,
        NvdCveCvssRemediationLevel remediationLevel,
        NvdCveCvss3Confidence reportConfidence,
        NvdCveCvss3Scope scope,
        double temporalScore,
        NvdCveCvss3Severity temporalSeverity,
        NvdCveCvss3UserInteraction userInteraction,
        string vectorString,
        NvdCveCvss3Version version)
    {
        this.Version = version;
        this.VectorString = vectorString;
        this.AttackVector = attackVector;
        this.AttackComplexity = attackComplexity;
        this.PrivilegesRequired = privilegesRequired;
        this.UserInteraction = userInteraction;
        this.Scope = scope;
        this.ConfidentialityImpact = confidentialityImpact;
        this.IntegrityImpact = integrityImpact;
        this.AvailabilityImpact = availabilityImpact;
        this.BaseScore = baseScore;
        this.BaseSeverity = baseSeverity;
        this.ExploitCodeMaturity = exploitCodeMaturity;
        this.RemediationLevel = remediationLevel;
        this.ReportConfidence = reportConfidence;
        this.TemporalScore = temporalScore;
        this.TemporalSeverity = temporalSeverity;
        this.ConfidentialityRequirement = confidentialityRequirement;
        this.IntegrityRequirement = integrityRequirement;
        this.AvailabilityRequirement = availabilityRequirement;
        this.ModifiedAttackVector = modifiedAttackVector;
        this.ModifiedAttackComplexity = modifiedAttackComplexity;
        this.ModifiedPrivilegesRequired = modifiedPrivilegesRequired;
        this.ModifiedUserInteraction = modifiedUserInteraction;
        this.ModifiedScope = modifiedScope;
        this.ModifiedConfidentialityImpact = modifiedConfidentialityImpact;
        this.ModifiedIntegrityImpact = modifiedIntegrityImpact;
        this.ModifiedAvailabilityImpact = modifiedAvailabilityImpact;
        this.EnvironmentalScore = environmentalScore;
        this.EnvironmentalSeverity = environmentalSeverity;
    }

    /// <summary>
    /// CVSS Version
    /// </summary>
    [Newtonsoft.Json.JsonProperty("version", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3Version Version { get; init; }

    [Newtonsoft.Json.JsonProperty("vectorString", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^CVSS:3.[01]/((AV:[NALP]|AC:[LH]|PR:[UNLH]|UI:[NR]|S:[UC]|[CIA]:[NLH]|E:[XUPFH]|RL:[XOTWU]|RC:[XURC]|[CIA]R:[XLMH]|MAV:[XNALP]|MAC:[XLH]|MPR:[XUNLH]|MUI:[XNR]|MS:[XUC]|M[CIA]:[XNLH])/)*(AV:[NALP]|AC:[LH]|PR:[UNLH]|UI:[NR]|S:[UC]|[CIA]:[NLH]|E:[XUPFH]|RL:[XOTWU]|RC:[XURC]|[CIA]R:[XLMH]|MAV:[XNALP]|MAC:[XLH]|MPR:[XUNLH]|MUI:[XNR]|MS:[XUC]|M[CIA]:[XNLH])$")]
    public string VectorString { get; init; }

    [Newtonsoft.Json.JsonProperty("attackVector", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3AttackVector AttackVector { get; init; }

    [Newtonsoft.Json.JsonProperty("attackComplexity", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3AttackComplexity AttackComplexity { get; init; }

    [Newtonsoft.Json.JsonProperty("privilegesRequired", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3PrivilegesRequired PrivilegesRequired { get; init; }

    [Newtonsoft.Json.JsonProperty("userInteraction", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3UserInteraction UserInteraction { get; init; }

    [Newtonsoft.Json.JsonProperty("scope", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3Scope Scope { get; init; }

    [Newtonsoft.Json.JsonProperty("confidentialityImpact", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3CiaImpact ConfidentialityImpact { get; init; }

    [Newtonsoft.Json.JsonProperty("integrityImpact", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3CiaImpact IntegrityImpact { get; init; }

    [Newtonsoft.Json.JsonProperty("availabilityImpact", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3CiaImpact AvailabilityImpact { get; init; }

    [Newtonsoft.Json.JsonProperty("baseScore", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Range(0D, 10D)]
    public double BaseScore { get; init; }

    [Newtonsoft.Json.JsonProperty("baseSeverity", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3Severity BaseSeverity { get; init; }

    [Newtonsoft.Json.JsonProperty("exploitCodeMaturity", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3ExploitCodeMaturity ExploitCodeMaturity { get; init; }

    [Newtonsoft.Json.JsonProperty("remediationLevel", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvssRemediationLevel RemediationLevel { get; init; }

    [Newtonsoft.Json.JsonProperty("reportConfidence", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3Confidence ReportConfidence { get; init; }

    [Newtonsoft.Json.JsonProperty("temporalScore", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [System.ComponentModel.DataAnnotations.Range(0D, 10D)]
    public double TemporalScore { get; init; }

    [Newtonsoft.Json.JsonProperty("temporalSeverity", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3Severity TemporalSeverity { get; init; }

    [Newtonsoft.Json.JsonProperty("confidentialityRequirement", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvssCiaRequirement ConfidentialityRequirement { get; init; }

    [Newtonsoft.Json.JsonProperty("integrityRequirement", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvssCiaRequirement IntegrityRequirement { get; init; }

    [Newtonsoft.Json.JsonProperty("availabilityRequirement", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvssCiaRequirement AvailabilityRequirement { get; init; }

    [Newtonsoft.Json.JsonProperty("modifiedAttackVector", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3ModifiedAttackVector ModifiedAttackVector { get; init; }

    [Newtonsoft.Json.JsonProperty("modifiedAttackComplexity", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3ModifiedAttackComplexity ModifiedAttackComplexity { get; init; }

    [Newtonsoft.Json.JsonProperty("modifiedPrivilegesRequired", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3ModifiedPrivilegesRequired ModifiedPrivilegesRequired { get; init; }

    [Newtonsoft.Json.JsonProperty("modifiedUserInteraction", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3ModifiedUserInteraction ModifiedUserInteraction { get; init; }

    [Newtonsoft.Json.JsonProperty("modifiedScope", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3ModifiedScope ModifiedScope { get; init; }

    [Newtonsoft.Json.JsonProperty("modifiedConfidentialityImpact", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3ModifiedCiaImpact ModifiedConfidentialityImpact { get; init; }

    [Newtonsoft.Json.JsonProperty("modifiedIntegrityImpact", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3ModifiedCiaImpact ModifiedIntegrityImpact { get; init; }

    [Newtonsoft.Json.JsonProperty("modifiedAvailabilityImpact", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3ModifiedCiaImpact ModifiedAvailabilityImpact { get; init; }

    [Newtonsoft.Json.JsonProperty("environmentalScore", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [System.ComponentModel.DataAnnotations.Range(0D, 10D)]
    public double EnvironmentalScore { get; init; }

    [Newtonsoft.Json.JsonProperty("environmentalSeverity", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss3Severity EnvironmentalSeverity { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }
}
