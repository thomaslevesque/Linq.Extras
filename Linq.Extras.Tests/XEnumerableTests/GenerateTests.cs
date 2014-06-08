using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Linq.Extras.Tests.XEnumerableTests
{
    [TestFixture]
    class GenerateTests
    {
        [Test]
        public void Generate_Throws_If_Generator_Is_Null()
        {
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => XEnumerable.Generate(0, null));
            ex.ParamName.Should().Be("generator");
        }

        [Test]
        public void Generate_By_Index_Throws_If_Generator_Is_Null()
        {
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => XEnumerable.Generate<int>(null));
            ex.ParamName.Should().Be("generator");
        }

        [Test]
        public void Generate_Returns_Sequence_Of_Values_Based_On_Previous_Value()
        {
            var sequence = XEnumerable.Generate(0, previous => previous + 2);
            var expected = new[] { 0, 2, 4, 6, 8 };
            var actual = sequence.Take(5);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Generate_By_Index_Returns_Sequence_Of_Values_Based_On_Index()
        {
            var sequence = XEnumerable.Generate(index => index * 2);
            var expected = new[] { 0, 2, 4, 6, 8 };
            var actual = sequence.Take(5);
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
