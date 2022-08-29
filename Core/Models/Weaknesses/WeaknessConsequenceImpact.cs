using System.Text.Json.Serialization;

namespace Vulns.Core;
[JsonConverter(typeof(SmartFlagEnumNameConverter<WeaknessConsequenceImpact, int>))]
public class WeaknessConsequenceImpact : SmartFlagEnum<WeaknessConsequenceImpact>
{
    public static readonly WeaknessConsequenceImpact Unspecified = new(nameof(Unspecified), 0x00000000);
    public static readonly WeaknessConsequenceImpact ModifyMemory = new("Modify Memory", 0x00000001);
    public static readonly WeaknessConsequenceImpact ReadMemory = new("Read Memory", 0x00000002);
    public static readonly WeaknessConsequenceImpact ModifyFilesOrDirectories = new("Modify Files or Directories", 0x00000004);
    public static readonly WeaknessConsequenceImpact ReadFilesOrDirectories = new("Read Files or Directories", 0x00000008);
    public static readonly WeaknessConsequenceImpact ModifyApplicationData = new("Modify Application Data", 0x00000010);
    public static readonly WeaknessConsequenceImpact ReadApplicationData = new("Read Application Data", 0x00000020);
    public static readonly WeaknessConsequenceImpact DosCrashOrExitOrRestart = new("DoS: Crash or Exit or Restart", 0x00000040);
    public static readonly WeaknessConsequenceImpact DosAmplification = new("DoS: Amplification", 0x00000080);
    public static readonly WeaknessConsequenceImpact DosInstability = new("DoS: Instability", 0x00000100);
    public static readonly WeaknessConsequenceImpact DosCpuConsumption = new("DoS: CPU Consumption", 0x00000200);
    public static readonly WeaknessConsequenceImpact DosMemoryConsumption = new("DoS: Memory Consumption", 0x00000400);
    public static readonly WeaknessConsequenceImpact DosOtherResourceConsumption = new("DoS: Other Resource Consumption", 0x00000800);
    public static readonly WeaknessConsequenceImpact ExecuteUnauthorizedCodeOrCommands = new("Execute Unauthorized Code or Commands", 0x00001000);
    public static readonly WeaknessConsequenceImpact GainPrivilegesOrAssumeIdentity = new("Gain Privileges or Assume Identity", 0x00002000);
    public static readonly WeaknessConsequenceImpact BypassProtectionMechanism = new("Bypass Protection Mechanism", 0x00004000);
    public static readonly WeaknessConsequenceImpact HideActivities = new("Hide Activities", 0x00008000);
    public static readonly WeaknessConsequenceImpact AlterExecutionLogic = new("Alter Execution Logic", 0x00010000);
    public static readonly WeaknessConsequenceImpact QualityDegradation = new("Quality Degradation", 0x00020000);
    public static readonly WeaknessConsequenceImpact UnexpectedState = new("Unexpected State", 0x00040000);
    public static readonly WeaknessConsequenceImpact VariesByContext = new("Varies By Context", 0x00080000);
    public static readonly WeaknessConsequenceImpact ReduceMaintainability = new("Reduce Maintainability", 0x00100000);
    public static readonly WeaknessConsequenceImpact ReducePerformance = new("Reduce Performance", 0x00200000);
    public static readonly WeaknessConsequenceImpact ReduceReliability = new("Reduce Reliability", 0x00400000);
    public static readonly WeaknessConsequenceImpact Other = new(nameof(Other), 0x00800000);

    private WeaknessConsequenceImpact(string name, int value) : base(name, value)
    {
    }

    public WeaknessConsequenceImpact() : base(nameof(Unspecified), 0) { }
}