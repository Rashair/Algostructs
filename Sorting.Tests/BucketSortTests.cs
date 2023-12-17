using System.Diagnostics;
using System.Numerics;

namespace Sorting.Tests;

[TestFixture]
[TestOf(typeof(BucketSort<>))]
public class BucketSortTests
{
    protected readonly Random Random = new Random(1234);

    public virtual ISort<int> InitSort()
    {
        return new BucketSort<int>(c => c);
    }

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

    [TestCase(10_000)]
    [TestCase(100_000)]
    [TestCase(10_000_000)]
    public void Performance(int size)
    {
        // Arrange
        var input = Enumerable.Range(0, size).Select(c => Random.Next()).ToArray();
        var sort = InitSort();

        // Act
        var st = Stopwatch.StartNew();
        var sortedInput = sort.Apply(input.ToArray());
        var elapsed = st.ElapsedMilliseconds;

        // Assert
        var stLib = Stopwatch.StartNew();
        Array.Sort(input);
        var elapsedLib=  stLib.ElapsedMilliseconds;

        Assert.That(input, Is.EqualTo(sortedInput));
        var logN = BitOperations.Log2((uint)size);
        Assert.That(elapsed, Is.LessThan(elapsedLib)
            .Or
            .EqualTo(elapsedLib).Within(size * (1 + logN))); // can be log(n) times worse
    }
}
