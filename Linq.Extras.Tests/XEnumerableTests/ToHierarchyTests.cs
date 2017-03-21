using System.Linq;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class ToHierarchyTests
    {
        [Fact]
        public void ToHierarchy_Throws_If_Argument_Is_Null()
        {
            var source = XEnumerable.Empty<Foo>().ForbidEnumeration();
            TestHelper.AssertThrowsWhenArgumentNull(
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                () => source.ToHierarchy(f => f.Id, f => f.ParentId, null),
                "source", "idSelector", "parentIdSelector");
        }

        [Fact]
        public void ToHierarchy_Returns_Hierarchy_Of_Nodes()
        {
            var source = new[]
                         {
                             new Foo(1, "1"),
                             new Foo(2, "1.1", 1),
                             new Foo(3, "1.2", 1),
                             new Foo(4, "1.1.1", 2),
                             new Foo(5, "2"),
                             new Foo(6, "2.1", 5)
                         }.ForbidMultipleEnumeration();

            var hierarchy = source.ToHierarchy(f => f.Id, f => f.ParentId).ToArray();
            
            hierarchy.Should().HaveCount(2);
            
            hierarchy[0].Item.Name.Should().Be("1");
            hierarchy[0].Parent.Should().BeNull();
            hierarchy[0].Children.Should().HaveCount(2);

            hierarchy[0].Children[0].Item.Name.Should().Be("1.1").Should();
            hierarchy[0].Children[0].Parent.Should().BeSameAs(hierarchy[0]);
            hierarchy[0].Children[0].Children.Should().HaveCount(1);

            hierarchy[0].Children[1].Item.Name.Should().Be("1.2");
            hierarchy[0].Children[1].Parent.Should().BeSameAs(hierarchy[0]);
            hierarchy[0].Children[1].Children.Should().BeEmpty();

            hierarchy[0].Children[0].Children[0].Item.Name.Should().Be("1.1.1");
            hierarchy[0].Children[0].Children[0].Parent.Should().BeSameAs(hierarchy[0].Children[0]);
            hierarchy[0].Children[0].Children[0].Children.Should().BeEmpty();
            
            hierarchy[1].Item.Name.Should().Be("2");
            hierarchy[1].Parent.Should().BeNull();
            hierarchy[1].Children.Should().HaveCount(1);

            hierarchy[1].Children[0].Item.Name.Should().Be("2.1");
            hierarchy[1].Children[0].Parent.Should().BeSameAs(hierarchy[1]);
            hierarchy[1].Children[0].Children.Should().BeEmpty();
        }

        class Foo
        {
            public Foo(int id, string name, int? parentId = null)
            {
                Id = id;
                ParentId = parentId;
                Name = name;
            }
            public int Id { get; }
            public int? ParentId { get; }
            public string Name { get; }
        }
    }
}
