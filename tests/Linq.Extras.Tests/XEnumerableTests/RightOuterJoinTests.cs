using System;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class RightOuterJoinTests
    {
        [Fact]
        public void RightOuterJoin_Throws_If_Argument_Null()
        {
            var left = XEnumerable.Empty<int>().ForbidEnumeration();
            var right = XEnumerable.Empty<int>().ForbidEnumeration();
            TestHelper.AssertThrowsWhenArgumentNull(
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                () => left.RightOuterJoin(right, x => x, y => y, (x, y) => 0, 0, null));
        }

        [Fact]
        public void RightOuterJoin_Joins_Elements_Of_Both_Sequence()
        {
            var left = new[] { "hello", "world", "!" };
            var right = new[] { "hello", "!" };
            var result = left.RightOuterJoin(right, x => x.Length, y => y.Length, (x, y) => x + y, string.Empty);
            result.Should().Equal("hellohello", "worldhello", "!!");
        }

        [Fact]
        public void RightOuterJoin_Uses_Specified_Default_Value_For_Elements_Missing_In_Left()
        {
            var left = new[] { "hello", "world" };
            var right = new[] { "hello", "!" };
            var result = left.RightOuterJoin(right, x => x.Length, y => y.Length, (x, y) => x + y, "?");
            result.Should().Equal("hellohello", "worldhello", "?!");
        }

        [Fact]
        public void RightOuterJoin_Uses_Specified_Key_Comparer()
        {
            var left = new[] { "HELLO", "world" };
            var right = new[] { "hello", "!" };
            var comparer = StringComparer.CurrentCultureIgnoreCase;
            var result = left.RightOuterJoin(right, x => x, y => y, (x, y) => x + y, "?", comparer);
            result.Should().Equal("HELLOhello", "?!");
        }
    }
}
