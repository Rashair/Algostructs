namespace Searching.Strings;

public class KnuthMorrisSearcher : ISearcher
{
    private readonly IReadOnlyList<char> _text;

    public KnuthMorrisSearcher(IReadOnlyList<char> text)
    {
        _text = text;
    }

    public int Length => _text.Count;

    /// <summary>
    ///  Find all indexes of pattern in text. Omits overlapping patterns.
    /// </summary>
    public List<int> Search(string pattern)
    {
        if (pattern.Length > _text.Count)
        {
            return [];
        }

        var lps = ComputeLps(pattern);
        var indexes = new List<int>();
        int textIndex = 0;
        int patternIndex = 0;
        while (textIndex < _text.Count)
        {
            if (pattern[patternIndex] == _text[textIndex])
            {
                ++patternIndex;
                ++textIndex;
                if (patternIndex == pattern.Length)
                {
                    indexes.Add(textIndex - pattern.Length);
                    patternIndex = 0; // would be lps[patternIndex - 1] if we wanted overlapping patterns
                }
            }
            else if (patternIndex > 0)
            {
                // shift in pattern, no shift in text
                patternIndex = lps[patternIndex - 1];
            }
            else
            {
                ++textIndex;
            }
        }

        return indexes;
    }

    private static int[] ComputeLps(string pattern)
    {
        int index = 1; // lps[0] = 0;
        int longestPrefixSuffix = 0;
        var lps = new int[pattern.Length];
        while (index < pattern.Length)
        {
            if (pattern[index] == pattern[longestPrefixSuffix])
            {
                ++longestPrefixSuffix;
                lps[index] = longestPrefixSuffix;
                ++index;
            }
            else if (longestPrefixSuffix > 0)
            {
                longestPrefixSuffix = lps[longestPrefixSuffix - 1];
            }
            else
            {
                ++index;
            }
        }

        return lps;
    }
}
