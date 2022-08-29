using System.Text.Json.Serialization;

namespace Vulns.Core;
[JsonConverter(typeof(SmartFlagEnumNameConverter<WeaknessConsequenceScope, int>))]
public class WeaknessConsequenceScope : SmartFlagEnum<WeaknessConsequenceScope>
{
    public static readonly WeaknessConsequenceScope Unspecified = new(nameof(Unspecified), 0);
    public static readonly WeaknessConsequenceScope Confidentiality = new(nameof(Confidentiality), 1);
    public static readonly WeaknessConsequenceScope Integrity = new(nameof(Integrity), 2);
    public static readonly WeaknessConsequenceScope Availability = new(nameof(Availability), 4);
    public static readonly WeaknessConsequenceScope AccessControl = new("Access Control", 8);
    public static readonly WeaknessConsequenceScope Accountability = new(nameof(Accountability), 16);
    public static readonly WeaknessConsequenceScope Authentication = new(nameof(Authentication), 32);
    public static readonly WeaknessConsequenceScope Authorization = new(nameof(Authorization), 64);
    public static readonly WeaknessConsequenceScope NonRepudiation = new("Non-Repudiation", 128);
    public static readonly WeaknessConsequenceScope Other = new(nameof(Other), 256);

    private WeaknessConsequenceScope(string name, int value) : base(name, value)
    {
    }

    public WeaknessConsequenceScope() : base(nameof(Unspecified), 0) { }
}