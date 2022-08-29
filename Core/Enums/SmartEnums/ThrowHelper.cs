using Vulns.Core.Exceptions;

namespace Vulns.Core;
static class ThrowHelper
{
    public static void ThrowArgumentNullException(string paramName)
        => throw new ArgumentNullException(paramName);

    public static void ThrowArgumentNullOrEmptyException(string paramName)
        => throw new ArgumentException("Argument cannot be null or empty.", paramName);

    public static void ThrowNameNotFoundException<TEnum, TValue>(string? name)
        where TEnum : ISmartEnum
        where TValue : IEquatable<TValue>, IComparable<TValue>
        => throw new SmartEnumNotFoundException($"No {typeof(TEnum).Name} with Name \"{name}\" found.");

    public static void ThrowValueNotFoundException<TEnum, TValue>(TValue value)
        where TEnum : ISmartEnum
        where TValue : IEquatable<TValue>, IComparable<TValue>
        => throw new SmartEnumNotFoundException($"No {typeof(TEnum).Name} with Value {value} found.");

    public static void ThrowInvalidValueCastException<TEnum, TValue>(TValue value)
        where TEnum : ISmartEnum
        where TValue : IEquatable<TValue>, IComparable<TValue>
    => throw new InvalidFlagEnumValueParseException($"The value: {value} input to {typeof(TEnum).Name} could not be parsed into an integer value.");

    public static void ThrowDoesNotContainPowerOfTwoValuesException<TEnum, TValue>()
        where TEnum : ISmartEnum
        where TValue : IEquatable<TValue>, IComparable<TValue>
    => throw new SmartFlagEnumDoesNotContainPowerOfTwoValuesException($"the {typeof(TEnum).Name} does not contain consecutive power of two values.");
}