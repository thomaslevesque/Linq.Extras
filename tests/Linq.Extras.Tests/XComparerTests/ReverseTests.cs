using System.Collections.Generic;
using Xunit;

namespace Linq.Extras.Tests.XComparerTests
{
    public class ReverseTests
    {
        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 0, -1)]
        [InlineData(int.MinValue, int.MaxValue, 1)]
        [InlineData(int.MinValue, 0, 1)]
        [InlineData(0, int.MaxValue, 1)]
        [InlineData(int.MaxValue, int.MinValue, -1)]
        [InlineData(0, int.MinValue, -1)]
        [InlineData(int.MaxValue, 0, -1)]
        public void Reverse_Returns_Comparer_With_Reverse_Logic(int x, int y, int expected)
        {
            var comparer = Comparer<int>.Default.Reverse();
            int actual = comparer.Compare(x, y);
            Assert.Equal(expected, actual);
        }
    }
}
