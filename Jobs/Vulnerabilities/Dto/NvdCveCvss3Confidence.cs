namespace Vulns.Jobs.Vulnerabilities;
public enum NvdCveCvss3Confidence
{
    UNDEFINED = 0,
    [System.Runtime.Serialization.EnumMember(Value = @"UNKNOWN")]
    UNKNOWN = 1,
    [System.Runtime.Serialization.EnumMember(Value = @"REASONABLE")]
    REASONABLE = 2,
    [System.Runtime.Serialization.EnumMember(Value = @"CONFIRMED")]
    CONFIRMED = 3,
    [System.Runtime.Serialization.EnumMember(Value = @"NOT_DEFINED")]
    NOT_DEFINED = 4,
}
