using System.Text.Json.Serialization;

namespace Vulns.Core;
[JsonConverter(typeof(SmartFlagEnumNameConverter<WeaknessAffectedResources, int>))]
public class WeaknessAffectedResources : SmartFlagEnum<WeaknessAffectedResources>
{
    public static readonly WeaknessAffectedResources Unspecified = new(nameof(Unspecified), 0);
    public static readonly WeaknessAffectedResources Cpu = new("CPU", 1);
    public static readonly WeaknessAffectedResources FileOrDirectory = new("File or Directory", 2);
    public static readonly WeaknessAffectedResources Memory = new("Memory", 4);
    public static readonly WeaknessAffectedResources SystemProcess = new("System Process", 8);
    public static readonly WeaknessAffectedResources Other = new(nameof(Other), 16);

    private WeaknessAffectedResources(string name, int value) : base(name, value)
    {
    }

    public WeaknessAffectedResources() : base(nameof(Unspecified), 0) { }
}