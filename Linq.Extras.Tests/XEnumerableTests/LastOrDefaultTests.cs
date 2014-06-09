using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Linq.Extras.Tests.XEnumerableTests
{
    [TestFixture]
    class LastOrDefaultTests
    {
        [Test]
        public void LastOrDefault_Throws_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.LastOrDefault(42));
            ex.ParamName.Should().Be("source");
        }

        [Test]
        public void LastOrDefault_Returns_Specified_Default_Value_If_Sequence_Is_Empty()
        {
            var source = XEnumerable.Empty<int>().ForbidMultipleEnumeration();
            source.LastOrDefault(42).Should().Be(42);
        }

        [Test]
        public void LastOrDefault_Returns_Last_Element_If_Sequence_Is_Not_Empty()
        {
            var source = new[] { 1, 2, 3 }.ForbidMultipleEnumeration();
            source.LastOrDefault(42).Should().Be(3);
        }

        [Test]
        public void LastOrDefault_With_Predicate_Throws_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.LastOrDefault(42));
            ex.ParamName.Should().Be("source");
        }

        [Test]
        public void LastOrDefault_With_Predicate_Throws_If_Predicate_Is_Null()
        {
            var source = XEnumerable.Empty<int>().ForbidEnumeration();
            Func<int, bool> predicate = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.LastOrDefault(predicate, 42));
            ex.ParamName.Should().Be("predicate");
        }

        [Test]
        public void LastOrDefault_With_Predicate_Returns_Specified_Default_Value_If_Sequence_Is_Empty()
        {
            var source = XEnumerable.Empty<int>().ForbidMultipleEnumeration();
            source.LastOrDefault(IsEven, 42).Should().Be(42);
        }

        [Test]
        public void LastOrDefault_With_Predicate_Returns_Specified_Default_Value_If_Sequence_Contains_No_Match()
        {
            var source = new[] { 1, 3, 5 }.ForbidMultipleEnumeration();
            source.LastOrDefault(IsEven, 42).Should().Be(42);
        }

        [Test]
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
