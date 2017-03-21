using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class GroupUntilChangedTests
    {
        [Fact]
        public void GroupUntilChanged_Throws_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.GroupUntilChanged());
            ex.ParamName.Should().Be("source");
        }

        [Fact]
        public void GroupUntilChanged_Returns_Empty_Sequence_If_Source_Is_Empty()
        {
            var source = Enumerable.Empty<int>().ForbidMultipleEnumeration();
            var result = source.GroupUntilChanged();
            result.Should().BeEmpty();
        }

        [Fact]
        public void GroupUntilChanged_Returns_Groupings_Of_Adjacent_Elements()
        {
            var source = new[] { 1, 1, 1, 2, 3, 3, 1, 3, 2, 2 }.ForbidMultipleEnumeration();
            var result = source.GroupUntilChanged().ToArray();
            result.Length.Should().Be(6);
            result[0].Key.Should().Be(1);
            result[0].Should().Equal(1, 1, 1);
            result[1].Key.Should().Be(2);
            result[1].Should().Equal(2);
            result[2].Key.Should().Be(3);
            result[2].Should().Equal(3, 3);
            result[3].Key.Should().Be(1);
            result[3].Should().Equal(1);
            result[4].Key.Should().Be(3);
            result[4].Should().Equal(3);
            result[5].Key.Should().Be(2);
            result[5].Should().Equal(2, 2);
        }

        [Fact]
        public void GroupUntilChanged_Uses_Specified_Comparer()
        {
            var source = new[] { -1, 1, -1, 2, 3, -3, 1, 3, -2, 2 }.ForbidMultipleEnumeration();
            var comparer = XEqualityComparer<int>.By(Math.Abs);
            var result = source.GroupUntilChanged(comparer).ToArray();
            result.Length.Should().Be(6);
            result[0].Key.Should().Be(-1);
            result[0].Should().Equal(-1, 1, -1);
            result[1].Key.Should().Be(2);
            result[1].Should().Equal(2);
            result[2].Key.Should().Be(3);
            result[2].Should().Equal(3, -3);
            result[3].Key.Should().Be(1);
            result[3].Should().Equal(1);
            result[4].Key.Should().Be(3);
            result[4].Should().Equal(3);
            result[5].Key.Should().Be(-2);
            result[5].Should().Equal(-2, 2);
        }

        [Fact]
        public void GroupUntilChangedBy_Throws_If_Source_Is_Null()
        {
            IEnumerable<int> source = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.GroupUntilChangedBy(Math.Abs));
            ex.ParamName.Should().Be("source");
        }

        [Fact]
        public void GroupUntilChangedBy_Throws_If_KeySelector_Is_Null()
        {
            var source = Enumerable.Empty<int>().ForbidEnumeration();
            Func<int, int> keySelector = null;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => source.GroupUntilChangedBy(keySelector));
            ex.ParamName.Should().Be("keySelector");
        }

        [Fact]
        public void GroupUntilChangedBy_Returns_Empty_Sequence_If_Source_Is_Empty()
        {
            var source = Enumerable.Empty<Foo>().ForbidMultipleEnumeration();
            var result = source.GroupUntilChangedBy(f => f.X);
            result.Should().BeEmpty();
        }

        [Fact]
        public void GroupUntilChangedBy_Returns_Groupings_Of_Adjacent_Elements()
        {
            var source = new[]
                         {
                             new Foo(0, 1),
                             new Foo(0, 2),
                             new Foo(1, 3),
                             new Foo(1, 8),
                             new Foo(2, 5),
                             new Foo(0, 3),
                             new Foo(2, 0),
                             new Foo(2, 2)
                         }.ForbidMultipleEnumeration();

            var result = source.GroupUntilChangedBy(f => f.X).ToArray();
            result.Length.Should().Be(5);
            result[0].Key.Should().Be(0);
            result[0].Should().Equal(new Foo(0, 1), new Foo(0, 2));
            result[1].Key.Should().Be(1);
            result[1].Should().Equal(new Foo(1, 3), new Foo(1, 8));
            result[2].Key.Should().Be(2);
            result[2].Should().Equal(new Foo(2, 5));
            result[3].Key.Should().Be(0);
            result[3].Should().Equal(new Foo(0, 3));
            result[4].Key.Should().Be(2);
            result[4].Should().Equal(new Foo(2, 0), new Foo(2, 2));
        }

        [Fact]
        public void GroupUntilChangedBy_Uses_Specified_Comparer()
        {
            var source = new[]
                         {
                             new Foo(0, 1),
                             new Foo(0, 2),
                             new Foo(-1, 3),
                             new Foo(1, 8),
                             new Foo(2, 5),
                             new Foo(0, 3),
                             new Foo(2, 0),
                             new Foo(-2, 2)
                         }.ForbidMultipleEnumeration();

            var comparer = XEqualityComparer<int>.By(Math.Abs);
            var result = source.GroupUntilChangedBy(f => f.X, comparer).ToArray();
            result.Length.Should().Be(5);
            result[0].Key.Should().Be(0);
            result[0].Should().Equal(new Foo(0, 1), new Foo(0, 2));
            result[1].Key.Should().Be(-1);
            result[1].Should().Equal(new Foo(-1, 3), new Foo(1, 8));
            result[2].Key.Should().Be(2);
            result[2].Should().Equal(new Foo(2, 5));
            result[3].Key.Should().Be(0);
            result[3].Should().Equal(new Foo(0, 3));
            result[4].Key.Should().Be(2);
            result[4].Should().Equal(new Foo(2, 0), new Foo(-2, 2));
        }


        [ExcludeFromCodeCoverage]
        struct Foo
        {
            public Foo(int x, int y)
                : this()
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            // ReSharper disable once MemberCanBePrivate.Local
            // ReSharper disable once UnusedAutoPropertyAccessor.Local
            public int Y { get; }
        }

    }
}
