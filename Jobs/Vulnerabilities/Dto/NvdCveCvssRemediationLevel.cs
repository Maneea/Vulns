namespace Vulns.Jobs.Vulnerabilities;
public enum NvdCveCvssRemediationLevel
{
    UNDEFINED = 0,
    [System.Runtime.Serialization.EnumMember(Value = @"OFFICIAL_FIX")]
    OFFICIAL_FIX = 1,
    [System.Runtime.Serialization.EnumMember(Value = @"TEMPORARY_FIX")]
    TEMPORARY_FIX = 2,
    [System.Runtime.Serialization.EnumMember(Value = @"WORKAROUND")]
    WORKAROUND = 3,
    [System.Runtime.Serialization.EnumMember(Value = @"UNAVAILABLE")]
    UNAVAILABLE = 4,
    [System.Runtime.Serialization.EnumMember(Value = @"NOT_DEFINED")]
    NOT_DEFINED = 5,
}
