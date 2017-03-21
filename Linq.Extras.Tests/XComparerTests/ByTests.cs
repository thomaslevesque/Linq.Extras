using System;
using Xunit;

namespace Linq.Extras.Tests.XComparerTests
{
    public class ByTests
    {
        [Fact]
        public void By_Returns_A_Comparer_Based_On_Key()
        {
            var a = new Foo { X = 0, Y = 0 };
            var b = new Foo { X = 1, Y = 0 };
            
            Func<Foo, int> keySelector = f => f.X;
            var comparer = XComparer.By(keySelector);

            int expected = -1;
            int actual = comparer.Compare(a, b);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ByDescending_Returns_A_Descending_Comparer_Based_On_Key()
        {
            var a = new Foo { X = 0, Y = 0 };
            var b = new Foo { X = 1, Y = 0 };

            Func<Foo, int> keySelector = f => f.X;
            var comparer = XComparer.ByDescending(keySelector);

            int expected = 1;
            int actual = comparer.Compare(a, b);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ThenBy_Returns_A_Comparer_Based_On_First_Then_Second_Key()
        {
            var a = new Foo { X = 0, Y = 0 };
            var b = new Foo { X = 0, Y = 1 };

            Func<Foo, int> keySelector1 = f => f.X;
            Func<Foo, int> keySelector2 = f => f.Y;
            var comparer = XComparer.By(keySelector1).ThenBy(keySelector2);

            int expected = -1;
            int actual = comparer.Compare(a, b);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ThenByDescending_Returns_A_Comparer_Based_On_First_Then_Descending_Second_Key()
        {
            var a = new Foo { X = 0, Y = 0 };
            var b = new Foo { X = 0, Y = 1 };

            Func<Foo, int> keySelector1 = f => f.X;
            Func<Foo, int> keySelector2 = f => f.Y;
            var comparer = XComparer.By(keySelector1).ThenByDescending(keySelector2);

            int expected = 1;
            int actual = comparer.Compare(a, b);
            Assert.Equal(expected, actual);
        }

        class Foo
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
