using System;
using System.Linq;
using FluentAssertions;
using Xunit;

#pragma warning disable CS0618

namespace Linq.Extras.Tests.XEnumerableTests
{

    public class BatchTests
    {
        [Fact]
        public void Batch_Throws_If_Argument_Is_Null()
        {
            var source = Enumerable.Empty<int>();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            TestHelper.AssertThrowsWhenArgumentNull(() => source.Batch(10));
        }

        [Fact]
        public void Batch_Throws_If_Size_Is_Negative_Or_Zero()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 }.ForbidEnumeration();
            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => source.Batch(-1));
            ex.ParamName.Should().Be("size");
            ex = Assert.Throws<ArgumentOutOfRangeException>(() => source.Batch(0));
            ex.ParamName.Should().Be("size");
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed
        }

        [Fact]
        public void Batch_Returns_Batches_Of_Specified_Size()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            var batches = source.Batch(2).ToArray();
            batches.Should().HaveCount(3);
            batches[0].Should().Equal(4, 8);
            batches[1].Should().Equal(15, 16);
            batches[2].Should().Equal(23, 42);
        }

        [Fact]
        public void Batch_Returns_Smaller_Last_Batch_If_Count_Not_Divisible_By_Size()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 }.ForbidMultipleEnumeration();
            var batches = source.Batch(4).ToArray();
            batches.Should().HaveCount(2);
            batches[0].Should().Equal(4, 8, 15, 16);
            batches[1].Should().Equal(23, 42);
        }
    }
}
