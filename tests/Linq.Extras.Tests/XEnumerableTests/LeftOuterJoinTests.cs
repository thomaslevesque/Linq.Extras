using System;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class LeftOuterJoinTests
    {
        [Fact]
        public void LeftOuterJoin_Throws_If_Argument_Null()
        {
            var left = XEnumerable.Empty<int>().ForbidEnumeration();
            var right = XEnumerable.Empty<int>().ForbidEnumeration();
            TestHelper.AssertThrowsWhenArgumentNull(
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                () => left.LeftOuterJoin(right, x => x, y => y, (x, y) => 0, 0, null));
        }

        [Fact]
        public void LeftOuterJoin_Joins_Elements_Of_Both_Sequence()
        {
            var left = new[] { "hello", "!"};
            var right = new[] { "hello", "world", "!" };
            var result = left.LeftOuterJoin(right, x => x.Length, y => y.Length, (x, y) => x + y, string.Empty);
            result.Should().Equal("hellohello", "helloworld", "!!");
        }

        [Fact]
        public void LeftOuterJoin_Uses_Specified_Default_Value_For_Elements_Missing_In_Right()
        {
            var left = new[] { "hello", "!" };
            var right = new[] { "hello", "world" };
            var result = left.LeftOuterJoin(right, x => x.Length, y => y.Length, (x, y) => x + y, "?");
            result.Should().Equal("hellohello", "helloworld", "!?");
        }

        [Fact]
        public void LeftOuterJoin_Uses_Specified_Key_Comparer()
        {
            var left = new[] { "hello", "!" };
            var right = new[] { "HELLO", "world" };
            var comparer = StringComparer.CurrentCultureIgnoreCase;
            var result = left.LeftOuterJoin(right, x => x, y => y, (x, y) => x + y, "?", comparer);
            result.Should().Equal("helloHELLO", "!?");
        }
    }
}
