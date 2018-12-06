using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class MinMaxTests
    {
        [Fact]
        public void MaxBy_Throws_If_Source_Is_Null()
        {
            IEnumerable<Foo> source = null;
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentNullException>(() => source.MaxBy(_getFooValue));
            ex.ParamName.Should().Be("source");
        }

        [Fact]
        public void MaxBy_Throws_If_Source_Is_Empty()
        {
            var foos = new Foo[] { }.ForbidMultipleEnumeration();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<InvalidOperationException>(() => foos.MaxBy(_getFooValue));
        }

        [Fact]
        public void MaxBy_Throws_If_KeySelector_Is_Null()
        {
            var source = GetFoos().ForbidEnumeration();
            Func<Foo, string> keySelector = null;
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentNullException>(() => source.MaxBy(keySelector));
            ex.ParamName.Should().Be("keySelector");
        }

        [Fact]
        public void MaxBy_Returns_Item_With_Max_Value_For_Key()
        {
            var foos = GetFoos().ForbidMultipleEnumeration();
            var fooWithMaxValue = foos.MaxBy(_getFooValue);
            var expected = "xyz";
            var actual = fooWithMaxValue.Value;
            actual.Should().Be(expected);
        }

        [Fact]
        public void MaxBy_Returns_Item_With_Max_Value_For_Key_Based_On_Comparer()
        {
            var foos = GetFoos().ForbidMultipleEnumeration();
            var fooWithMaxValue = foos.MaxBy(_getFooValue, Comparer<string>.Default.Reverse());
            var expected = "abcd";
            var actual = fooWithMaxValue.Value;
            actual.Should().Be(expected);
        }

        [Fact]
        public void MinBy_Throws_If_Source_Is_Null()
        {
            IEnumerable<Foo> source = null;
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentNullException>(() => source.MinBy(_getFooValue));
            ex.ParamName.Should().Be("source");
        }

        [Fact]
        public void MinBy_Throws_If_KeySelector_Is_Null()
        {
            var source = GetFoos().ForbidEnumeration();
            Func<Foo, string> keySelector = null;
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentNullException>(() => source.MinBy(keySelector));
            ex.ParamName.Should().Be("keySelector");
        }

        [Fact]
        public void MinBy_Throws_If_Source_Is_Empty()
        {
            var foos = new Foo[] { }.ForbidMultipleEnumeration();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<InvalidOperationException>(() => foos.MinBy(_getFooValue));
        }

        [Fact]
        public void MinBy_Returns_Item_With_Min_Value_For_Key()
        {
            var foos = GetFoos().ForbidMultipleEnumeration();
            var fooWithMinValue = foos.MinBy(_getFooValue);
            var expected = "abcd";
            var actual = fooWithMinValue.Value;
            actual.Should().Be(expected);
        }

        [Fact]
        public void MinBy_Returns_Item_With_Min_Value_For_Key_Based_On_Comparer()
        {
            var foos = GetFoos().ForbidMultipleEnumeration();
            var fooWithMinValue = foos.MinBy(_getFooValue, Comparer<string>.Default.Reverse());
            var expected = "xyz";
            var actual = fooWithMinValue.Value;
            actual.Should().Be(expected);
        }

        [Fact]
        public void Max_Throws_If_Source_Is_Null()
        {
            IEnumerable<Foo> source = null;
            var comparer = XComparer.By(_getFooValue);
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentNullException>(() => source.Max(comparer));
            ex.ParamName.Should().Be("source");
        }

        [Fact]
        public void Max_Throws_If_Comparer_Is_Null()
        {
            var source = GetFoos().ForbidEnumeration();
            IComparer<Foo> comparer = null;
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentNullException>(() => source.Max(comparer));
            ex.ParamName.Should().Be("comparer");
        }

        [Fact]
        public void Max_Return_Max_Value_Based_On_Comparer()
        {
            var foos = GetFoos().ForbidMultipleEnumeration();
            var fooWithMaxValue = foos.Max(new FooComparer());
            var expected = "xyz";
            var actual = fooWithMaxValue.Value;
            actual.Should().Be(expected);
        }

        [Fact]
        public void Max_Throws_If_Source_Is_Empty()
        {
            var foos = new Foo[] { }.ForbidMultipleEnumeration();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<InvalidOperationException>(() => foos.Max(new FooComparer()));
        }

        [Fact]
        public void Min_Throws_If_Source_Is_Null()
        {
            IEnumerable<Foo> source = null;
            var comparer = XComparer.By(_getFooValue);
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentNullException>(() => source.Min(comparer));
            ex.ParamName.Should().Be("source");
        }

        [Fact]
        public void Min_Throws_If_Comparer_Is_Null()
        {
            var source = GetFoos().ForbidEnumeration();
            IComparer<Foo> comparer = null;
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentNullException>(() => source.Min(comparer));
            ex.ParamName.Should().Be("comparer");
        }

        [Fact]
        public void Min_Return_Min_Value_Based_On_Comparer()
        {
            var foos = GetFoos().ForbidMultipleEnumeration();
            var fooWithMinValue = foos.Min(new FooComparer());
            var expected = "abcd";
            var actual = fooWithMinValue.Value;
            actual.Should().Be(expected);
        }

        [Fact]
        public void Min_Throws_If_Source_Is_Empty()
        {
            var foos = new Foo[] { }.ForbidMultipleEnumeration();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<InvalidOperationException>(() => foos.Min(new FooComparer()));
        }

        private static IEnumerable<Foo> GetFoos()
        {
            var foos = new List<Foo>
                   {
                       new Foo { Value = "abcd" },
                       new Foo { Value = "efgh" },
                       new Foo { Value = "ijkl" },
                       new Foo { Value = "mnop" },
                       new Foo { Value = "qrst" },
                       new Foo { Value = "uvw" },
                       new Foo { Value = "xyz" }
                   };
            foos.Shuffle();
            return foos;
        }

        private static readonly Func<Foo, string> _getFooValue = f => f.Value;

        private class Foo
        {
            public string Value { get; set; }
        }

        [ExcludeFromCodeCoverage]
        private class FooComparer : IComparer<Foo>
        {
            public int Compare(Foo x, Foo y)
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
