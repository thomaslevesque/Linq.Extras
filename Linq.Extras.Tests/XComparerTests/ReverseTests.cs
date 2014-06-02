using System.Collections.Generic;
using NUnit.Framework;

namespace Linq.Extras.Tests.XComparerTests
{
    [TestFixture]
    class ReverseTests
    {
        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 1)]
        [TestCase(1, 0, -1)]
        [TestCase(int.MinValue, int.MaxValue, 1)]
        [TestCase(int.MinValue, 0, 1)]
        [TestCase(0, int.MaxValue, 1)]
        [TestCase(int.MaxValue, int.MinValue, -1)]
        [TestCase(0, int.MinValue, -1)]
        [TestCase(int.MaxValue, 0, -1)]
        public void Reverse_Returns_Comparer_With_Reverse_Logic(int x, int y, int expected)
        {
            var comparer = Comparer<int>.Default.Reverse();
            int actual = comparer.Compare(x, y);
            Assert.AreEqual(expected, actual);
        }
    }
}
