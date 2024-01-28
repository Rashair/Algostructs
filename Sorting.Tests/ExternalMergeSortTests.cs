using Sorting.External;

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
        const uint count = 10_000;
        var file = GenerateSampleFile(count, 1_000);

        // Act
        sort.Apply(file);

        AssertSorted(file, count);
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

        AssertSorted(file, (uint)count);
    }

    [Test]
    [Category("LongRunning")]
    public void TestHugeFile()
    {
        var sort = InitSort();
        const uint count = uint.MaxValue;
        var file = GenerateSampleFile(count, int.MaxValue / 2);

        // Act
        sort.Apply(file);

        AssertSorted(file, count);
    }
}
