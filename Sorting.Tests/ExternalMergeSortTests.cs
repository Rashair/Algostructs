namespace Sorting.Tests;

[TestFixture]
[TestOf(typeof(ExternalMergeSort))]
public class ExternalMergeSortTests : ExternalSortingTests
{
    protected override IExternalSort InitSort()
    {
        return new ExternalMergeSort();
    }

    [Test]
    public void TestSmallFile()
    {
        var sort = InitSort();
        var file = GenerateSampleFile(10_000, 1_000);

        // Act
        sort.Apply(file);

        AssertSorted(file);
    }

    [Test]
    [Category("LongRunning")]
    public void TestMediumFile()
    {
        var sort = InitSort();
        var file = GenerateSampleFile(int.MaxValue / 4);

        // Act
        sort.Apply(file);

        AssertSorted(file);
    }

    [Test]
    [Category("LongRunning")]
    public void TestHugeFile()
    {
        var sort = InitSort();
        var file = GenerateSampleFile(uint.MaxValue, long.MaxValue);

        // Act
        sort.Apply(file);

        AssertSorted(file);
    }
}
