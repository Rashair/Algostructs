namespace Sorting.External;

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
