using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Linq.Extras.Tests.XListTests
{
    [TestFixture]
    class ShuffleTests
    {
        [Test]
        public void Shuffle_Throws_If_List_Argument_Is_Null()
        {
            IList<int> list = null;
            // ReSharper disable AssignNullToNotNullAttribute
            // ReSharper disable ConvertClosureToMethodGroup
            Assert.Throws<ArgumentNullException>(() => list.Shuffle());
            Assert.Throws<ArgumentNullException>(() => list.Shuffle(new Random()));
            // ReSharper restore AssignNullToNotNullAttribute
            // ReSharper restore ConvertClosureToMethodGroup
        }

        [Test]
        public void Shuffle_Throws_If_Random_Argument_Is_Null()
        {
            IList<int> list = new[] { 1, 2, 3 };
            Random rnd = null;
            // ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => list.Shuffle(rnd));
        }

        // Since the Shuffle method is random by nature, the shuffled list could be in the same order as the original list;
        // this would be a valid result (just a special case), but it would fail this test. To avoid that, use constant
        // seeds to ensure consistent test results.
        [Test]
        [TestCase(0)]
        [TestCase(42)]
        [TestCase(int.MaxValue)]
        public void Shuffle_With_Specified_Random_Shuffles_The_List(int seed)
        {
            var rnd = new Random(seed);

            var numbers = Enumerable.Repeat(rnd, 100).Select(r => r.Next()).ToList();
            var original = numbers.ToList();
            numbers.Shuffle(rnd);

            CollectionAssert.AreEquivalent(original, numbers);
            CollectionAssert.AreNotEqual(original, numbers);
        }

        // Since we don't control the Random seed, we can't prevent the shuffled list from being in the same order
        // as the original list. Hence, asserting anything about the result is pointless; just ensure the shuffled
        // list contains the same item as the original, in any order.
        [Test]
        public void Shuffle_Without_Specified_Random_Shuffles_The_List()
        {
            var rnd = new Random();
            var numbers = Enumerable.Repeat(rnd, 100).Select(r => r.Next()).ToList();

            var original = numbers.ToList();
            numbers.Shuffle();

            CollectionAssert.AreEquivalent(original, numbers);
        }

    }
}
