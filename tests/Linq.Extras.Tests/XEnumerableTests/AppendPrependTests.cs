#if !FEATURE_APPEND_PREPEND
using System.Linq;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class AppendPrependTests
    {
        [Fact]
        public void Append_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<int>();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.Append(42));
        }

        [Fact]
        public void Append_Adds_Item_At_End_Of_Sequence()
        {
            var input = new[] { 4, 8, 15, 16, 23 }.ForbidMultipleEnumeration();
            var item = 42;
            var expected = new[] { 4, 8, 15, 16, 23, 42 };
            var actual = input.Append(item);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Prepend_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<int>();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.Prepend(42));
        }

        [Fact]
        public void Prepend_Insert_Item_At_Beginning_Of_Sequence()
        {
            var input = new[] { 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            var item = 4;
            var expected = new[] { 4, 8, 15, 16, 23, 42 };
            var actual = input.Prepend(item);
            Assert.Equal(expected, actual);
        }
    }
}
#endif
