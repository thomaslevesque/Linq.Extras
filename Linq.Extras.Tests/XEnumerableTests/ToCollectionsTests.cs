using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Linq.Extras.Tests.XEnumerableTests
{
    [TestFixture]
    class ToCollectionsTests
    {
        [Test]
        public void ToQueue_Throws_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentNullException>(() => source.ToQueue());
            ex.ParamName.Should().Be("source");
        }

        [Test]
        public void ToQueue_Returns_A_Queue_With_The_Same_Items_As_Source()
        {
            var items = new[] { 4, 8, 15, 16, 23, 42 };
            var source = items.ForbidMultipleEnumeration();
            var queue = source.ToQueue();
            queue.Should().Equal(items);
        }

        [Test]
        public void ToStack_Throws_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentNullException>(() => source.ToStack());
            ex.ParamName.Should().Be("source");
        }

        [Test]
        public void ToStack_Returns_A_Stack_With_The_Same_Items_As_Source()
        {
            var items = new[] { 4, 8, 15, 16, 23, 42 };
            var source = items.ForbidMultipleEnumeration();
            var stack = source.ToStack();
            stack.Should().Equal(items.Reverse());
        }

        [Test]
        public void ToHashSet_Throws_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentNullException>(() => source.ToHashSet());
            ex.ParamName.Should().Be("source");
        }

        [Test]
        public void ToHashSet_Returns_A_HashSet_With_The_Same_Items_As_Source()
        {
            var items = new[] { 4, 8, 15, 16, 23, 42 };
            var source = items.ForbidMultipleEnumeration();
            var hashSet = source.ToHashSet();
            hashSet.Should().Equal(items);
        }

        [Test]
        public void ToHashSet_Uses_The_Provided_Comparer()
        {
            var items = new[] { 4, -8, 15, 16, -23, 42 };
            var source = items.ForbidMultipleEnumeration();
            var comparer = XEqualityComparer<int>.By(Math.Abs);
            var hashSet = source.ToHashSet(comparer);
            hashSet.Should().Equal(items, comparer.Equals);
        }

        [Test]
        public void ToLinkedList_Throws_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentNullException>(() => source.ToLinkedList());
            ex.ParamName.Should().Be("source");
        }

        [Test]
        public void ToLinkedList_Returns_A_LinkedList_With_The_Same_Items_As_Source()
        {
            var items = new[] { 4, 8, 15, 16, 23, 42 };
            var source = items.ForbidMultipleEnumeration();
            var linkedList = source.ToLinkedList();
            linkedList.Should().Equal(items);
        }
    }
}
