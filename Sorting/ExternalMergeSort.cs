using System.Text;

namespace Sorting;

public interface IExternalSort
{
    void Apply(string filePath);
}

public class ExternalMergeSort : IExternalSort
{
    private const int NumbersLimit = 128_000_000; // 512M

    public void Apply(string filePath)
    {
        var numbersBatch = new List<long>(NumbersLimit);

        var chunksNum = GenerateSortedChunks(filePath, numbersBatch);

        MergeSortedChunks(chunksNum);
        MoveChunkToFile(0, filePath);
    }

    private static long GenerateSortedChunks(string filePath, List<long> numbersBatch)
    {
        long chunksNum = 0;
        long? number;
        using var numbersReader = new NumbersReader(filePath);
        while ((number = numbersReader.Read()) != null)
        {
            numbersBatch.Add(number.Value);
            if (numbersBatch.Count == NumbersLimit)
            {
                WriteSortedChunk(numbersBatch, chunksNum);
                ++chunksNum;
            }
        }

        if (numbersBatch.Count > 0)
        {
            WriteSortedChunk(numbersBatch, chunksNum);
            ++chunksNum;
        }

        return chunksNum;
    }

    private static void WriteSortedChunk(List<long> chunk, long chunksNum)
    {
        chunk.Sort();
        WriteChunk(chunk, chunksNum);
        chunk.Clear();
    }

    private static void WriteChunk(IList<long> chunk, long chunkNum)
    {
        using var sw = CreatePerformantStreamWriter(GetChunkFileName(chunkNum));
        foreach (var number in chunk)
        {
            sw.WriteLine(number);
        }
    }

    private static StreamWriter CreatePerformantStreamWriter(string filePath)
    {
        return new(filePath, false, Encoding.Default, ushort.MaxValue + 1);
    }

    private static string GetChunkFileName(long chunkNum) => $"chunk-{chunkNum}.txt";

    private void MergeSortedChunks(long chunksNum)
    {
        int chunksIter = 0;
        while (chunksNum > 1)
        {
            if (chunksIter == chunksNum - 1) // uneven chunks num
            {
                MoveChunkToFile(chunksIter, GetChunkFileName(chunksIter / 2));
            }

            if (chunksIter > chunksNum - 2)
            {
                chunksIter = 0;
                chunksNum /= 2;
                continue;
            }

            MergeChunks(chunksIter, chunksIter + 1, chunksIter / 2);
            chunksIter += 2;
        }
    }

    private static void MergeChunks(int chunkA, int chunkB, int chunkTarget)
    {
        var tempFileName = $"chunk-{chunkTarget}-temp.txt";
        using (var targetWriter = CreatePerformantStreamWriter(tempFileName))
        using (var bReader = new NumbersReader(GetChunkFileName(chunkB)))
        using (var aReader = new NumbersReader(GetChunkFileName(chunkA)))
        {
            WriteSorted(aReader, bReader, targetWriter);

            WriteToEnd(aReader, targetWriter);
            WriteToEnd(bReader, targetWriter);
        }

        File.Delete(GetChunkFileName(chunkA));
        File.Delete(GetChunkFileName(chunkB));

        MoveChunkToFile(chunkTarget, tempFileName);
    }

    private static void WriteSorted(NumbersReader aReader, NumbersReader bReader, StreamWriter targetWriter)
    {
        for (long? a = aReader.Read(), b = bReader.Read();
             a != null && b != null;)
        {
            if (a < b)
            {
                targetWriter.WriteLine(a);
                a = aReader.Read();
            }
            else
            {
                targetWriter.WriteLine(b);
                b = bReader.Read();
            }
        }
    }

    private static void WriteToEnd(NumbersReader reader, StreamWriter targetWriter)
    {
        for (long? k = reader.Read(); k != null; k = reader.Read())
        {
            targetWriter.WriteLine(k);
        }
    }

    private static void MoveChunkToFile(int chunkNum, string filePath)
    {
        File.Delete(filePath);
        File.Move(GetChunkFileName(chunkNum), filePath);
    }
}

public sealed class NumbersReader : IDisposable
{
    private readonly TextReader _reader;

    public NumbersReader(string filePath)
    {
        _reader = new StreamReader(filePath);
    }

    public long? Read()
    {
        var line = _reader.ReadLine();
        return line == null ? null : long.Parse(line);
    }

    public void Dispose()
    {
        _reader.Dispose();
    }
}
