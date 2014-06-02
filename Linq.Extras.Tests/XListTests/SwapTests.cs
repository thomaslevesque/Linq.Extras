using NUnit.Framework;

namespace Linq.Extras.Tests.XListTests
{
    [TestFixture]
    class SwapTests
    {
        [Test]
        [TestCase(new[] { 1, 2, 3 }, 0, 2, new[] { 3, 2, 1 })]
        [TestCase(new[] { 1, 2, 3 }, 0, 1, new[] { 2, 1, 3 })]
        [TestCase(new[] { 1, 2, 3 }, 1, 2, new[] { 1, 3, 2 })]
        [TestCase(new[] { 1, 2, 3 }, 0, 0, new[] { 1, 2, 3 })]
        public void Swap_Swaps_Positions_Of_Items(int[] input, int index1, int index2, int[] expected)
        {
            input.Swap(index1, index2);
            CollectionAssert.AreEqual(input, expected);
        }
    }
}
