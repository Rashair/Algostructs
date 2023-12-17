using Strings.Searchers;

namespace Strings;

public class DsStringBuilder
{
    private readonly List<char> _values;

    public DsStringBuilder()
    {
        _values = [];
    }

    public DsStringBuilder(string str)
    {
        _values = [..str];
    }

    public int Length => _values.Count;

    public void Append(string str)
    {
        _values.AddRange(str);
    }

    public void Insert(int index, string str)
    {
        _values.InsertRange(index, str.AsSpan());
    }

    public void Remove(int startIndex, int length)
    {
        if (startIndex + length > _values.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        if (startIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(startIndex));
        }

        if (length <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        _values.RemoveRange(startIndex, length);
    }

    public void Replace(string pattern, string replacement)
    {
        var searcher = GetSearcher();
        var indexes = searcher.Search(pattern);
        if (indexes.Count == 0)
        {
            return;
        }

        if (replacement.Length > pattern.Length)
        {
            _values.EnsureCapacity(_values.Count + (replacement.Length - pattern.Length) * indexes.Count);
        }

        ReplaceAllPattern(indexes, pattern.Length, replacement);
    }

    private void ReplaceAllPattern(List<int> indexes, int patternLen, string replacement)
    {
        int indexShift = 0;
        foreach (int patternIndex in indexes)
        {
            var currentPatternInd = patternIndex + indexShift;
            _values.RemoveRange(currentPatternInd, patternLen);
            _values.InsertRange(currentPatternInd, replacement.AsSpan());
            indexShift += replacement.Length - patternLen;
        }
    }

    private ISearcher GetSearcher()
    {
        return new KnuthMorrisSearcher(_values);
    }

    public void Clear()
    {
        _values.Clear();
    }

    public override string ToString()
    {
        return string.Concat(_values);
    }
}
