using System;
using FluentAssertions;
using Xunit;

namespace Linq.Extras.Tests.XEnumerableTests
{
    public class RankByTests
    {
        [Fact]
        public void RankBy_Throws_When_Argument_Is_Null()
        {
            var source = XEnumerable.Empty<int>().ForbidEnumeration();
            TestHelper.AssertThrowsWhenArgumentNull(
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                () => source.RankBy(x => x, (x, r) => x, null));
        }

        [Fact]
        public void RankBy_Associates_Item_With_Rank_NoDraws()
        {
            var source = new[]
                         {
                             new Player("Alice", 42),
                             new Player("Bob", 25),
                             new Player("Charlie", 75),
                             new Player("David", 17),
                             new Player("Emily", 31)
                         }.ForbidMultipleEnumeration();

            var result = source.RankBy(p => p.Score, (player, rank) => $"{rank}. {player.Name}");
            result.Should().Equal(
                "1. David",
                "2. Bob",
                "3. Emily",
                "4. Alice",
                "5. Charlie");
        }

        [Fact]
        public void RankBy_Uses_Specified_Key_Comparer()
        {
            var source = new[]
                         {
                             new Player("Alice", -42),
                             new Player("Bob", -25),
                             new Player("Charlie", 75),
                             new Player("David", -17),
                             new Player("Emily", 31)
                         }.ForbidMultipleEnumeration();
            var keyComparer = XComparer<int>.By(Math.Abs);

            var result = source.RankBy(p => p.Score, (player, rank) => $"{rank}. {player.Name}", keyComparer);
            result.Should().Equal(
                "1. David",
                "2. Bob",
                "3. Emily",
                "4. Alice",
                "5. Charlie");
        }

        [Fact]
        public void RankBy_Associates_Item_With_Rank_WithDraws()
        {
            var source = new[]
                         {
                             new Player("Alice", 42),
                             new Player("Bob", 25),
                             new Player("Charlie", 25),
                             new Player("David", 17),
                             new Player("Emily", 42)
                         }.ForbidMultipleEnumeration();

            var result = source.RankBy(p => p.Score, (player, rank) => $"{rank}. {player.Name}");
            result.Should().Equal(
                "1. David",
                "2. Bob",
                "2. Charlie",
                "4. Alice",
                "4. Emily");
        }

        [Fact]
        public void RankByDescending_Throws_When_Argument_Is_Null()
        {
            var source = XEnumerable.Empty<int>().ForbidEnumeration();
            TestHelper.AssertThrowsWhenArgumentNull(
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                () => source.RankByDescending(x => x, (x, r) => x, null));
        }

        [Fact]
        public void RankByDescending_Associates_Item_With_Rank_NoDraws()
        {
            var source = new[]
                         {
                             new Player("Alice", 42),
                             new Player("Bob", 25),
                             new Player("Charlie", 75),
                             new Player("David", 17),
                             new Player("Emily", 31)
                         }.ForbidMultipleEnumeration();

            var result = source.RankByDescending(p => p.Score, (player, rank) => $"{rank}. {player.Name}");
            result.Should().Equal(
                "1. Charlie",
                "2. Alice",
                "3. Emily",
                "4. Bob",
                "5. David");
        }

        [Fact]
        public void RankByDescending_Uses_Specified_Key_Comparer()
        {
            var source = new[]
                         {
                             new Player("Alice", -42),
                             new Player("Bob", 25),
                             new Player("Charlie", -75),
                             new Player("David", -17),
                             new Player("Emily", 31)
                         }.ForbidMultipleEnumeration();
            var keyComparer = XComparer<int>.By(Math.Abs);

            var result = source.RankByDescending(p => p.Score, (player, rank) => $"{rank}. {player.Name}", keyComparer);
            result.Should().Equal(
                "1. Charlie",
                "2. Alice",
                "3. Emily",
                "4. Bob",
                "5. David");
        }

        [Fact]
        public void RankByDescending_Associates_Item_With_Rank_WithDraws()
        {
            var source = new[]
                         {
                             new Player("Alice", 42),
                             new Player("Bob", 25),
                             new Player("Charlie", 25),
                             new Player("David", 17),
                             new Player("Emily", 42)
                         }.ForbidMultipleEnumeration();

            var result = source.RankByDescending(p => p.Score, (player, rank) => $"{rank}. {player.Name}");
            result.Should().Equal(
                "1. Alice",
                "1. Emily",
                "3. Bob",
                "3. Charlie",
                "5. David");
        }

