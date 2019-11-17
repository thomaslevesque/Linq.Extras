using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class LastOrDefaultTests
    {
        [Fact]
        public void LastOrDefault_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<int>();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.LastOrDefault(42));
        }

        [Fact]
        public void LastOrDefault_Returns_Specified_Default_Value_If_Sequence_Is_Empty()
        {
            var source = XEnumerable.Empty<int>().ForbidMultipleEnumeration();
            source.LastOrDefault(42).Should().Be(42);
        }

        [Fact]
        public void LastOrDefault_Returns_Last_Element_If_Sequence_Is_Not_Empty()
        {
            var source = new[] { 1, 2, 3 }.ForbidMultipleEnumeration();
            source.LastOrDefault(42).Should().Be(3);
        }

        [Fact]
        public void LastOrDefault_With_Predicate_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<int>();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.LastOrDefault(i => true, 42));
        }

        [Fact]
        public void LastOrDefault_With_Predicate_Returns_Specified_Default_Value_If_Sequence_Is_Empty()
        {
            var source = XEnumerable.Empty<int>().ForbidMultipleEnumeration();
            source.LastOrDefault(IsEven, 42).Should().Be(42);
        }

        [Fact]
        public void LastOrDefault_With_Predicate_Returns_Specified_Default_Value_If_Sequence_Contains_No_Match()
        {
            var source = new[] { 1, 3, 5 }.ForbidMultipleEnumeration();
            source.LastOrDefault(IsEven, 42).Should().Be(42);
        }

        [Fact]
        public void LastOrDefault_With_Predicate_Returns_Last_Matching_Element()
        {
            var source = new[] { 2, 4, 6 }.ForbidMultipleEnumeration();
            source.LastOrDefault(IsEven, 42).Should().Be(6);
        }

        private static bool IsEven(int x)
        {
            return x % 2 == 0;
        }
    }
}
