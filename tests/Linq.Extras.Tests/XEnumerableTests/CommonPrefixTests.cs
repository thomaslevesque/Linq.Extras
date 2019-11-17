using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class CommonPrefixTests
    {
        [Fact]
        public void CommonPrefix_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<int>();
            var other = Enumerable.Empty<int>();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.CommonPrefix(other, null));
        }

        [Fact]
        public void CommonPrefix_Returns_Common_Prefix_Of_Two_Sequences()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            var other = new[] { 4, 8, 15, 99, 123, 999 }.ForbidMultipleEnumeration();
            var result = source.CommonPrefix(other);
            result.Should().Equal(4, 8, 15);
        }

        [Fact]
        public void CommonPrefix_Returns_Empty_Sequence_If_Source_Is_Empty()
        {
            var source = new int[] { }.ForbidMultipleEnumeration();
            var other = new[] { 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();

            var result = source.CommonPrefix(other);
            result.Count().Should().Be(0);
        }

        [Fact]
        public void CommonPrefix_Returns_Empty_Sequence_If_Other_Is_Empty()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            var other = new int[] { }.ForbidMultipleEnumeration();

            var result = source.CommonPrefix(other);
            result.Count().Should().Be(0);
        }

        [Fact]
        public void CommonPrefix_Uses_The_Specified_Comparer()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            var other = new[] { -4, 8, -15, -99, -123, 999 }.ForbidMultipleEnumeration();
            var comparer = XEqualityComparer<int>.By(Math.Abs);

            var result = source.CommonPrefix(other, comparer);
            result.Should().Equal(4, 8, 15);
        }
    }
}
