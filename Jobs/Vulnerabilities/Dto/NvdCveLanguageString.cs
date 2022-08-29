namespace Vulns.Jobs.Vulnerabilities;
public partial record NvdCveLanguageString
{
    [Newtonsoft.Json.JsonConstructor]
    public NvdCveLanguageString(string language, string value)
    {
        this.Language = language;
        this.Value = value;
    }

    [Newtonsoft.Json.JsonProperty("lang", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    public string Language { get; init; }

    [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.Always)]
    [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
    [System.ComponentModel.DataAnnotations.StringLength(3999)]
    public string Value { get; init; }

    private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [Newtonsoft.Json.JsonExtensionData]
    public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
    {
        get { return _additionalProperties; }
        set { _additionalProperties = value; }
    }

}
