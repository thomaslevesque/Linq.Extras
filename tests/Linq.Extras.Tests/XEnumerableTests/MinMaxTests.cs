using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class MinMaxTests
    {
        [Fact]
        public void MaxBy_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<Foo>();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.MaxBy(_getFooValue, null));
        }

        [Fact]
        public void MaxBy_Throws_If_Source_Is_Empty_And_TSource_Is_Not_Nullable()
        {
            var items = new DateTime[] { }.ForbidMultipleEnumeration();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<InvalidOperationException>(() => items.MaxBy(d => d.Ticks));
        }

        [Fact]
        public void MaxBy_Returns_Null_If_Source_Is_Empty_And_TSource_Is_Nullable()
        {
            var items = new Foo[] { }.ForbidMultipleEnumeration();
            var actual = items.MaxBy(f => f.Value);
            actual.Should().BeNull();
        }

        [Fact]
        public void MaxBy_Returns_Item_With_Max_Value_For_Key()
        {
            var foos = GetFoos().ForbidMultipleEnumeration();
            var fooWithMaxValue = foos.MaxBy(_getFooValue);
            var expected = "xyz";
            var actual = fooWithMaxValue?.Value;
            actual.Should().Be(expected);
        }

        [Fact]
        public void MaxBy_Returns_Item_With_Max_Value_For_Key_Based_On_Comparer()
        {
            var foos = GetFoos().ForbidMultipleEnumeration();
            var fooWithMaxValue = foos.MaxBy(_getFooValue, Comparer<string>.Default.Reverse());
            var expected = "abcd";
            var actual = fooWithMaxValue?.Value;
            actual.Should().Be(expected);
        }

        [Fact]
        public void MinBy_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<Foo>();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.MinBy(_getFooValue, null));
        }

        [Fact]
        public void MinBy_Throws_If_Source_Is_Empty_And_TSource_Is_Not_Nullable()
        {
            var items = new DateTime[] { }.ForbidMultipleEnumeration();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<InvalidOperationException>(() => items.MinBy(d => d.Ticks));
        }

        [Fact]
        public void MinBy_Returns_Null_If_Source_Is_Empty_And_TSource_Is_Nullable()
        {
            var items = new Foo[] { }.ForbidMultipleEnumeration();
            var actual = items.MinBy(f => f.Value);
            actual.Should().BeNull();
        }

        [Fact]
        public void MinBy_Returns_Item_With_Min_Value_For_Key()
        {
            var foos = GetFoos().ForbidMultipleEnumeration();
            var fooWithMinValue = foos.MinBy(_getFooValue);
            var expected = "abcd";
            var actual = fooWithMinValue?.Value;
            actual.Should().Be(expected);
        }

        [Fact]
        public void MinBy_Returns_Item_With_Min_Value_For_Key_Based_On_Comparer()
        {
            var foos = GetFoos().ForbidMultipleEnumeration();
            var fooWithMinValue = foos.MinBy(_getFooValue, Comparer<string>.Default.Reverse());
            var expected = "xyz";
            var actual = fooWithMinValue?.Value;
            actual.Should().Be(expected);
        }

        [Fact]
        public void MinBy_Returns_Item_With_Min_Non_Null_Value_For_Key()
        {
            var bars = new[]
            {
                new Bar("abcd"),
                new Bar(null),
                new Bar("efgh")
            }.ForbidMultipleEnumeration();
            var fooWithMinValue = bars.MinBy(b => b.Value);
            var expected = "abcd";
            var actual = fooWithMinValue?.Value;
            actual.Should().Be(expected);
        }

        [Fact]
        public void MinBy_Returns_Item_With_Null_Key_If_All_Items_Have_A_Null_Key()
        {
            var bars = new[] {
                new Bar(null),
                new Bar(null),
                new Bar(null)
            }.ForbidMultipleEnumeration();
            var fooWithMinValue = bars.MinBy(b => b.Value);
            var actual = fooWithMinValue?.Value;
            actual.Should().BeNull();
        }

        [Fact]
        public void Max_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<Foo>();
            var comparer = XComparer.By(_getFooValue);
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.Max(comparer));
        }

        [Fact]
        public void Max_Return_Max_Value_Based_On_Comparer()
        {
            var foos = GetFoos().ForbidMultipleEnumeration();
            var fooWithMaxValue = foos.Max(new FooComparer());
            var expected = "xyz";
            var actual = fooWithMaxValue?.Value;
            actual.Should().Be(expected);
        }

        [Fact]
        public void Max_Throws_If_Source_Is_Empty_And_TSource_Is_Not_Nullable()
        {
            var items = new DateTime[] { }.ForbidMultipleEnumeration();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<InvalidOperationException>(() => items.Max(Comparer<DateTime>.Default));
        }

        [Fact]
        public void Max_Returns_Null_If_Source_Is_Empty_And_TSource_Is_Nullable()
        {
            var items = new Foo[] { }.ForbidMultipleEnumeration();
            var actual = items.Max(new FooComparer());
            actual.Should().BeNull();
        }

        [Fact]
        public void Min_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<Foo>();
            var comparer = XComparer.By(_getFooValue);
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.Min(comparer));
        }

        [Fact]
        public void Min_Return_Min_Value_Based_On_Comparer()
        {
            var foos = GetFoos().ForbidMultipleEnumeration();
            var fooWithMinValue = foos.Min(new FooComparer());
            var expected = "abcd";
            var actual = fooWithMinValue?.Value;
            actual.Should().Be(expected);
        }

        [Fact]
        public void Min_Throws_If_Source_Is_Empty_And_TSource_Is_Not_Nullable()
        {
            var items = new DateTime[] { }.ForbidMultipleEnumeration();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<InvalidOperationException>(() => items.Min(Comparer<DateTime>.Default));
        }

        [Fact]
        public void Min_Returns_Null_If_Source_Is_Empty_And_TSource_Is_Nullable()
        {
            var items = new Foo[] { }.ForbidMultipleEnumeration();
            var actual = items.Min(new FooComparer());
            actual.Should().BeNull();
        }

        private static IEnumerable<Foo> GetFoos()
        {
            var foos = new List<Foo>
                   {
                       new Foo("abcd"),
                       new Foo("efgh"),
                       new Foo("ijkl"),
                       new Foo("mnop"),
                       new Foo("qrst"),
                       new Foo("uvw"),
                       new Foo("xyz")
                   };
            foos.Shuffle();
            return foos;
        }

        private static readonly Func<Foo, string> _getFooValue = f => f.Value;

        private class Foo
        {
            public Foo(string value)
            {
                Value = value;
            }

            public string Value { get; }
        }

        private class Bar
        {
            public Bar(string? value)
            {
                Value = value;
            }

            public string? Value { get; }
        }

        [ExcludeFromCodeCoverage]
        private class FooComparer : IComparer<Foo>
        {
            public int Compare(Foo? x, Foo? y)
            {
                if (x == null && y == null)
                    return 0;
                if (x == null)
                    return -1;
                if (y == null)
                    return 1;
                return StringComparer.CurrentCulture.Compare(x.Value, y.Value);
            }
        }
    }
}
