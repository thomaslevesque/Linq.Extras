using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Linq.Extras.Tests.XEnumerableTests
{
    [TestFixture]
    class DistinctUntilChangedByTests
    {
        [Test]
        public void DistinctUntilChanged_Throws_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.DistinctUntilChanged());
            ex.ParamName.Should().Be("source");
        }

        [Test]
        public void DistinctUntilChanged_Returns_Distinct_Contiguous_Elements()
        {
            var source = new[] { 1, 1, 1, 2, 3, 3, 1, 3, 2, 2 }.ForbidMultipleEnumeration();
            var result = source.DistinctUntilChanged();
            result.Should().BeEquivalentTo(new[] { 1, 2, 3, 1, 3, 2 });
        }

        [Test]
        public void DistinctUntilChanged_Uses_Specified_Comparer()
        {
            var source = new[] { -1, 1, -1, 2, 3, -3, 1, 3, -2, 2 }.ForbidMultipleEnumeration();
            var comparer = XEqualityComparer<int>.By(Math.Abs);
            var result = source.DistinctUntilChanged(comparer);
            result.Should().BeEquivalentTo(new[] { -1, 2, 3, 1, 3, -2 });
        }

        [Test]
        public void DistinctUntilChangedBy_Throws_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.DistinctUntilChangedBy(Math.Abs));
            ex.ParamName.Should().Be("source");
        }

        [Test]
        public void DistinctUntilChangedBy_Throws_If_KeySelector_Is_Null()
        {
            var source = Enumerable.Empty<int>().ForbidMultipleEnumeration();
            Func<int, int> keySelector = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.DistinctUntilChangedBy(keySelector));
            ex.ParamName.Should().Be("keySelector");
        }

        [Test]
        public void DistinctUntilChangedBy_Returns_Distinct_Contiguous_Elements()
        {
            var source = new[]
                         {
                             new Foo(0, 1),
                             new Foo(0, 2),
                             new Foo(1, 3),
                             new Foo(1, 8),
                             new Foo(2, 5),
                             new Foo(0, 3),
                             new Foo(2, 0),
                             new Foo(2, 2)
                         }.ForbidMultipleEnumeration();

            var result = source.DistinctUntilChangedBy(f => f.X);
            result.Should().BeEquivalentTo(
                new[]
                {
                    new Foo(0, 1),
                    new Foo(1, 3),
                    new Foo(2, 5),
                    new Foo(0, 3),
                    new Foo(2, 0),
                });
        }

        [Test]
        public void DistinctUntilChangedBy_Uses_Specified_Comparer()
        {
            var source = new[]
                         {
                             new Foo(0, 1),
                             new Foo(0, 2),
                             new Foo(-1, 3),
                             new Foo(1, 8),
                             new Foo(2, 5),
                             new Foo(0, 3),
                             new Foo(2, 0),
                             new Foo(-2, 2)
                         }.ForbidMultipleEnumeration();

            var comparer = XEqualityComparer<int>.By(Math.Abs);
            var result = source.DistinctUntilChangedBy(f => f.X, comparer);
            result.Should().BeEquivalentTo(
                new[]
                {
                    new Foo(0, 1),
                    new Foo(-1, 3),
                    new Foo(2, 5),
                    new Foo(0, 3),
                    new Foo(2, 0),
                });
        }


        [ExcludeFromCodeCoverage]
        struct Foo
        {
            public Foo(int x, int y)
                : this()
            {
                X = x;
                Y = y;
            }

            public int X { get; private set; }
            // ReSharper disable once MemberCanBePrivate.Local
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public int Y { get; private set; }
        }

    }
}
