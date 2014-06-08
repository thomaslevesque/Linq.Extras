using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Linq.Extras.Tests.XEnumerableTests
{
    [TestFixture]
    class ExceptByTests
    {
        [Test]
        public void ExceptBy_Throws_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            IEnumerable<int> other = Enumerable.Empty<int>().ForbidMultipleEnumeration();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.ExceptBy(other, Math.Abs));
            ex.ParamName.Should().Be("source");
        }

        [Test]
        public void ExceptBy_Throws_If_Other_Is_Null()
        {
            IEnumerable<int> source = Enumerable.Empty<int>().ForbidMultipleEnumeration();
            IEnumerable<int> other = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.ExceptBy(other, Math.Abs));
            ex.ParamName.Should().Be("other");
        }

        [Test]
        public void ExceptBy_Throws_If_KeySelector_Is_Null()
        {
            var source = Enumerable.Empty<int>().ForbidMultipleEnumeration();
            var other = Enumerable.Empty<int>().ForbidMultipleEnumeration();
            Func<int, string> keySelector = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.ExceptBy(other, keySelector));
            ex.ParamName.Should().Be("keySelector");
        }

        [Test]
        public void ExceptBy_Returns_Items_From_Source_Not_In_Other_Based_On_The_Key()
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
                        }.ForbidMultipleEnumeration();
            var result = source.ExceptBy(other, f => f.X);
            result.Should().BeEquivalentTo(
                new[]
                {
                    new Foo(2, 5),
                    new Foo(3, 2)
                });
        }

        [Test]
        public void ExceptBy_Uses_The_Specified_Key_Comparer()
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
                             new Foo(-3, 2)
                         }.ForbidMultipleEnumeration();
            var comparer = XEqualityComparer<int>.By(Math.Abs);
            var result = source.ExceptBy(other, f => f.X, comparer);
            result.Should().BeEquivalentTo(
                new[]
                {
                    new Foo(0, 1),
                    new Foo(1, 3),
                });
        }

        [ExcludeFromCodeCoverage]
        struct Foo
        {
            public Foo(int x, int y) : this()
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
