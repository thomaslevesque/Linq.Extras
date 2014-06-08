using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Linq.Extras.Tests.XEnumerableTests
{
    [TestFixture]
    class CommonPrefixTests
    {
        [Test]
        public void CommonPrefix_Throws_If_Source_Or_Other_Is_Null()
        {
            IEnumerable<int> source = null;
            IEnumerable<int> other = Enumerable.Empty<int>().ForbidEnumeration();
            // ReSharper disable AssignNullToNotNullAttribute
            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentNullException>(() => source.CommonPrefix(other));
            ex.ParamName.Should().Be("source");
            
            source = Enumerable.Empty<int>().ForbidEnumeration();
            other = null;
            ex = Assert.Throws<ArgumentNullException>(() => source.CommonPrefix(other));
            ex.ParamName.Should().Be("other");
            // ReSharper restore AssignNullToNotNullAttribute
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed
        }

        [Test]
        public void CommonPrefix_Returns_Common_Prefix_Of_Two_Sequences()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            var other = new[] { 4, 8, 15, 99, 123, 999 }.ForbidMultipleEnumeration();
            var result = source.CommonPrefix(other);
            result.Should().BeEquivalentTo(new[] { 4, 8, 15 });
        }

        [Test]
        public void CommonPrefix_Returns_Empty_Sequence_If_Source_Is_Empty()
        {
            var source = new int[] { }.ForbidMultipleEnumeration();
            var other = new[] { 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();

            var result = source.CommonPrefix(other);
            result.Count().Should().Be(0);
        }

        [Test]
        public void CommonPrefix_Returns_Empty_Sequence_If_Other_Is_Empty()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            var other = new int[] { }.ForbidMultipleEnumeration();

            var result = source.CommonPrefix(other);
            result.Count().Should().Be(0);
        }

        [Test]
        public void CommonPrefix_Uses_The_Specified_Comparer()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            var other = new[] { -4, 8, -15, -99, -123, 999 }.ForbidMultipleEnumeration();
            var comparer = XEqualityComparer<int>.By(Math.Abs);

            var result = source.CommonPrefix(other, comparer);
            result.Should().BeEquivalentTo(new[] { 4, 8, 15 });
        }
    }
}
