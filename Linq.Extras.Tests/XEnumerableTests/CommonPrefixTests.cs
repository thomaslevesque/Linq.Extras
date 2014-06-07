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
            IEnumerable<int> other = Enumerable.Empty<int>();
            // ReSharper disable AssignNullToNotNullAttribute
            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentNullException>(() => source.CommonPrefix(other));
            ex.ParamName.Should().Be("source");
            
            source = Enumerable.Empty<int>();
            other = null;
            ex = Assert.Throws<ArgumentNullException>(() => source.CommonPrefix(other));
            ex.ParamName.Should().Be("other");
            // ReSharper restore AssignNullToNotNullAttribute
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed
        }

        [Test]
        public void CommonPrefix_Returns_Common_Prefix_Of_Two_Sequences()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 };
            var other = new[] { 4, 8, 15, 99, 123, 999 };
            var result = source.CommonPrefix(other);
            result.Should().BeEquivalentTo(new[] { 4, 8, 15 });
        }

        [Test]
        public void CommonPrefix_Returns_Empty_Sequence_If_Source_Or_Other_Is_Empty()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 };
            var other = new int[] { };

            var result = source.CommonPrefix(other);
            result.Should().BeEmpty();

            result = other.CommonPrefix(source);
            result.Should().BeEmpty();
        }

        [Test]
        public void CommonPrefix_Uses_The_Provided_Comparer()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 };
            var other = new[] { -4, 8, -15, -99, -123, 999 };
            var comparer = XEqualityComparer<int>.By(Math.Abs);

            var result = source.CommonPrefix(other, comparer);
            result.Should().BeEquivalentTo(new[] { 4, 8, 15 });
        }
    }
}
