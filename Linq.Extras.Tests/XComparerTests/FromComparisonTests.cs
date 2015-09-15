using System;
using FluentAssertions;
using NUnit.Framework;

namespace Linq.Extras.Tests.XComparerTests
{
    [TestFixture]
    public class FromComparisonTests
    {
        [Test]
        public void FromComparison_Returns_A_Comparer_Based_On_A_Comparison()
        {
            var a = new Foo { Name = "foo" };
            var b = new Foo { Name = "bar" };

            var comparer = XComparer<Foo>.FromComparison((x, y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal));
            
            comparer.Compare(a, b).Should().BeGreaterThan(0);
        }

        class Foo
        {
            public string Name { get; set; }
        }
    }
}
