using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Linq.Extras.Tests.XEnumerableTests
{
    [TestFixture]
    class BatchTests
    {
        [Test]
        public void Batch_Throws_If_Source_Is_Null()
        {
            IEnumerable<string> source = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.Batch(10));
            ex.ParamName.Should().Be("source");
        }

        [Test]
        public void Batch_Throws_If_Size_Is_Negative_Or_Zero()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 };
            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => source.Batch(-1));
            ex.ParamName.Should().Be("size");
            ex = Assert.Throws<ArgumentOutOfRangeException>(() => source.Batch(0));
            ex.ParamName.Should().Be("size");
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed
        }

        [Test]
        public void Batch_Returns_Batches_Of_Specified_Size()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 };
            var batches = source.Batch(2).ToArray();
            batches.Should().HaveCount(3);
            batches[0].Should().BeEquivalentTo(new[] { 4, 8 });
            batches[1].Should().BeEquivalentTo(new[] { 15, 16 });
            batches[2].Should().BeEquivalentTo(new[] { 23, 42 });
        }

        [Test]
        public void Batch_Returns_Smaller_Last_Batch_If_Count_Not_Divisible_By_Size()
        {
            var source = new[] { 4, 8, 15, 16, 23, 42 };
            var batches = source.Batch(4).ToArray();
            batches.Should().HaveCount(2);
            batches[0].Should().BeEquivalentTo(new[] { 4, 8, 15, 16 });
            batches[1].Should().BeEquivalentTo(new[] { 23, 42 });
        }
    }
}
