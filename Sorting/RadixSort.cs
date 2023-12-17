namespace Sorting;

public class RadixSort<T> : ISort<T>
{
    private const int Base = 10;
    private readonly Func<T, int> _keyConverter;

    public RadixSort(Func<T, int> keyConverter)
    {
        _keyConverter = keyConverter;
    }

    public IList<T> Apply(IList<T> values)
    {
        var positives = new List<T>(values.Count / 2);
        var negatives = new List<T>(values.Count / 2);
        for (int i = 0; i < values.Count; ++i)
        {
            if (_keyConverter(values[i]) < 0)
            {
                negatives.Add(values[i]);
            }
            else
            {
                positives.Add(values[i]);
            }
        }

        InternalSort(positives, (T t) => _keyConverter(t));

        InternalSort(negatives, (T t) => -(long) _keyConverter(t));

        int valuesIter = 0;
        for (int i = negatives.Count - 1; i >= 0; --i)
        {
            values[valuesIter] = negatives[i];
            ++valuesIter;
        }

        for (int i = 0; i < positives.Count; ++i)
        {
            values[valuesIter] = positives[i];
            ++valuesIter;
        }

        return values;
    }

    /// <summary>
    /// Uses long to allow int.MinValue
    /// </summary>
    private static IList<T> InternalSort(IList<T> values, Func<T, long> keyConverter)
    {
        if (values.Count == 0)
        {
            return values;
        }

        var maxVal = values.Max(keyConverter);
        var temp = new T[values.Count];
        for (long exp = 1; maxVal / exp > 0; exp *= Base)
        {
            CountingSortByDigit(values, keyConverter, exp, temp);
        }

        return values;
    }

    private static void CountingSortByDigit(IList<T> values, Func<T, long> keyConverter, long exp, T[] temp)
    {
        var digitsCount = new int[Base];
        for (int i = 0; i < values.Count; ++i)
        {
            var digit = keyConverter(values[i]) / exp % Base;
            ++digitsCount[digit];
        }

        for (int k = 1; k < digitsCount.Length; ++k)
        {
            digitsCount[k] += digitsCount[k - 1];
        }

        for (int i = values.Count - 1; i >= 0; --i)
        {
            var index = keyConverter(values[i]) / exp % Base;
            temp[digitsCount[index] - 1] = values[i];
            --digitsCount[index];
        }

        for (int i = 0; i < values.Count; ++i)
        {
            values[i] = temp[i];
        }
    }
}
