using System.Text.Json.Serialization;

namespace Vulns.Core;

[JsonConverter(typeof(SmartEnumNameConverter<WeaknessType, int>))]
public class WeaknessType : SmartEnum<WeaknessType>
{
    public static readonly WeaknessType Undefined = new(nameof(Undefined), 0);
    public static readonly WeaknessType Pillar = new(nameof(Pillar), 1);
    public static readonly WeaknessType Class = new(nameof(Class), 2);
    public static readonly WeaknessType Base = new(nameof(Base), 3);
    public static readonly WeaknessType Variant = new(nameof(Variant), 4);
    public static readonly WeaknessType Compound = new(nameof(Compound), 5);
    
    public WeaknessType(string name, int value) : base(name, value)
    {
    }

    public WeaknessType() : base(nameof(Undefined), 0) {}
}
