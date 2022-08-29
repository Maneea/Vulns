namespace Vulns.Jobs.Vulnerabilities;
public enum NvdCveCvss3ModifiedScope
{
    UNDEFINED = 0,
    [System.Runtime.Serialization.EnumMember(Value = @"UNCHANGED")]
    UNCHANGED = 1,
    [System.Runtime.Serialization.EnumMember(Value = @"CHANGED")]
    CHANGED = 2,
    [System.Runtime.Serialization.EnumMember(Value = @"NOT_DEFINED")]
    NOT_DEFINED = 3,
}
