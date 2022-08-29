namespace Vulns.Core;
public static class IntegerExtensions
{
    public static bool IsBetween(this int value, int min, int max) => value >= min && value <= max;
}