using System;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XComparerTests
{
    public class FromComparisonTests
    {
        [Fact]
        public void FromComparison_Returns_A_Comparer_Based_On_A_Comparison()
        {
            var a = new Foo("foo");
            var b = new Foo("bar");

            var comparer = XComparer<Foo>.FromComparison((x, y) => string.Compare(x.Name, y.Name, StringComparison.Ordinal));
            
            comparer.Compare(a, b).Should().BeGreaterThan(0);
        }

        class Foo
        {
            public Foo(string name)
            {
                Name = name;
            }

            public string Name { get; }
        }
    }
}
