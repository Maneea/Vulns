namespace Vulns.Jobs.Vulnerabilities;
public enum NvdCveCvss2AccessVector
{
    UNDEFINED = 0,
    [System.Runtime.Serialization.EnumMember(Value = @"NETWORK")]
    NETWORK = 1,
    [System.Runtime.Serialization.EnumMember(Value = @"ADJACENT_NETWORK")]
    ADJACENT_NETWORK = 2,
    [System.Runtime.Serialization.EnumMember(Value = @"LOCAL")]
    LOCAL = 3,
}
