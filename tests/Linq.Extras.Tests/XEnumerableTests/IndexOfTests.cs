using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class IndexOfTests
    {
        [Fact]
        public void IndexOf_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<int>();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.IndexOf(42, null));
        }

        [Fact]
        public void IndexOf_Returns_MinusOne_If_Element_Is_Not_Found()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            int index = source.IndexOf(99);
            index.Should().Be(-1);
        }

        [Fact]
        public void IndexOf_Returns_Index_Of_First_Element_If_Found()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42, 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            int index = source.IndexOf(42);
            index.Should().Be(5);
        }

        [Fact]
        public void IndexOf_With_Predicate_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<int>();
            Func<int, bool> predicate = i => true;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.IndexOf(i => true));
        }

        [Fact]
        public void IndexOf_Returns_MinusOne_If_No_Element_Matches_Predicate()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            int index = source.IndexOf(i => i % 12 == 0);
            index.Should().Be(-1);
        }

        [Fact]
        public void IndexOf_Returns_Index_Of_First_Element_That_Matches_Predicate()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42, 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            int index = source.IndexOf(i => i % 7 == 0);
            index.Should().Be(5);
        }

        [Fact]
        public void LastIndexOf_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<int>();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.LastIndexOf(42, null));
        }

        [Fact]
        public void LastIndexOf_Returns_MinusOne_If_Element_Is_Not_Found()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            int index = source.LastIndexOf(99);
            index.Should().Be(-1);
        }

        [Fact]
        public void LastIndexOf_Returns_Index_Of_Last_Element_If_Found()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42, 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            int index = source.LastIndexOf(42);
            index.Should().Be(11);
        }

        [Fact]
        public void LastIndexOf_With_Predicate_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<int>();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.LastIndexOf(i => true));
        }

        [Fact]
        public void LastIndexOf_Returns_MinusOne_If_No_Element_Matches_Predicate()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            int index = source.LastIndexOf(i => i % 12 == 0);
            index.Should().Be(-1);
        }

        [Fact]
        public void LastIndexOf_Returns_Index_Of_Last_Element_That_Matches_Predicate()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42, 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            int index = source.LastIndexOf(i => i % 7 == 0);
            index.Should().Be(11);
        }
    }
}
