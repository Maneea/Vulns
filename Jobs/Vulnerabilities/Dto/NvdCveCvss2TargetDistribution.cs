namespace Vulns.Jobs.Vulnerabilities;
public enum NvdCveCvss2TargetDistribution
{
    UNDEFINED = 0,
    [System.Runtime.Serialization.EnumMember(Value = @"NONE")]
    NONE = 1,
    [System.Runtime.Serialization.EnumMember(Value = @"LOW")]
    LOW = 2,
    [System.Runtime.Serialization.EnumMember(Value = @"MEDIUM")]
    MEDIUM = 3,
    [System.Runtime.Serialization.EnumMember(Value = @"HIGH")]
    HIGH = 4,
    [System.Runtime.Serialization.EnumMember(Value = @"NOT_DEFINED")]
    NOT_DEFINED = 5,
}
