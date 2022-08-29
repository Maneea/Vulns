namespace Vulns.Jobs.Vulnerabilities;
public enum NvdCveCvss3Scope
{
    UNDEFINED = 0,
    [System.Runtime.Serialization.EnumMember(Value = @"UNCHANGED")]
    UNCHANGED = 1,
    [System.Runtime.Serialization.EnumMember(Value = @"CHANGED")]
    CHANGED = 2,
}
