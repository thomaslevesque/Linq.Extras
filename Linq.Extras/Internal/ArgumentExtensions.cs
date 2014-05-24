using System;
using JetBrains.Annotations;

namespace Linq.Extras.Internal
{
    static class ArgumentExtensions
    {
        [ContractAnnotation("value:null => halt")]
        public static void CheckArgumentNull<T>(
            [NoEnumeration] this T value,
            [InvokerParameterName] string paramName)
            where T : class
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
        }

        public static void CheckArgumentOutOfRange<T>(
            [NotNull] this T value,
            [InvokerParameterName] string paramName,
            T min,
            T max)
            where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
                throw new ArgumentOutOfRangeException(paramName);
        }

        public static void CheckArgumentOutOfRange<T>(
            [NotNull] this T value,
            [InvokerParameterName] string paramName,
            T min,
            T max,
            string message)
            where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
                throw new ArgumentOutOfRangeException(paramName, message);
        }

        public static void CheckArgumentInEnum(
            this Enum value,
            [InvokerParameterName] string paramName)
        {
            if (!Enum.IsDefined(value.GetType(), value))
                throw new ArgumentOutOfRangeException(paramName);
        }
    }
}
