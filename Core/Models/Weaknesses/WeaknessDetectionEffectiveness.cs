
using System.Text.Json.Serialization;

namespace Vulns.Core;
[JsonConverter(typeof(SmartEnumNameConverter<WeaknessDetectionEffectiveness, int>))]
public class WeaknessDetectionEffectiveness : SmartEnum<WeaknessDetectionEffectiveness>
{
    public static readonly WeaknessDetectionEffectiveness Unspecified = new(nameof(Unspecified), 0);
    public static readonly WeaknessDetectionEffectiveness High = new(nameof(High), 1);
    public static readonly WeaknessDetectionEffectiveness Moderate = new(nameof(Moderate), 2);
    public static readonly WeaknessDetectionEffectiveness SoarPartial = new("SOAR Partial", 3);
    public static readonly WeaknessDetectionEffectiveness Opportunistic = new(nameof(Opportunistic), 4);
    public static readonly WeaknessDetectionEffectiveness Limited = new(nameof(Limited), 5);
    public static readonly WeaknessDetectionEffectiveness None = new(nameof(None), 6);

    private WeaknessDetectionEffectiveness(string name, int value) : base(name, value) { }
    public WeaknessDetectionEffectiveness() : base(nameof(Unspecified), 0) { }
}