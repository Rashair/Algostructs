using System.Text;

namespace Sorting.External;

public static class FileHelper
{
    public static string GetChunkFileName(long chunkNum)
    {
        return $"chunk-{chunkNum}.txt";
    }

    public static StreamWriter CreatePerformantStreamWriter(string filePath)
    {
        return new(filePath, false, Encoding.Default, ushort.MaxValue + 1);
    }
}
