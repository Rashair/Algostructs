using System.Text;

namespace Sorting.Tests;

public abstract class ExternalSortingTests
{
    private static readonly List<string> FilesToCleanUp = [];
    private const int CleanUpCountLimit = (int)1e8;

    protected readonly Random Random = new(3234);

    protected abstract IExternalSort InitSort();

    protected string GenerateSampleFile(long count, long limit = int.MaxValue)
    {
        var fileName = $"test-file-c{count}-lim{limit}.txt";
        if (File.Exists(fileName))
        {
            return fileName;
        }

        using var fileWriter = new StreamWriter(fileName, true, Encoding.Default, ushort.MaxValue + 1);
        for (long numsIter = 0; numsIter < count; ++numsIter)
        {
            fileWriter.WriteLine(Random.NextInt64(limit));
        }

        if (count < CleanUpCountLimit)
        {
            FilesToCleanUp.Add(fileName);
        }

        return fileName;
    }

    public void AssertSorted(string fileName)
    {
        using var reader = new StreamReader(fileName);
        var prev = long.MinValue;
        while (reader.ReadLine() is { } line)
        {
            if (long.TryParse(line, out var current))
            {
                Assert.That(current, Is.GreaterThanOrEqualTo(prev));
                prev = current;
            }
            else
            {
                Assert.Fail($"Failed to parse line: {line}");
            }
        }
    }


    [OneTimeTearDown]
    public static void TearDown()
    {
        foreach (var file in FilesToCleanUp)
        {
            File.Delete(file);
        }
    }
}
