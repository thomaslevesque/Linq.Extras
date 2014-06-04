using System.Collections.Generic;
using NUnit.Framework;

namespace Linq.Extras.Tests.XComparerTests
{
    [TestFixture]
    class MinMaxTests
    {
        [Test]
        public void Max_Returns_Greater_Arg_According_To_Comparer()
        {
            var comparer = Comparer<int>.Default;
            int expected = 42;
            int actual = comparer.Max(3, 42);
            Assert.AreEqual(expected, actual);

            actual = comparer.Max(42, 3);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Min_Returns_Lesser_Arg_According_To_Comparer()
        {
            var comparer = Comparer<int>.Default;
            int expected = 42;
            int actual = comparer.Min(99, 42);
            Assert.AreEqual(expected, actual);

            actual = comparer.Min(42, 99);
            Assert.AreEqual(expected, actual);
        }
    }
}
