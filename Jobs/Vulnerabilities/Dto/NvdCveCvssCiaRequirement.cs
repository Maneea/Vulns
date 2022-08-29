namespace Vulns.Jobs.Vulnerabilities;
public enum NvdCveCvssCiaRequirement
{
    UNDEFINED = 0,
    [System.Runtime.Serialization.EnumMember(Value = @"LOW")]
    LOW = 1,
    [System.Runtime.Serialization.EnumMember(Value = @"MEDIUM")]
    MEDIUM = 2,
    [System.Runtime.Serialization.EnumMember(Value = @"HIGH")]
    HIGH = 3,
    [System.Runtime.Serialization.EnumMember(Value = @"NOT_DEFINED")]
    NOT_DEFINED = 4,
}
