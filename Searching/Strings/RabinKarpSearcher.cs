namespace Searching.Strings;

public class RabinKarpSearcher : ISearcher
{
    private const int NumberBase = 128;
    private readonly IReadOnlyList<char> _values;

    public RabinKarpSearcher(IReadOnlyList<char> values)
    {
        _values = values;
    }

    public int Length => _values.Count;

    public List<int> Search(string pattern)
    {
        const ulong mod = int.MaxValue;
        ulong[] valueOfNextXChars = ComputeHashes(pattern.Length, mod);

        // Search
        var indexes = new List<int>();
        var patternHash = ComputeHash(pattern, 0, pattern.Length, mod);
        for (int i = 0; i < valueOfNextXChars.Length; ++i)
        {
            if (valueOfNextXChars[i] != patternHash)
            {
                continue;
            }

            if (IsEqualToValuesInRange(pattern, i))
            {
                indexes.Add(i);
            }
        }

        return indexes;
    }

    private ulong[] ComputeHashes(int patternLength, ulong mod)
    {
        ulong max = (ulong) Math.Pow(NumberBase, patternLength - 1) % mod;
        var valueOfNextXChars = new ulong[_values.Count - patternLength + 1];
        int nonApplicableRangeStart = valueOfNextXChars.Length;
        ulong currentValue = ComputeValuesHash(nonApplicableRangeStart, patternLength - 1, mod);
        for (int i = nonApplicableRangeStart - 1; i >= 0; --i)
        {
            currentValue = (currentValue * NumberBase + _values[i]) % mod;
            valueOfNextXChars[i] = currentValue;
            currentValue -= _values[i + patternLength - 1] * max % mod;
        }

        return valueOfNextXChars;
    }

    private ulong ComputeValuesHash(int start, int length, ulong mod = 0)
    {
        if (start + length > _values.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        return ComputeHash(i => _values[i], start, length, mod);
    }

    private ulong ComputeHash(string value, int start, int length, ulong mod)
    {
        if (start + length > _values.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        return ComputeHash(i => value[i], start, length, mod);
    }

    private static ulong ComputeHash(Func<int, int> getValue, int start, int length, ulong mod)
    {
        ulong result = 0;
        for (int i = start + length - 1; i >= start; --i)
        {
            result = result * NumberBase % mod + (ulong) getValue(i);
        }

        return result;
    }

    private bool IsEqualToValuesInRange(string pattern, int start)
    {
        if (start + pattern.Length > _values.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(start));
        }

        for (int k = start; k < start + pattern.Length; ++k)
        {
            if (pattern[k - start] != _values[k])
            {
                return false;
            }
        }

        return true;
    }
}
