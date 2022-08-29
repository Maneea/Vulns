namespace Vulns.Jobs.Vulnerabilities;
public enum NvdCveCvss3ModifiedAttackVector
{
    UNDEFINED = 0,
    [System.Runtime.Serialization.EnumMember(Value = @"NETWORK")]
    NETWORK = 1,
    [System.Runtime.Serialization.EnumMember(Value = @"ADJACENT_NETWORK")]
    ADJACENT_NETWORK = 2,
    [System.Runtime.Serialization.EnumMember(Value = @"LOCAL")]
    LOCAL = 3,
    [System.Runtime.Serialization.EnumMember(Value = @"PHYSICAL")]
    PHYSICAL = 4,
    [System.Runtime.Serialization.EnumMember(Value = @"NOT_DEFINED")]
    NOT_DEFINED = 5,
}
