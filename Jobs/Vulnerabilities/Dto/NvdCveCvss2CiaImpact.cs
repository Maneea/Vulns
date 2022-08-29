namespace Vulns.Jobs.Vulnerabilities;
public enum NvdCveCvss2CiaImpact
{
    UNDEFINED = 0,
    [System.Runtime.Serialization.EnumMember(Value = @"NONE")]
    NONE = 1,
    [System.Runtime.Serialization.EnumMember(Value = @"PARTIAL")]
    PARTIAL = 2,
    [System.Runtime.Serialization.EnumMember(Value = @"COMPLETE")]
    COMPLETE = 3,
}