        [Fact]
        public void DenseRankBy_Throws_When_Argument_Is_Null()
        {
            var source = XEnumerable.Empty<int>().ForbidEnumeration();
            TestHelper.AssertThrowsWhenArgumentNull(
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                () => source.DenseRankBy(x => x, (x, r) => x, null));
        }

        [Fact]
        public void DenseRankBy_Associates_Item_With_Rank_NoDraws()
        {
            var source = new[]
                         {
                             new Player("Alice", 42),
                             new Player("Bob", 25),
                             new Player("Charlie", 75),
                             new Player("David", 17),
                             new Player("Emily", 31)
                         }.ForbidMultipleEnumeration();

            var result = source.DenseRankBy(p => p.Score, (player, rank) => $"{rank}. {player.Name}");
            result.Should().Equal(
                "1. David",
                "2. Bob",
                "3. Emily",
                "4. Alice",
                "5. Charlie");
        }

        [Fact]
        public void DenseRankBy_Uses_Specified_Key_Comparer()
        {
            var source = new[]
                         {
                             new Player("Alice", -42),
                             new Player("Bob", -25),
                             new Player("Charlie", 75),
                             new Player("David", -17),
                             new Player("Emily", 31)
                         }.ForbidMultipleEnumeration();
            var keyComparer = XComparer<int>.By(Math.Abs);

            var result = source.DenseRankBy(p => p.Score, (player, rank) => $"{rank}. {player.Name}", keyComparer);
            result.Should().Equal(
                "1. David",
                "2. Bob",
                "3. Emily",
                "4. Alice",
                "5. Charlie");
        }

        [Fact]
        public void DenseRankBy_Associates_Item_With_Rank_WithDraws()
        {
            var source = new[]
                         {
                             new Player("Alice", 42),
                             new Player("Bob", 25),
                             new Player("Charlie", 25),
                             new Player("David", 17),
                             new Player("Emily", 42)
                         }.ForbidMultipleEnumeration();

            var result = source.DenseRankBy(p => p.Score, (player, rank) => $"{rank}. {player.Name}");
            result.Should().Equal(
                "1. David",
                "2. Bob",
                "2. Charlie",
                "3. Alice",
                "3. Emily");
        }

        [Fact]
        public void DenseRankByDescending_Throws_When_Argument_Is_Null()
        {
            var source = XEnumerable.Empty<int>().ForbidEnumeration();
            TestHelper.AssertThrowsWhenArgumentNull(
                // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
                () => source.DenseRankByDescending(x => x, (x, r) => x, null));
        }

        [Fact]
        public void DenseRankByDescending_Associates_Item_With_Rank_NoDraws()
        {
            var source = new[]
                         {
                             new Player("Alice", 42),
                             new Player("Bob", 25),
                             new Player("Charlie", 75),
                             new Player("David", 17),
                             new Player("Emily", 31)
                         }.ForbidMultipleEnumeration();

            var result = source.DenseRankByDescending(p => p.Score, (player, rank) => $"{rank}. {player.Name}");
            result.Should().Equal(
                "1. Charlie",
                "2. Alice",
                "3. Emily",
                "4. Bob",
                "5. David");
        }

        [Fact]
        public void DenseRankByDescending_Uses_Specified_Key_Comparer()
        {
            var source = new[]
                         {
                             new Player("Alice", -42),
                             new Player("Bob", 25),
                             new Player("Charlie", -75),
                             new Player("David", -17),
                             new Player("Emily", 31)
                         }.ForbidMultipleEnumeration();
            var keyComparer = XComparer<int>.By(Math.Abs);

            var result = source.DenseRankByDescending(p => p.Score, (player, rank) => $"{rank}. {player.Name}", keyComparer);
            result.Should().Equal(
                "1. Charlie",
                "2. Alice",
                "3. Emily",
                "4. Bob",
                "5. David");
        }

        [Fact]
        public void DenseRankByDescending_Associates_Item_With_Rank_WithDraws()
        {
            var source = new[]
                         {
                             new Player("Alice", 42),
                             new Player("Bob", 25),
                             new Player("Charlie", 25),
                             new Player("David", 17),
                             new Player("Emily", 42)
                         }.ForbidMultipleEnumeration();

            var result = source.DenseRankByDescending(p => p.Score, (player, rank) => $"{rank}. {player.Name}");
            result.Should().Equal(
                "1. Alice",
                "1. Emily",
                "2. Bob",
                "2. Charlie",
                "3. David");
        }

        class Player
        {
            public Player(string name, int score)
            {
                Name = name;
                Score = score;
            }

            public string Name { get; }
            public int Score { get; }
        }
    }
}
