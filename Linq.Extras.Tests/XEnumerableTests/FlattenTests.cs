using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Linq.Extras.Tests.XEnumerableTests
{
    [TestFixture]
    class FlattenTests
    {
        [Test]
        public void Flatten_Throws_If_Source_Is_Null()
        {
            IEnumerable<Foo> source = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.Flatten(f => f.Children, TreeTraversalMode.DepthFirst));
            ex.ParamName.Should().Be("source");
        }

        [Test]
        public void Flatten_Throws_If_ChildrenSelector_Is_Null()
        {
            IEnumerable<Foo> source = Enumerable.Empty<Foo>().ForbidEnumeration();
            Func<Foo, IEnumerable<Foo>> childrenSelector = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.Flatten(childrenSelector, TreeTraversalMode.DepthFirst));
            ex.ParamName.Should().Be("childrenSelector");
        }

        [Test]
        public void Flatten_Throws_If_ResultSelector_Is_Null()
        {
            var source = Enumerable.Empty<Foo>().ForbidEnumeration();
            Func<Foo, int> resultSelector = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.Flatten(f => f.Children, TreeTraversalMode.DepthFirst, resultSelector));
            ex.ParamName.Should().Be("resultSelector");
        }

        [Test]
        public void Flatten_Throws_If_ResultSelector_With_Level_Is_Null()
        {
            var source = Enumerable.Empty<Foo>().ForbidEnumeration();
            Func<Foo, int, int> resultSelector = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.Flatten(f => f.Children, TreeTraversalMode.DepthFirst, resultSelector));
            ex.ParamName.Should().Be("resultSelector");
        }

        [Test]
        public void Flatten_Throws_If_TraversalMode_Is_Invalid()
        {
            var source = Enumerable.Empty<Foo>().ForbidEnumeration();
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => source.Flatten(f => f.Children, (TreeTraversalMode)42));
            ex.ParamName.Should().Be("traversalMode");
        }

        [Test]
        public void Flatten_Returns_Flat_Sequence_Of_Nodes_DepthFirst()
        {
            var source = GetFoos().ForbidMultipleEnumeration();
            var result = source.Flatten(f => f.Children, TreeTraversalMode.DepthFirst);
            var expected = new[] { 1, 2, 3, 4, 5, 6, 7 };
            var actual = result.Select(f => f.Id);
            actual.Should().Equal(expected);
        }

        [Test]
        public void Flatten_Returns_Flat_Sequence_Of_Nodes_BreadthFirst()
        {
            var source = GetFoos().ForbidMultipleEnumeration();
            var result = source.Flatten(f => f.Children, TreeTraversalMode.BreadthFirst);
            var expected = new[] { 1, 6, 2, 3, 7, 4, 5 };
            var actual = result.Select(f => f.Id);
            actual.Should().Equal(expected);
        }

        [Test]
        public void Flatten_With_ResultSelector_Returns_Flat_Sequence_DepthFirst()
        {
            var source = GetFoos().ForbidMultipleEnumeration();
            var actual = source.Flatten(f => f.Children, TreeTraversalMode.DepthFirst, f => f.Id);
            var expected = new[] { 1, 2, 3, 4, 5, 6, 7 };
            actual.Should().Equal(expected);
        }

        [Test]
        public void Flatten_With_ResultSelector_Returns_Flat_Sequence_BreadthFirst()
        {
            var source = GetFoos().ForbidMultipleEnumeration();
            var actual = source.Flatten(f => f.Children, TreeTraversalMode.BreadthFirst, f => f.Id);
            var expected = new[] { 1, 6, 2, 3, 7, 4, 5 };
            actual.Should().Equal(expected);
        }

        [Test]
        public void Flatten_With_ResultSelector_With_Level_Returns_Flat_Sequence_DepthFirst()
        {
            var source = GetFoos().ForbidMultipleEnumeration();
            var actual = source.Flatten(f => f.Children, TreeTraversalMode.DepthFirst, (f, level) => new { f.Id, Level = level});
            IEnumerable expected = new[]
                           {
                               new { Id = 1, Level = 0 },
                               new { Id = 2, Level = 1 },
                               new { Id = 3, Level = 1 },
                               new { Id = 4, Level = 2 },
                               new { Id = 5, Level = 2 },
                               new { Id = 6, Level = 0 },
                               new { Id = 7, Level = 1 },
                           };
            actual.Should().Equal(expected);
        }

        [Test]
        public void Flatten_With_ResultSelector_With_Level_Returns_Flat_Sequence_BreadthFirst()
        {
            var source = GetFoos().ForbidMultipleEnumeration();
            var actual = source.Flatten(f => f.Children, TreeTraversalMode.BreadthFirst, (f, level) => new { f.Id, Level = level });
            IEnumerable expected = new[]
                           {
                               new { Id = 1, Level = 0 },
                               new { Id = 6, Level = 0 },
                               new { Id = 2, Level = 1 },
                               new { Id = 3, Level = 1 },
                               new { Id = 7, Level = 1 },
                               new { Id = 4, Level = 2 },
                               new { Id = 5, Level = 2 },
                           };
            actual.Should().Equal(expected);
        }

        private static IEnumerable<Foo> GetFoos()
        {
            return new[]
                   {
                       new Foo
                       {
                           Id = 1,
                           Children = new[]
                                      {
                                          new Foo { Id = 2 },
                                          new Foo
                                          {
                                              Id = 3,
                                              Children = new[]
                                                         {
                                                             new Foo { Id = 4 },
                                                             new Foo { Id = 5 }
                                                         }
                                          }
                                      }
                       },
                       new Foo
                       {
                           Id = 6,
                           Children = new[]
                                      {
                                          new Foo { Id = 7 }
                                      }
                       }
                   };
        }

        class Foo
        {
            public Foo() { Children = new Foo[0]; }
            public int Id { get; set; }
            public IList<Foo> Children { get; set; }
        }
    }
}
