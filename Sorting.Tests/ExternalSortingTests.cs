using System.Text;

namespace Sorting.Tests;

public abstract class ExternalSortingTests
{
    private static readonly List<string> _files = new();

    protected readonly Random Random = new(3234);

    protected abstract IExternalSort InitSort();

    protected string GenerateSampleFile(long count, long limit = int.MaxValue)
    {
        var fileName = $"test-file-{Random.Next()}";
        using var fileWriter = new StreamWriter(fileName, true, Encoding.Default, ushort.MaxValue + 1);
        for (long numsIter = 0; numsIter < count; ++numsIter)
        {
            fileWriter.WriteLine(Random.NextInt64(limit));
        }

        _files.Add(fileName);

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
        foreach (var file in _files)
        {
            File.Delete(file);
        }
    }
}
