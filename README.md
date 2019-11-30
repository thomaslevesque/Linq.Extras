# Linq.Extras

[![NuGet version](https://img.shields.io/nuget/v/Linq.Extras.svg?logo=nuget)](https://www.nuget.org/packages/Linq.Extras)
[![AppVeyor build](https://img.shields.io/appveyor/ci/thomaslevesque/linq-extras.svg?logo=appveyor)](https://ci.appveyor.com/project/thomaslevesque/linq-extras)
[![AppVeyor tests](https://img.shields.io/appveyor/tests/thomaslevesque/linq-extras.svg?logo=appveyor)](https://ci.appveyor.com/project/thomaslevesque/linq-extras/build/tests)
[![Docs (online)](https://img.shields.io/badge/docs-online%20(HTML)-blue.svg)](https://thomaslevesque.github.io/Linq.Extras/api/)
[![Docs (offline)](https://img.shields.io/badge/docs-offline%20(CHM)-blue.svg)](https://thomaslevesque.github.io/Linq.Extras/api/Linq.Extras.chm)

A set of extension and helper methods to complement the ones from `System.Linq.Enumerable`.

Some of these methods are just shortcuts for common Linq operations (e.g. `Append`, `IsNullOrEmpty`), or improvements to
existing Linq methods (e.g. specify default value for `FirstOrDefault`, specify comparer for `Max`). Others do more
complex things that have no equivalent in standard Linq (`RankBy`, `DistinctUntilChanged`).

Here are some methods of interest:

### `DistinctBy`, `IntersectBy`, `UnionBy`, `ExceptBy`, `SequenceEqualBy`

Same as `Distinct`, `Intersect`, `Union`, `Except`, `SequenceEqual`, but allow you to specify a key for equality comparison.

```csharp
var result = items.DistinctBy(i => i.Name);
```

### `DistinctUntilChanged`

Returns a sequence with distinct contiguous items (i.e. removes contiguous duplicates).

```csharp
var input = new[] { 1, 1, 1, 2, 3, 3, 1, 3, 2, 2, 1 };
var result = input.DistinctUntilChanged(); // 1, 2, 3, 1, 3, 2, 1
```

This is the enumerable equivalent of the [`Observable.DistinctUntilChanged`](http://msdn.microsoft.com/en-us/library/system.reactive.linq.observable.distinctuntilchanged%28v=vs.103%29.aspx) method from Rx.

### `MinBy`, `MaxBy`

Return the item of a sequence that has the min or max value for the specified key.

```csharp
var winner = players.MaxBy(p => p.Score);
Console.WriteLine("The winner is {0} with {1} points!", winner.Name, winner.Score);
```

Unlike the well known approach of sorting the list and taking the first item, this method doesn't need sorting and operates in O(n).

### `RankBy`, `DenseRankBy`

These methods associate a rank with each item of a collection, based on the specified key. The difference between the two is the same as between the `RANK` and `DENSE_RANK` functions in SQL:
`RankBy` leaves "holes" in the ranks if some items are equal, while `DenseRankBy` does not.

This code:

```csharp
var ranking = players.RankByDescending(player => player.Score, (player, rank) => string.Format("{0}. {1} ({2})", rank, player.Name, player.Score));
```

Produces the following results

```
1. Joe (42)
2. Liz (23)
2. Ben (23)
4. Ann (16)
5. Bob (15)
```

### `LeftOuterJoin`, `RightOuterJoin`, `FullOuterJoin`

As the names imply.

The first two are for those who always forget how to do an outer join with Linq ;).

`FullOuterJoin` fills a gap, though, since there is no built-in way to do it with Linq.

```csharp
var result = left.OuterJoin(right, x => x.Id, y => y.Id, (id, x, y) => new { x, y });
```

### `ToHierarchy`

Transforms a flat sequence of items into a hierarchy. Each node is of type `INode<T>` and exposes its children and parent.

```csharp
var roots = items.ToHierarchy(i => i.Id, i => i.ParentId);
```

### `Flatten`

Transforms a hierarchy of objects to a flat sequence.

```csharp
var flat = roots.Flatten(node => node.Children, TreeTraversalMode.DepthFirst);
```
