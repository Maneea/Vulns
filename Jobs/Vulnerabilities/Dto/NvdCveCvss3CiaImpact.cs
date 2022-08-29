namespace Vulns.Jobs.Vulnerabilities;
public enum NvdCveCvss3CiaImpact
{
    UNDEFINED = 0,
    [System.Runtime.Serialization.EnumMember(Value = @"NONE")]
    NONE = 1,
    [System.Runtime.Serialization.EnumMember(Value = @"LOW")]
    LOW = 2,
    [System.Runtime.Serialization.EnumMember(Value = @"HIGH")]
    HIGH = 3,
}
