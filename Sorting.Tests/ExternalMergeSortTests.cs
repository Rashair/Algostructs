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

    [Category("LongRunning")]
    [TestCase(int.MaxValue / 16, Description = "1/16 of int max value")]
    [TestCase(int.MaxValue / 8, Description = "1/8 of int max value")]
    [TestCase(int.MaxValue / 4, Description = "1/4 of int max value")]
    public void TestMediumFile(int count)
    {
        var sort = InitSort();
        var file = GenerateSampleFile(count);

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
