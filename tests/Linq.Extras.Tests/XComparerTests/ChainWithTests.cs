using System.Collections.Generic;
using Xunit;
#if FEATURE_ICOMPARER_NULLABILITY
using System.Diagnostics.CodeAnalysis;
#endif

namespace Linq.Extras.Tests.XComparerTests
{
    public class ChainWithTests
    {
        [Fact]
        public void ChainWith_Uses_Next_Comparer_If_First_Returns_Zero()
        {
            var a = new Foo { X = 0, Y = 0 };
            var b = new Foo { X = 0, Y = 1 };

            var first = XComparer<Foo>.By(f => f.X);
            var next = Intercept(XComparer<Foo>.By(f => f.Y));
            var comparer = first.ChainWith(next);

            int expected = -1;
            int actual = comparer.Compare(a, b);
            Assert.Equal(expected, actual);

            Assert.Equal(1, next.CallCount);
        }

        [Fact]
        public void ChainWith_Doesnt_Use_Next_Comparer_If_First_Returns_NonZero()
        {
            var a = new Foo { X = 0, Y = 0 };
            var b = new Foo { X = 1, Y = 1 };

            var first = XComparer<Foo>.By(f => f.X);
            var next = Intercept(XComparer<Foo>.By(f => f.Y));
            var comparer = first.ChainWith(next);

            int expected = -1;
            int actual = comparer.Compare(a, b);
            Assert.Equal(expected, actual);

            Assert.Equal(0, next.CallCount);
        }

        [Fact]
        public void Chaining_Multiple_Comparers_Works_As_Well()
        {
            var a = new Foo { X = 0, Y = 0 , Z = 1};
            var b = new Foo { X = 0, Y = 0 , Z = 0};

            var first = XComparer<Foo>.By(f => f.X);
            var second = XComparer<Foo>.By(f => f.Y);
            var third = XComparer<Foo>.By(f => f.Z);

            // Test various ways of chaining

            // (first.second).third
            var comparer = first.ChainWith(second).ChainWith(third);
            int expected = 1;
            int actual = comparer.Compare(a, b);
            Assert.Equal(expected, actual);


            // first.(second.third)
            comparer = first.ChainWith(second.ChainWith(third));
            expected = 1;
            actual = comparer.Compare(a, b);
            Assert.Equal(expected, actual);

            // (first.second).(first.second)
            comparer = first.ChainWith(second).ChainWith(first.ChainWith(second));
            expected = 0;
            actual = comparer.Compare(a, b);
            Assert.Equal(expected, actual);
        }

        class Foo
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
        }

        static InterceptingComparer<T> Intercept<T>(IComparer<T> comparer)
        {
            return new InterceptingComparer<T>(comparer);
        }

        class InterceptingComparer<T> : IComparer<T>
        {
            private readonly IComparer<T> _comparer;

            public InterceptingComparer(IComparer<T>? comparer = null)
            {
                _comparer = comparer ?? Comparer<T>.Default;
            }

#if FEATURE_ICOMPARER_NULLABILITY
            public int Compare([AllowNull] T x, [AllowNull] T y)
#else
            public int Compare(T x, T y)
#endif
            {
                CallCount++;
                return _comparer.Compare(x, y);
            }

            public int CallCount { get; private set; }
        }
    }
}
