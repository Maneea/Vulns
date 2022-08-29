namespace Vulns.Jobs.Vulnerabilities;
public enum NvdCveCvss2ReportConfidence
{
    UNDEFINED = 0,
    [System.Runtime.Serialization.EnumMember(Value = @"UNCONFIRMED")]
    UNCONFIRMED = 1,
    [System.Runtime.Serialization.EnumMember(Value = @"UNCORROBORATED")]
    UNCORROBORATED = 2,
    [System.Runtime.Serialization.EnumMember(Value = @"CONFIRMED")]
    CONFIRMED = 3,
    [System.Runtime.Serialization.EnumMember(Value = @"NOT_DEFINED")]
    NOT_DEFINED = 4,
}
