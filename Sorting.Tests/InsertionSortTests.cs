namespace Sorting.Tests;

[TestFixture]
[TestOf(typeof(InsertionSort<>))]
public class InsertionSortTests : SortingTests
{
    protected override ISort<int> InitSort()
    {
        return new InsertionSort<int>(c => c);
    }

    [TestCase(10_000, 400)]
    [TestCase(25_000, 2_000)]
    [TestCase(50_000, 10_000)]
    public void Performance(int size, int timeLimit)
    {
        base.Performance(size, timeLimit);
    }

    [TestCase(10_000, 300)]
    [TestCase(25_000, 2_000)]
    [TestCase(50_000, 10_000)]
    public void PerformanceSmallRange(int size, int timeLimit)
    {
        base.Performance(size, timeLimit, 100);
    }
}
