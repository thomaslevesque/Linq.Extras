using System.Collections;
using FluentAssertions;
using NUnit.Framework;

namespace Linq.Extras.Tests.XEnumerableTests
{
    [TestFixture]
    class MiscTests
    {
        [Test]
        public void Unit_Returns_Sequence_With_Single_Element()
        {
            var result = XEnumerable.Unit(42);
            (result as ICollection).Should().BeNull("Sequence should not be a collection");
            result.Should().HaveCount(1);
        }

        [Test]
        public void Empty_Returns_Empty_Sequence()
        {
            var result = XEnumerable.Empty<int>();
            (result as ICollection).Should().BeNull("Sequence should not be a collection");
            result.Should().BeEmpty();
        }
    }
}
