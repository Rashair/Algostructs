namespace DynamicProgramming;

public class ParensGenerator
{
    public static void PrintAllPossibleParens(int n)
    {
        var results = GenerateParens(n);

        Console.Write(results.First());
        foreach (var paren in results.Skip(1))
        {
            Console.Write(", ");
            Console.Write(paren);
        }
        Console.WriteLine();
    }

    public static List<string> GenerateParens(int n)
    {
        var processingStr = new char[n * 2];
        var results = new List<string>();
        GenerateParens(results, processingStr, 0, n, n);
        return results;
    }

    private static void GenerateParens(List<string> results, char[] processingStr, int index, int openRem, int closeRem)
    {
        if (openRem == 0 && closeRem == 0)
        {
            results.Add(new(processingStr));
            return;
        }

        if (openRem > 0)
        {
            processingStr[index] = '(';
            GenerateParens(results, processingStr, index + 1, openRem - 1, closeRem);
        }

        if (closeRem > openRem)
        {
            processingStr[index] = ')';
            GenerateParens(results, processingStr, index + 1, openRem, closeRem - 1);
        }
    }
}
