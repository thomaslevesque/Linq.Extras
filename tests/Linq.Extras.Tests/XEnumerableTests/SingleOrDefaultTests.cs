using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class SingleOrDefaultTests
    {
        [Fact]
        public void SingleOrDefault_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<int>();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.SingleOrDefault(42));
        }

        [Fact]
        public void SingleOrDefault_Returns_Specified_Default_Value_If_Sequence_Is_Empty()
        {
            var source = XEnumerable.Empty<int>().ForbidMultipleEnumeration();
            source.SingleOrDefault(42).Should().Be(42);
        }

        [Fact]
        public void SingleOrDefault_Returns_Single_Element_If_Sequence_Has_One_Element()
        {
            var source = XEnumerable.Unit(1).ForbidMultipleEnumeration();
            source.SingleOrDefault(42).Should().Be(1);
        }

        [Fact]
        public void SingleOrDefault_Throws_If_Sequence_Has_More_Than_One_Element()
        {
            var source = new[] { 1, 2, 3 }.ForbidMultipleEnumeration();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<InvalidOperationException>(() => source.SingleOrDefault(42));
        }

        [Fact]
        public void SingleOrDefault_With_Predicate_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<int>();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.SingleOrDefault(i => false, 42));
        }

        [Fact]
        public void SingleOrDefault_With_Predicate_Returns_Specified_Default_Value_If_Sequence_Is_Empty()
        {
            var source = XEnumerable.Empty<int>().ForbidMultipleEnumeration();
            source.SingleOrDefault(IsEven, 42).Should().Be(42);
        }

        [Fact]
        public void SingleOrDefault_With_Predicate_Returns_Specified_Default_Value_If_Sequence_Contains_No_Match()
        {
            var source = new[] { 1, 3, 5 }.ForbidMultipleEnumeration();
            source.SingleOrDefault(IsEven, 42).Should().Be(42);
        }

        [Fact]
        public void SingleOrDefault_With_Predicate_Returns_Single_Matching_Element()
        {
            var source = new[] { 1, 2, 3 }.ForbidMultipleEnumeration();
            source.SingleOrDefault(IsEven, 42).Should().Be(2);
        }

        [Fact]
        public void SingleOrDefault_With_Predicate_Throws_If_Sequence_Has_More_Than_One_Matching_Element()
        {
            var source = new[] { 1, 2, 3, 4 }.ForbidMultipleEnumeration();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<InvalidOperationException>(() => source.SingleOrDefault(42));
        }

        private static bool IsEven(int x)
        {
            return x % 2 == 0;
        }
    }
}
