namespace Vulns.Jobs.Vulnerabilities;
public partial record NvdCveCvss2
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdCveCvss2(
        NvdCveCvss2AccessComplexity accessComplexity,
        NvdCveCvss2AccessVector accessVector,
        NvdCveCvss2Authentication authentication,
        NvdCveCvss2CiaImpact availabilityImpact,
        NvdCveCvssCiaRequirement availabilityRequirement,
        double baseScore,
        NvdCveCvss2CollateralDamagePotential collateralDamagePotential,
        NvdCveCvss2CiaImpact confidentialityImpact,
        NvdCveCvssCiaRequirement confidentialityRequirement,
        double environmentalScore,
        NvdCveCvss2Exploitability exploitability,
        NvdCveCvss2CiaImpact integrityImpact,
        NvdCveCvssCiaRequirement integrityRequirement,
        NvdCveCvssRemediationLevel remediationLevel,
        NvdCveCvss2ReportConfidence reportConfidence,
        NvdCveCvss2TargetDistribution targetDistribution,
        double temporalScore,
        string vectorString)
    {
        this.VectorString = vectorString;
        this.AccessVector = accessVector;
        this.AccessComplexity = accessComplexity;
        this.Authentication = authentication;
        this.ConfidentialityImpact = confidentialityImpact;
        this.IntegrityImpact = integrityImpact;
        this.AvailabilityImpact = availabilityImpact;
        this.BaseScore = baseScore;
        this.Exploitability = exploitability;
        this.RemediationLevel = remediationLevel;
        this.ReportConfidence = reportConfidence;
        this.TemporalScore = temporalScore;
        this.CollateralDamagePotential = collateralDamagePotential;
        this.TargetDistribution = targetDistribution;
        this.ConfidentialityRequirement = confidentialityRequirement;
        this.IntegrityRequirement = integrityRequirement;
        this.AvailabilityRequirement = availabilityRequirement;
        this.EnvironmentalScore = environmentalScore;
    }

    [Newtonsoft.Json.JsonProperty("vectorString", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.RegularExpression(@"^((AV:[NAL]|AC:[LMH]|Au:[MSN]|[CIA]:[NPC]|E:(U|POC|F|H|ND)|RL:(OF|TF|W|U|ND)|RC:(UC|UR|C|ND)|CDP:(N|L|LM|MH|H|ND)|TD:(N|L|M|H|ND)|[CIA]R:(L|M|H|ND))/)*(AV:[NAL]|AC:[LMH]|Au:[MSN]|[CIA]:[NPC]|E:(U|POC|F|H|ND)|RL:(OF|TF|W|U|ND)|RC:(UC|UR|C|ND)|CDP:(N|L|LM|MH|H|ND)|TD:(N|L|M|H|ND)|[CIA]R:(L|M|H|ND))$")]
    public string VectorString { get; init; }

    [Newtonsoft.Json.JsonProperty("accessVector", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss2AccessVector AccessVector { get; init; }

    [Newtonsoft.Json.JsonProperty("accessComplexity", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss2AccessComplexity AccessComplexity { get; init; }

    [Newtonsoft.Json.JsonProperty("authentication", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss2Authentication Authentication { get; init; }

    [Newtonsoft.Json.JsonProperty("confidentialityImpact", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss2CiaImpact ConfidentialityImpact { get; init; }

    [Newtonsoft.Json.JsonProperty("integrityImpact", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss2CiaImpact IntegrityImpact { get; init; }

    [Newtonsoft.Json.JsonProperty("availabilityImpact", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss2CiaImpact AvailabilityImpact { get; init; }

    [Newtonsoft.Json.JsonProperty("baseScore", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Range(0D, 10D)]
    public double BaseScore { get; init; }

    [Newtonsoft.Json.JsonProperty("exploitability", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss2Exploitability Exploitability { get; init; }

    [Newtonsoft.Json.JsonProperty("remediationLevel", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvssRemediationLevel RemediationLevel { get; init; }

    [Newtonsoft.Json.JsonProperty("reportConfidence", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss2ReportConfidence ReportConfidence { get; init; }

    [Newtonsoft.Json.JsonProperty("temporalScore", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [System.ComponentModel.DataAnnotations.Range(0D, 10D)]
    public double TemporalScore { get; init; }

    [Newtonsoft.Json.JsonProperty("collateralDamagePotential", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss2CollateralDamagePotential CollateralDamagePotential { get; init; }

    [Newtonsoft.Json.JsonProperty("targetDistribution", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvss2TargetDistribution TargetDistribution { get; init; }

    [Newtonsoft.Json.JsonProperty("confidentialityRequirement", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvssCiaRequirement ConfidentialityRequirement { get; init; }

    [Newtonsoft.Json.JsonProperty("integrityRequirement", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvssCiaRequirement IntegrityRequirement { get; init; }

    [Newtonsoft.Json.JsonProperty("availabilityRequirement", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public NvdCveCvssCiaRequirement AvailabilityRequirement { get; init; }

    [Newtonsoft.Json.JsonProperty("environmentalScore", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
    [System.ComponentModel.DataAnnotations.Range(0D, 10D)]
    public double EnvironmentalScore { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }
}
