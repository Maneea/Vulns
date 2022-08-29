namespace Vulns.Core;

public static class CharExtensions
{
    public static bool IsUppercase(this char input) => input >= 0x41 && input <= 0x5A;
    public static bool IsLowercase(this char input) => input >= 0x61 && input <= 0x7A;
    public static char ToUppercase(this char input) => input >= 0x61 && input <= 0x7A ? (char)(input - 0x20) : input;
    public static char ToLowercase(this char input) => input >= 0x41 && input <= 0x5A ? (char)(input + 0x20) : input;
}