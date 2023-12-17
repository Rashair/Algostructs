namespace Sorting.Tests;

[TestFixture]
[TestOf(typeof(BucketSort<>))]
public class InsertionSortTests : SortingTests
{
    protected override ISort<int> InitSort()
    {
        return new InsertionSort<int>(c => c);
    }

    [TestCase(10_000, 300)]
    [TestCase(25_000, 2_000)]
    [TestCase(75_000, 15_000)]
    public override void Performance(int size, int timeLimit)
    {
        base.Performance(size, timeLimit);
    }
}
