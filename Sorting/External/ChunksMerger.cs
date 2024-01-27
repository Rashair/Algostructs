using static Sorting.External.FileHelper;

namespace Sorting.External;

public class ChunksMerger
{
    private readonly int _chunksNum;
    private readonly object[] _chunkLocks;

    public ChunksMerger(int chunksNum)
    {
        _chunksNum = chunksNum;
        _chunkLocks = Enumerable.Range(0, chunksNum).Select(o => new object()).ToArray();
    }

    public void MergeSortedChunks()
    {
        var chunksIter = _chunksNum;
        while (chunksIter > 1)
        {
            Parallel.ForEach(GetChunksRange(chunksIter), TryMergeChunks);

            if (chunksIter % 2 == 1) // uneven chunks num
            {
                var lastChunk = chunksIter - 1;
                MoveChunkToFile(lastChunk, GetChunkFileName(lastChunk / 2));
            }

            chunksIter /= 2;
        }
    }

    private static IEnumerable<(int chunkA, int chunkB)> GetChunksRange(int chunksNum)
    {
        for (int i = 0; i < chunksNum - 1; i += 2)
        {
            yield return (i, i + 1);
        }
    }

    private void TryMergeChunks((int chunkA, int chunkB) range)
    {
        try
        {
            MergeChunks(range.chunkA, range.chunkB, range.chunkA / 2);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in [{range.chunkA}, {range.chunkB}] => {range.chunkA / 2}" + ex);
            throw;
        }
    }

    private void MergeChunks(int chunkA, int chunkB, int chunkTarget)
    {
        var tempFileName = $"chunk-{chunkTarget}-temp.txt";
        lock (_chunkLocks[chunkA]) lock (_chunkLocks[chunkB])
        {
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
        }


        lock (_chunkLocks[chunkTarget])
        {
            File.Move(tempFileName, GetChunkFileName(chunkTarget));
        }
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
