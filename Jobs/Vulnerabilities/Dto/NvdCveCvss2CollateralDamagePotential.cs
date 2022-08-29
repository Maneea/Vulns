namespace Vulns.Jobs.Vulnerabilities;
public enum NvdCveCvss2CollateralDamagePotential
{
    UNDEFINED = 0,
    [System.Runtime.Serialization.EnumMember(Value = @"NONE")]
    NONE = 1,
    [System.Runtime.Serialization.EnumMember(Value = @"LOW")]
    LOW = 2,
    [System.Runtime.Serialization.EnumMember(Value = @"LOW_MEDIUM")]
    LOW_MEDIUM = 3,
    [System.Runtime.Serialization.EnumMember(Value = @"MEDIUM_HIGH")]
    MEDIUM_HIGH = 4,
    [System.Runtime.Serialization.EnumMember(Value = @"HIGH")]
    HIGH = 5,
    [System.Runtime.Serialization.EnumMember(Value = @"NOT_DEFINED")]
    NOT_DEFINED = 6,
}
