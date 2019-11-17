using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class WithIndexTests
    {
        [Fact]
        public void WithIndex_Throws_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentNullException>(() => source.WithIndex());
            ex.ParamName.Should().Be("source");
        }

        [Fact]
        public void WithIndex_Associates_An_Index_With_Each_Item()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            var result = source.WithIndex();
            result.Should().Equal(
                new []{
                new Indexed<int>(4, 0),
                new Indexed<int>(8, 1),
                new Indexed<int>(15, 2),
                new Indexed<int>(16, 3),
                new Indexed<int>(23, 4),
                new Indexed<int>(42, 5)
                }, HaveSameIndexAndItem);
        }

        [Fact]
        public void WithoutIndex_Throws_If_Source_Is_Null()
        {
            IEnumerable<IIndexedItem<int>> source = null;
            // ReSharper disable once AssignNullToNotNullAttribute
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentNullException>(() => source.WithoutIndex());
            ex.ParamName.Should().Be("source");
        }

        [Fact]
        public void WithoutIndex_Removes_Index_From_Sequence()
        {
            var source = new[]
                         {
                             new Indexed<int>(4, 0),
                             new Indexed<int>(8, 1),
                             new Indexed<int>(15, 2),
                             new Indexed<int>(16, 3),
                             new Indexed<int>(23, 4),
                             new Indexed<int>(42, 5)
                         }.ForbidMultipleEnumeration();
            var result = source.WithoutIndex();
            result.Should().Equal(4, 8, 15, 16, 23, 42);
        }

        [Fact]
        public void Deconstruct_Throws_If_IndexedItem_Is_Null()
        {
            IIndexedItem<int> indexedItem = null;
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => indexedItem.Deconstruct(out _, out _));
            ex.ParamName.Should().Be(nameof(indexedItem));
        }

        [Fact]
        public void Deconstruct_Returns_Item_And_Index()
        {
            var indexedItem = new Indexed<int>(42, 5);
            var (item, index) = indexedItem;
            item.Should().Be(42);
            index.Should().Be(5);
        }

        static bool HaveSameIndexAndItem<T>(IIndexedItem<T> x, IIndexedItem<T> y)
        {
            return x.Index == y.Index && EqualityComparer<T>.Default.Equals(x.Item, y.Item);
        }

        [ExcludeFromCodeCoverage]
        class Indexed<T> : IIndexedItem<T>
        {
            public Indexed(T item, int index)
            {
                Item = item;
                Index = index;
            }

            public int Index { get; }

            public T Item { get; }
        }
    }
}
