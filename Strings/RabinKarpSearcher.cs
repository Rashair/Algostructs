namespace Strings;

internal class RabinKarpSearcher(IReadOnlyList<char> _values)
{
    private const int NumberBase = 128;

    public List<int> Search(string pattern)
    {
        // Pre compute hashes
        const ulong mod = int.MaxValue;
        ulong max = (ulong)Math.Pow(NumberBase, pattern.Length - 1) % mod;
        var valueOfNextXChars = new ulong[_values.Count - pattern.Length + 1];

        int nonApplicableRangeStart = valueOfNextXChars.Length;
        ulong currentValue = ComputeValuesHash(nonApplicableRangeStart, pattern.Length - 1, mod);
        for (int i = nonApplicableRangeStart - 1; i >= 0; --i)
        {
            currentValue = (currentValue * NumberBase + _values[i]) % mod;
            valueOfNextXChars[i] = currentValue;
            currentValue -= _values[i + pattern.Length - 1] * max % mod;
        }

        // Search
        var indexes = new List<int>();
        var patternHash = ComputeHash(pattern, 0, pattern.Length, mod);
        for (int i = 0; i < valueOfNextXChars.Length; ++i)
        {
            if (valueOfNextXChars[i] != patternHash)
            {
                continue;
            }

            if(IsEqualToValuesInRange(pattern, i))
            {
                indexes.Add(i);
            }
        }

        return indexes;
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
            result = result * NumberBase % mod + (ulong)getValue(i);
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
