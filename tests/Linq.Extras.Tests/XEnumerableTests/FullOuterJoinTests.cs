using System;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class FullOuterJoinTests
    {
        [Fact]
        public void FullOuterJoin_Throws_If_Argument_Null()
        {
            var left = XEnumerable.Empty<int>().ForbidEnumeration();
            var right = XEnumerable.Empty<int>().ForbidEnumeration();
            TestHelper.AssertThrowsWhenArgumentNull(
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                () => left.FullOuterJoin(right, x => x, y => y, (k, x, y) => 0, 0, 0, null));
        }

        [Fact]
        public void FullOuterJoin_Joins_Elements_Of_Both_Sequence()
        {
            var left = new[] { "hello", "!" };
            var right = new[] { "hello", "world", "!" };
            var result = left.FullOuterJoin(right, x => x.Length, y => y.Length, (len, x, y) => x + y);
            result.Should().Equal("hellohello", "helloworld", "!!");
        }

        [Fact]
        public void FullOuterJoin_Uses_Specified_Default_Value_For_Missing_Elements()
        {
            var left = new[] { "hello", "!" };
            var right = new[] { "hello", "world", "!!!" };
            var result = left.FullOuterJoin(right, x => x.Length, y => y.Length, (len, x, y) => x + y, "?", ".");
            result.Should().Equal("hellohello", "helloworld", "!.", "?!!!");
        }

        [Fact]
        public void FullOuterJoin_Uses_Specified_Key_Comparer()
        {
            var left = new[] { "hello", "!" };
            var right = new[] { "HELLO", "world" };
            var comparer = StringComparer.CurrentCultureIgnoreCase;
            var result = left.FullOuterJoin(right, x => x, y => y, (len, x, y) => x + y, "?", ".", comparer);
            result.Should().Equal("helloHELLO", "!.", "?world");
        }

    }
}
