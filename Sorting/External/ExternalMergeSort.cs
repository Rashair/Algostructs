using static Sorting.External.FileHelper;

namespace Sorting.External;

public interface IExternalSort
{
    void Apply(string filePath);
}

public class ExternalMergeSort : IExternalSort
{
    private const int NumbersLimit = 32_000_000; // 256M

    public void Apply(string filePath)
    {
        try
        {
            var numbersBatch = new List<long>(NumbersLimit);
            var chunksNum = GenerateSortedChunks(filePath, numbersBatch);

            var chunksMerger = new ChunksMerger(chunksNum);
            chunksMerger.MergeSortedChunks();
            MoveChunkToFile(0, filePath);
        }
        catch (Exception)
        {
            CleanUpChunks();
            throw;
        }
    }

    private static int GenerateSortedChunks(string filePath, List<long> numbersBatch)
    {
        int chunksNum = 0;
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

    private static void MoveChunkToFile(int chunkNum, string filePath)
    {
        File.Delete(filePath);
        File.Move(GetChunkFileName(chunkNum), filePath);
    }

    private static void CleanUpChunks()
    {
        var chunkPattern = GetChunkFileName(123).Replace("123", "*");
        foreach (var chunkFile in Directory.GetFiles(".", chunkPattern))
        {
            File.Delete(chunkFile);
        }
    }
}
