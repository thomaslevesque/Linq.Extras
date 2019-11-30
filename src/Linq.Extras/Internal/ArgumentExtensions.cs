using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Linq.Extras.Internal
{
    static class ArgumentExtensions
    {
        // ReSharper disable UnusedParameter.Global

        [ContractAnnotation("value:null => halt")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckArgumentNull<T>(
            [NoEnumeration] this T value,
            [InvokerParameterName] string paramName)
            where T : class
        {
            if (value is null)
                throw new ArgumentNullException(paramName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        // will uncomment if necessary

        //public static void CheckArgumentInEnum(
        //    this Enum value,
        //    [InvokerParameterName] string paramName)
        //{
        //    if (!Enum.IsDefined(value.GetType(), value))
        //        throw new ArgumentOutOfRangeException(paramName);
        //}

        // ReSharper restore UnusedParameter.Global
    }
}
