using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Linq.Extras.Tests.XListTests
{
    [TestFixture]
    class SwapTests
    {
        [Test]
        public void Swap_Throws_If_List_Is_Null()
        {
            IList<int> list = null;
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => list.Swap(0, 1));
            ex.ParamName.Should().Be("list");
        }

        [Test]
        [TestCase(-1, 1, "index1")]
        [TestCase(3, 1, "index1")]
        [TestCase(int.MinValue, 1, "index1")]
        [TestCase(int.MaxValue, 1, "index1")]
        [TestCase(1, -1, "index2")]
        [TestCase(1, 3, "index2")]
        [TestCase(1, int.MinValue, "index2")]
        [TestCase(1, int.MaxValue, "index2")]
        public void Swap_Throws_If_Index_Is_Out_Of_Range(int index1, int index2, string name)
        {
            IList<int> list = new[] { 1, 2, 3 };
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => list.Swap(index1, index2));
            ex.ParamName.Should().Be(name);
        }

        [Test]
        [TestCase(new[] { 1, 2, 3 }, 0, 2, new[] { 3, 2, 1 })]
        [TestCase(new[] { 1, 2, 3 }, 0, 1, new[] { 2, 1, 3 })]
        [TestCase(new[] { 1, 2, 3 }, 1, 2, new[] { 1, 3, 2 })]
        [TestCase(new[] { 1, 2, 3 }, 0, 0, new[] { 1, 2, 3 })]
        public void Swap_Swaps_Positions_Of_Items(int[] input, int index1, int index2, int[] expected)
        {
            input.Swap(index1, index2);
            input.Should().Equal(expected);
        }
    }
}
