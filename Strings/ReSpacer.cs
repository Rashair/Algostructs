using System.Text;

namespace Strings;

public class ReSpacer
{
    private readonly HashSet<string> _dictionary;
    private readonly string? _document;

    private int _minUnrecognizedChars;
    private List<int> _spacesPositions;

    public ReSpacer(HashSet<string> dictionary, string document)
    {
        _dictionary = dictionary;
        _document = document;
        _spacesPositions = new();
        _minUnrecognizedChars = _document.Length;
    }


    public string? ReSpace()
    {
        if (string.IsNullOrEmpty(_document))
        {
            return _document;
        }

        ReSpace(0, 1, new(), 0);

        var solutionBuilder = new StringBuilder();
        int prevPos = 0;
        foreach (var pos in _spacesPositions)
        {
            solutionBuilder.Append(_document.AsSpan(prevPos, pos - prevPos)).Append(' ');
            prevPos = pos;
        }

        solutionBuilder.Append(_document.AsSpan(prevPos));

        return solutionBuilder.ToString();
    }

    private void ReSpace(int start, int count, LinkedList<int> positionBuilder, int builderUnrecognisedChars)
    {
        if (start + count + 1 <= _document!.Length)
        {
            ReSpace(start, count + 1, positionBuilder, builderUnrecognisedChars);
        }

        var word = _document.Substring(start, count);
        int cost = _dictionary.Contains(word) ? 0 : word.Length;

        var newPos = start + count;
        builderUnrecognisedChars += cost;
        if (newPos == _document!.Length)
        {
            if (builderUnrecognisedChars < _minUnrecognizedChars)
            {
                _minUnrecognizedChars = builderUnrecognisedChars;
                _spacesPositions = positionBuilder.ToList();
            }

            return;
        }

        if (builderUnrecognisedChars < _minUnrecognizedChars)
        {
            positionBuilder.AddLast(start + count);
            ReSpace(newPos, 1, positionBuilder, builderUnrecognisedChars);
            positionBuilder.RemoveLast();
        }
    }
}
