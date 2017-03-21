using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class FirstOrDefaultTests
    {
        [Fact]
        public void FirstOrDefault_Throws_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.FirstOrDefault(42));
            ex.ParamName.Should().Be("source");
        }

        [Fact]
        public void FirstOrDefault_Returns_Specified_Default_Value_If_Sequence_Is_Empty()
        {
            var source = XEnumerable.Empty<int>().ForbidMultipleEnumeration();
            source.FirstOrDefault(42).Should().Be(42);
        }

        [Fact]
        public void FirstOrDefault_Returns_First_Element_If_Sequence_Is_Not_Empty()
        {
            var source = new[] { 1, 2, 3 }.ForbidMultipleEnumeration();
            source.FirstOrDefault(42).Should().Be(1);
        }

        [Fact]
        public void FirstOrDefault_With_Predicate_Throws_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.FirstOrDefault(42));
            ex.ParamName.Should().Be("source");
        }

        [Fact]
        public void FirstOrDefault_With_Predicate_Throws_If_Predicate_Is_Null()
        {
            var source = XEnumerable.Empty<int>().ForbidEnumeration();
            Func<int, bool> predicate = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.FirstOrDefault(predicate, 42));
            ex.ParamName.Should().Be("predicate");
        }

        [Fact]
        public void FirstOrDefault_With_Predicate_Returns_Specified_Default_Value_If_Sequence_Is_Empty()
        {
            var source = XEnumerable.Empty<int>().ForbidMultipleEnumeration();
            source.FirstOrDefault(IsEven, 42).Should().Be(42);
        }

        [Fact]
        public void FirstOrDefault_With_Predicate_Returns_Specified_Default_Value_If_Sequence_Contains_No_Match()
        {
            var source = new[] { 1, 3, 5 }.ForbidMultipleEnumeration();
            source.FirstOrDefault(IsEven, 42).Should().Be(42);
        }

        [Fact]
        public void FirstOrDefault_With_Predicate_Returns_First_Matching_Element()
        {
            var source = new[] { 2, 4, 6 }.ForbidMultipleEnumeration();
            source.FirstOrDefault(IsEven, 42).Should().Be(2);
        }

        private static bool IsEven(int x)
        {
            return x % 2 == 0;
        }
    }
}
