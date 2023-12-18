namespace Sorting.Tests;

[TestFixture]
[TestOf(typeof(BucketSort<>))]
public class BucketSortTests : SortingTests
{
    protected override ISort<int> InitSort()
    {
        return new BucketSort<int>(c => c);
    }

    [TestCase(10_000, 50)]
    [TestCase(100_000, 100)]
    [TestCase(10_000_000, 4_000)]
    public void Performance(int size, int timeLimit)
    {
        base.Performance(size, timeLimit);
    }

    [TestCase(10_000, 50)]
    [TestCase(100_000, 100)]
    [TestCase(10_000_000, 4_000)]
    public void PerformanceSmallRange(int size, int timeLimit)
    {
        base.Performance(size, timeLimit, 100);
    }
}
