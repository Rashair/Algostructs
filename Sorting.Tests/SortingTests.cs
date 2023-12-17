using System.Diagnostics;

namespace Sorting.Tests;

[TestFixture]
public abstract class SortingTests
{
    protected readonly Random Random = new(1234);

    protected abstract ISort<int> InitSort();

    [Test]
    public void ReverseSorted()
    {
        // Arrange
        var input = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
        var sort = InitSort();

        // Act
        var sortedInput = sort.Apply(input.ToArray());

        // Assert
        Array.Sort(input);
        Assert.That(input, Is.EqualTo(sortedInput));
    }

    [Test]
    public void AlreadySorted()
    {
        // Arrange
        var input = new[] { 5, 6, 7, 8, 9, 10 };
        var sort = InitSort();

        // Act
        var sortedInput = sort.Apply(input.ToArray());

        // Assert
        Array.Sort(input);
        Assert.That(input, Is.EqualTo(sortedInput));
    }

    [Test]
    public void Unsorted()
    {
        // Arrange
        var input = new[] { 5, 6, 1, 2, 4, 5, 9, -1, -15 };
        var sort = InitSort();

        // Act
        var sortedInput = sort.Apply(input.ToArray());

        // Assert
        Array.Sort(input);
        Assert.That(input, Is.EqualTo(sortedInput));
    }

    [Test]
    public void SameElements()
    {
        // Arrange
        var input = Enumerable.Repeat(1213, 1_000).ToArray();
        var sort = InitSort();

        // Act
        var sortedInput = sort.Apply(input.ToArray());

        // Assert
        Array.Sort(input);
        Assert.That(input, Is.EqualTo(sortedInput));
    }

    [Test]
    public void MultipleSimilarElements()
    {
        // Arrange
        var input = Enumerable.Repeat(-1, 1_500)
            .Concat(Enumerable.Repeat(15, 1_200))
            .Concat(Enumerable.Repeat(-1, 1_300))
            .Concat(Enumerable.Repeat(int.MinValue, 104))
            .Concat(Enumerable.Repeat(int.MaxValue, 105))
            .Concat(Enumerable.Repeat(1024, 1_00))
            .OrderBy(r => Random.Next())
            .ToArray();
        var sort = InitSort();

        // Act
        var sortedInput = sort.Apply(input.ToArray());

        // Assert
        Array.Sort(input);
        Assert.That(input, Is.EqualTo(sortedInput));
    }

    [Test]
    public void SingleElement()
    {
        // Arrange
        var input = new[] { 1 };
        var sort = InitSort();

        // Act
        var sortedInput = sort.Apply(input.ToArray());

        // Assert
        Array.Sort(input);
        Assert.That(input, Is.EqualTo(sortedInput));
    }

    [Test]
    public void Empty()
    {
        // Arrange
        var input = Array.Empty<int>();
        var sort = InitSort();

        // Act
        var sortedInput = sort.Apply(input.ToArray());

        // Assert
        Array.Sort(input);
        Assert.That(input, Is.EqualTo(sortedInput));
    }

    public virtual void Performance(int size, int timeLimitMs)
    {
        // Arrange
        var input = Enumerable.Range(0, size).Select(_ => Random.Next()).ToArray();
        var sort = InitSort();

        // Act
        var st = Stopwatch.StartNew();
        var sortedInput = sort.Apply(input.ToArray());
        var elapsed = st.ElapsedMilliseconds + 1;

        // Assert
        var stLib = Stopwatch.StartNew();
        Array.Sort(input, Comparer<int>.Create((a, b) => a.CompareTo(b)));
        var elapsedLib = stLib.ElapsedMilliseconds + 1;

        Assert.That(input, Is.EqualTo(sortedInput));

        Assert.That(elapsed, Is.LessThan(timeLimitMs));

        Assert.Pass(elapsed < elapsedLib
            ? $"Sort was {Math.Round((elapsedLib - elapsed) / (double) elapsed * 100)}% better than the lib version"
            : elapsed > elapsedLib
                ? $"Sort was {Math.Round((elapsed - elapsedLib) / (double) elapsedLib * 100)}% worse than the lib version"
                : "Sort was comparable to the lib version"
        );
    }
}
