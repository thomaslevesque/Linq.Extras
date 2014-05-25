using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Linq.Extras.Tests
{
    [TestFixture]
    class MinMaxTests
    {
        [Test]
        public void Test_MaxBy()
        {
            var foos = GetFoos();
            foos.Shuffle();
            var fooWithMaxValue = foos.MaxBy(f => f.Value);
            var expected = "xyz";
            var actual = fooWithMaxValue.Value;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_MaxBy_EmptySequence()
        {
            var foos = new Foo[] { };
// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<InvalidOperationException>(() => foos.MaxBy(f => f.Value));
        }

        [Test]
        public void Test_MaxBy_WithComparer()
        {
            var foos = GetFoos();
            foos.Shuffle();
            var fooWithMaxValue = foos.MaxBy(f => f.Value, Comparer<string>.Default.Reverse());
            var expected = "abcd";
            var actual = fooWithMaxValue.Value;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_MinBy()
        {
            var foos = GetFoos();
            foos.Shuffle();
            var fooWithMinValue = foos.MinBy(f => f.Value);
            var expected = "abcd";
            var actual = fooWithMinValue.Value;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_MinBy_EmptySequence()
        {
            var foos = new Foo[] { };
// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<InvalidOperationException>(() => foos.MinBy(f => f.Value));
        }

        [Test]
        public void Test_MinBy_WithComparer()
        {
            var foos = GetFoos();
            foos.Shuffle();
            var fooWithMinValue = foos.MinBy(f => f.Value, Comparer<string>.Default.Reverse());
            var expected = "xyz";
            var actual = fooWithMinValue.Value;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Max_WithComparer()
        {
            var foos = GetFoos();
            foos.Shuffle();
            var fooWithMaxValue = foos.Max(new FooComparer());
            var expected = "xyz";
            var actual = fooWithMaxValue.Value;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Max_WithComparer_EmptySequence()
        {
            var foos = new Foo[] { };
// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<InvalidOperationException>(() => foos.Max(new FooComparer()));
        }

        [Test]
        public void Test_Min_WithComparer()
        {
            var foos = GetFoos();
            foos.Shuffle();
            var fooWithMinValue = foos.Min(new FooComparer());
            var expected = "abcd";
            var actual = fooWithMinValue.Value;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Min_WithComparer_EmptySequence()
        {
            var foos = new Foo[] { };
// ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            Assert.Throws<InvalidOperationException>(() => foos.Min(new FooComparer()));
        }

        private static IList<Foo> GetFoos()
        {
            return new List<Foo>
            {
                new Foo { Value = "abcd" },
                new Foo { Value = "efgh" },
                new Foo { Value = "ijkl" },
                new Foo { Value = "mnop" },
                new Foo { Value = "qrst" },
                new Foo { Value = "uvw" },
                new Foo { Value = "xyz" }
            };
        }

        class Foo
        {
            public string Value { get; set; }
        }

        class FooComparer : IComparer<Foo>
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
