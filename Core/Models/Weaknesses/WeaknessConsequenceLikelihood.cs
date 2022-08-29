using System.Text.Json.Serialization;

namespace Vulns.Core;
[JsonConverter(typeof(SmartEnumNameConverter<WeaknessConsequenceLikelihood, int>))]
public class WeaknessConsequenceLikelihood : SmartEnum<WeaknessConsequenceLikelihood>
{
    public static readonly WeaknessConsequenceLikelihood Unspecified = new(nameof(Unspecified), 0);
    public static readonly WeaknessConsequenceLikelihood High = new(nameof(High), 1);
    public static readonly WeaknessConsequenceLikelihood Medium = new(nameof(Medium), 2);
    public static readonly WeaknessConsequenceLikelihood Low = new(nameof(Low), 3);
    public static readonly WeaknessConsequenceLikelihood Unknown = new(nameof(Unknown), 4);

    private WeaknessConsequenceLikelihood(string name, int value) : base(name, value)
    {
    }

    public WeaknessConsequenceLikelihood() : base(nameof(Unspecified), 0) { }
}