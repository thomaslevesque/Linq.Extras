using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Linq.Extras.Tests.XListTests
{
    [TestFixture]
    class SwapTests
    {
        [Test]
        public void Swap_Throws_If_List_Argument_Is_Null()
        {
            IList<int> list = null;
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => list.Swap(0, 1)).Assert().Verifies(e => e.ParamName == "list");
        }

        [Test]
        public void Swap_Throws_If_Index_Is_Out_Of_Range()
        {
            IList<int> list = new [] { 1, 2, 3};
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Swap(-1, 1)).Assert().Verifies(e => e.ParamName == "index1");
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Swap(3, 1)).Assert().Verifies(e => e.ParamName == "index1");
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Swap(int.MinValue, 1)).Assert().Verifies(e => e.ParamName == "index1");
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Swap(int.MaxValue, 1)).Assert().Verifies(e => e.ParamName == "index1");
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Swap(1, -1)).Assert().Verifies(e => e.ParamName == "index2");
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Swap(1, 3)).Assert().Verifies(e => e.ParamName == "index2");
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Swap(1, int.MinValue)).Assert().Verifies(e => e.ParamName == "index2");
            Assert.Throws<ArgumentOutOfRangeException>(() => list.Swap(1, int.MaxValue)).Assert().Verifies(e => e.ParamName == "index2");
        }

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
