using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Linq.Extras.Tests.XEnumerableTests
{
    class UnionByTests
    {
        [Test]
        public void UnionBy_Throws_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            IEnumerable<int> other = Enumerable.Empty<int>().ForbidEnumeration();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.UnionBy(other, Math.Abs));
            ex.ParamName.Should().Be("source");
        }

        [Test]
        public void UnionBy_Throws_If_Other_Is_Null()
        {
            IEnumerable<int> source = Enumerable.Empty<int>().ForbidEnumeration();
            IEnumerable<int> other = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.UnionBy(other, Math.Abs));
            ex.ParamName.Should().Be("other");
        }

        [Test]
        public void UnionBy_Throws_If_KeySelector_Is_Null()
        {
            var source = Enumerable.Empty<int>().ForbidEnumeration();
            var other = Enumerable.Empty<int>().ForbidEnumeration();
            Func<int, string> keySelector = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.UnionBy(other, keySelector));
            ex.ParamName.Should().Be("keySelector");
        }

        [Test]
        public void UnionBy_Returns_Items_From_Source_And_From_Other_Based_On_The_Key()
        {
            var source = new[]
                         {
                             new Foo(0, 1),
                             new Foo(0, 2),
                             new Foo(1, 3),
                             new Foo(2, 5),
                             new Foo(2, 0),
                             new Foo(3, 2)
                         }.ForbidMultipleEnumeration();
            var other = new[]
                        {
                            new Foo(0, 7),
                            new Foo(1, 0),
                            new Foo(4, 9)
                        }.ForbidMultipleEnumeration();
            var result = source.UnionBy(other, f => f.X);
            result.Should().Equal(
                             new Foo(0, 1),
                             new Foo(1, 3),
                             new Foo(2, 5),
                             new Foo(3, 2),
                             new Foo(4, 9)
                );
        }

        [Test]
        public void UnionBy_Uses_The_Specified_Key_Comparer()
        {
            var source = new[]
                         {
                             new Foo(0, 1),
                             new Foo(0, 2),
                             new Foo(1, 3),
                             new Foo(-2, 5),
                             new Foo(-2, 0),
                             new Foo(3, 2)
                         }.ForbidMultipleEnumeration();
            var other = new[]
                         {
                             new Foo(2, 5),
                             new Foo(-3, 2),
                             new Foo(4, 9)
                         }.ForbidMultipleEnumeration();
            var comparer = XEqualityComparer<int>.By(Math.Abs);
            var result = source.UnionBy(other, f => f.X, comparer);
            result.Should().Equal(
                new Foo(0, 1),
                new Foo(1, 3),
                new Foo(-2, 5),
                new Foo(3, 2),
                new Foo(4, 9)
                );
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
