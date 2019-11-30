using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class WithIndexTests
    {
        [Fact]
        public void WithIndex_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<int>();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.WithIndex());
        }

        [Fact]
        public void WithIndex_Associates_An_Index_With_Each_Item()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            var result = source.WithIndex();
            result.Should().Equal(
                new []{
                new ItemWithIndex<int>(4, 0),
                new ItemWithIndex<int>(8, 1),
                new ItemWithIndex<int>(15, 2),
                new ItemWithIndex<int>(16, 3),
                new ItemWithIndex<int>(23, 4),
                new ItemWithIndex<int>(42, 5)
                }, HaveSameIndexAndItem);
        }

        [Fact]
        public void WithIndexForArray_Throws_If_Argument_Is_Null()
        {
            var source = new int[0];
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.WithIndex());
        }

        [Fact]
        public void WithIndexForArray_Associates_An_Index_With_Each_Item()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 };
            var result = source.WithIndex();
            result.Should().Equal(
                new[]{
                new ItemWithIndex<int>(4, 0),
                new ItemWithIndex<int>(8, 1),
                new ItemWithIndex<int>(15, 2),
                new ItemWithIndex<int>(16, 3),
                new ItemWithIndex<int>(23, 4),
                new ItemWithIndex<int>(42, 5)
                }, HaveSameIndexAndItem);
        }

        [Fact]
        public void WithIndexForList_Throws_If_Argument_Is_Null()
        {
            var source = new List<int>();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.WithIndex());
        }

        [Fact]
        public void WithIndexForList_Associates_An_Index_With_Each_Item()
        {
            var source = new List<int> { 4, 8, 15, 16, 23, 42 };
            var result = source.WithIndex();
            result.Should().Equal(
                new[]{
                new ItemWithIndex<int>(4, 0),
                new ItemWithIndex<int>(8, 1),
                new ItemWithIndex<int>(15, 2),
                new ItemWithIndex<int>(16, 3),
                new ItemWithIndex<int>(23, 4),
                new ItemWithIndex<int>(42, 5)
                }, HaveSameIndexAndItem);
        }

        [Fact]
        public void WithoutIndex_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<ItemWithIndex<int>>();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.WithoutIndex());
        }

        [Fact]
        public void WithoutIndex_Removes_Index_From_Sequence()
        {
            var source = new[]
                         {
                             new ItemWithIndex<int>(4, 0),
                             new ItemWithIndex<int>(8, 1),
                             new ItemWithIndex<int>(15, 2),
                             new ItemWithIndex<int>(16, 3),
                             new ItemWithIndex<int>(23, 4),
                             new ItemWithIndex<int>(42, 5)
                         }.ForbidMultipleEnumeration();
            var result = source.WithoutIndex();
            result.Should().Equal(4, 8, 15, 16, 23, 42);
        }

        [Fact]
        public void Deconstruct_Returns_Item_And_Index()
        {
            var indexedItem = new ItemWithIndex<int>(42, 5);
            var (item, index) = indexedItem;
            item.Should().Be(42);
            index.Should().Be(5);
        }

        static bool HaveSameIndexAndItem<T>(ItemWithIndex<T> x, ItemWithIndex<T> y)
        {
            return x.Index == y.Index && EqualityComparer<T>.Default.Equals(x.Item, y.Item);
        }
    }
}
