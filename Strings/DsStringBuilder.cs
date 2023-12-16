using System.Text;

namespace Strings;

public class DsStringBuilder
{
    private readonly StringBuilder _builder;

    public DsStringBuilder()
    {
        _builder = new();
    }

    public DsStringBuilder(string str)
    {
        _builder = new(str);
    }

    public void Append(string str)
    {
        _builder.Append(str);
    }

    public void Insert(int index, string str)
    {
        _builder.Insert(index, str);
    }

    public void Remove(int startIndex, int length)
    {
        _builder.Remove(startIndex, length);
    }

    public void Replace(string pattern, string replacement)
    {
        _builder.Replace(pattern, replacement);
    }

    public void Clear()
    {
        _builder.Clear();
    }

    public int Length => _builder.Length;

    public override string ToString()
    {
        return _builder.ToString();
    }
}
