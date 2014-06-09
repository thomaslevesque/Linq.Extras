using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Linq.Extras.Tests.XEnumerableTests
{
    [TestFixture]
    class SingleOrDefaultTests
    {
        [Test]
        public void SingleOrDefault_Throws_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.SingleOrDefault(42));
            ex.ParamName.Should().Be("source");
        }

        [Test]
        public void SingleOrDefault_Returns_Specified_Default_Value_If_Sequence_Is_Empty()
        {
            var source = XEnumerable.Empty<int>().ForbidMultipleEnumeration();
            source.SingleOrDefault(42).Should().Be(42);
        }

        [Test]
        public void SingleOrDefault_Returns_Single_Element_If_Sequence_Has_One_Element()
        {
            var source = XEnumerable.Unit(1).ForbidMultipleEnumeration();
            source.SingleOrDefault(42).Should().Be(1);
        }

        [Test]
        public void SingleOrDefault_Throws_If_Sequence_Has_More_Than_One_Element()
        {
            var source = new[] { 1, 2, 3 }.ForbidMultipleEnumeration();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<InvalidOperationException>(() => source.SingleOrDefault(42));
        }

        [Test]
        public void SingleOrDefault_With_Predicate_Throws_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.SingleOrDefault(42));
            ex.ParamName.Should().Be("source");
        }

        [Test]
        public void SingleOrDefault_With_Predicate_Throws_If_Predicate_Is_Null()
        {
            var source = XEnumerable.Empty<int>().ForbidEnumeration();
            Func<int, bool> predicate = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.SingleOrDefault(predicate, 42));
            ex.ParamName.Should().Be("predicate");
        }

        [Test]
        public void SingleOrDefault_With_Predicate_Returns_Specified_Default_Value_If_Sequence_Is_Empty()
        {
            var source = XEnumerable.Empty<int>().ForbidMultipleEnumeration();
            source.SingleOrDefault(IsEven, 42).Should().Be(42);
        }

        [Test]
        public void SingleOrDefault_With_Predicate_Returns_Specified_Default_Value_If_Sequence_Contains_No_Match()
        {
            var source = new[] { 1, 3, 5 }.ForbidMultipleEnumeration();
            source.SingleOrDefault(IsEven, 42).Should().Be(42);
        }

        [Test]
        public void SingleOrDefault_With_Predicate_Returns_Single_Matching_Element()
        {
            var source = new[] { 1, 2, 3 }.ForbidMultipleEnumeration();
            source.SingleOrDefault(IsEven, 42).Should().Be(2);
        }

        [Test]
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
