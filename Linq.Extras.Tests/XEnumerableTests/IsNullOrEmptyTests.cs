using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class IsNullOrEmptyTests
    {
        [Fact]
        public static void IsNullOrEmpty_Generic_Returns_True_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            bool result = source.IsNullOrEmpty();
            result.Should().BeTrue();
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public static void IsNullOrEmpty_Generic_Returns_True_If_Source_Is_Empty()
        {
            var source = Enumerable.Empty<int>().ForbidMultipleEnumeration();
            bool result = source.IsNullOrEmpty();
            result.Should().BeTrue();
        }

        [Fact]
        public static void IsNullOrEmpty_Generic_Returns_False_If_Source_Is_Not_Empty()
        {
            var source = XEnumerable.Unit(42).ForbidMultipleEnumeration();
            bool result = source.IsNullOrEmpty();
            result.Should().BeFalse();
        }

        [Fact]
        public static void IsNullOrEmpty_NonGeneric_Returns_True_If_Source_Is_Null()
        {
            IEnumerable source = null;
            // ReSharper disable ConditionIsAlwaysTrueOrFalse
            bool result = source.IsNullOrEmpty();
            result.Should().BeTrue();
            // ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        [Fact]
        public static void IsNullOrEmpty_NonGeneric_Returns_True_If_Source_Is_Empty()
        {
            IEnumerable source = XEnumerable.Empty<int>().ForbidMultipleEnumeration();
            bool result = source.IsNullOrEmpty();
            result.Should().BeTrue();
        }

        [Fact]
        public static void IsNullOrEmpty_NonGeneric_Returns_True_If_Source_Is_Empty_Collection()
        {
            IEnumerable source = new int[0];
            bool result = source.IsNullOrEmpty();
            result.Should().BeTrue();
        }

        [Fact]
        public static void IsNullOrEmpty_NonGeneric_Returns_False_If_Source_Is_Not_Empty()
        {
            IEnumerable source = XEnumerable.Unit(42).ForbidMultipleEnumeration();
            bool result = source.IsNullOrEmpty();
            result.Should().BeFalse();
        }

        [Fact]
        public static void IsNullOrEmpty_NonGeneric_Returns_False_If_Source_Is_Non_Empty_Collection()
        {
            IEnumerable source = new[] { 42 };
            bool result = source.IsNullOrEmpty();
            result.Should().BeFalse();
        }
    }
}
