using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XListTests
{
    public class ShuffleTests
    {
        [Fact]
        public void Shuffle_Throws_If_Argument_Is_Null()
        {
            IList<int> list = new List<int>();
            TestHelper.AssertThrowsWhenArgumentNull(() => list.Shuffle(null));
        }

        // Since the Shuffle method is random by nature, the shuffled list could be in the same order as the original list;
        // this would be a valid result (just a special case), but it would fail this test. To avoid that, use constant
        // seeds to ensure consistent test results.
        [Theory]
        [InlineData(0)]
        [InlineData(42)]
        [InlineData(int.MaxValue)]
        public void Shuffle_With_Specified_Random_Shuffles_The_List(int seed)
        {
            var rnd = new Random(seed);

            var numbers = Enumerable.Repeat(rnd, 100).Select(r => r.Next()).ToList();
            var original = numbers.ToList();
            numbers.Shuffle(rnd);

            numbers.Should().BeEquivalentTo(original);
            numbers.Should().NotEqual(original);
        }

        // Since we don't control the Random seed, we can't prevent the shuffled list from being in the same order
        // as the original list. Hence, asserting anything about the result is pointless; just ensure the shuffled
        // list contains the same item as the original, in any order.
        [Fact]
        public void Shuffle_Without_Specified_Random_Shuffles_The_List()
        {
            var rnd = new Random();
            var numbers = Enumerable.Repeat(rnd, 100).Select(r => r.Next()).ToList();

            var original = numbers.ToList();
            numbers.Shuffle();

            numbers.Should().BeEquivalentTo(original);
        }

    }
}
