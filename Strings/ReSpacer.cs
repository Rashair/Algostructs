using System.Text;

namespace Strings;

public class ReSpacer
{
    private readonly HashSet<string> _dictionary;
    private readonly string _document;

    private readonly ParseResult?[] _memory;

    public ReSpacer(HashSet<string> dictionary, string document)
    {
        _dictionary = dictionary;
        _document = document;

        _memory = new ParseResult[document.Length];
    }

    public string? ReSpace()
    {
        if (string.IsNullOrEmpty(_document))
        {
            return _document;
        }

        var parseResult = ReSpace(0);

        return parseResult.Document;
    }

    private ParseResult ReSpace(int start)
    {
        if (start >= _document.Length)
        {
            return new(0, "");
        }

        if (_memory[start] != null)
        {
            return _memory[start]!;
        }

        var bestUnrecognisedChars = int.MaxValue;
        var bestDocument = "";
        int iter = start;
        var currentWord = "";
        for (; iter < _document.Length; ++iter)
        {
            currentWord += _document[iter];
            var cost = _dictionary.Contains(currentWord) ? 0 : currentWord.Length;
            var subResult = ReSpace(iter + 1);
            if (subResult.UnrecognisedChars + cost + 1 < bestUnrecognisedChars)
            {
                bestUnrecognisedChars = subResult.UnrecognisedChars + cost + 1;
                bestDocument = currentWord + (" " + subResult.Document).TrimEnd();
            }
        }

        _memory[start] = new(bestUnrecognisedChars, bestDocument);

        return _memory[start]!;
    }


    public class ParseResult
    {
        public ParseResult(int unrecognisedChars, string document)
        {
            UnrecognisedChars = unrecognisedChars;
            Document = document;
        }

        public string Document { get; set; }
        public int UnrecognisedChars { get; set; }
    }
}
