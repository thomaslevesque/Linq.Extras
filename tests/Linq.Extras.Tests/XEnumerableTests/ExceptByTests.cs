using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class ExceptByTests
    {
        [Fact]
        public void ExceptBy_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<int>();
            var other = Enumerable.Empty<int>();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.ExceptBy(other, Math.Abs, null));
        }

        [Fact]
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
            result.Should().Equal(
                    new Foo(2, 5),
                    new Foo(3, 2)
                );
        }

        [Fact]
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
            result.Should().Equal(
                    new Foo(0, 1),
                    new Foo(1, 3)
                );
        }

        [ExcludeFromCodeCoverage]
        struct Foo
        {
            public Foo(int x, int y) : this()
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            // ReSharper disable once MemberCanBePrivate.Local
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public int Y { get;  }
        }
    }
}
