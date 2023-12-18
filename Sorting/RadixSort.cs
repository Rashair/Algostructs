namespace Sorting;

public class RadixSort<T> : ISort<T>
{
    private const int Base = 256;
    private readonly Func<T, int> _keyConverter;

    public RadixSort(Func<T, int> keyConverter)
    {
        _keyConverter = keyConverter;
    }

    public IList<T> Apply(IList<T> values)
    {
        var (positives, negatives) = GetNegativesAndPositives(values);

        var resNegatives = InternalSort(negatives, (T t) => -(long) _keyConverter(t));
        int valuesIter = 0;
        for (int i = resNegatives.Length - 1; i >= 0; --i)
        {
            values[valuesIter] = resNegatives[i];
            ++valuesIter;
        }

        var resPositives = InternalSort(positives, (T t) => _keyConverter(t));
        for (int i = 0; i < resPositives.Length; ++i)
        {
            values[valuesIter] = resPositives[i];
            ++valuesIter;
        }

        return values;
    }

    private (T[] positives, T[] negatives) GetNegativesAndPositives(IList<T> values)
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

        return (positives.ToArray(), negatives.ToArray());
    }

    /// <summary>
    /// Uses long to allow int.MinValue
    /// </summary>
    private static T[] InternalSort(T[] values, Func<T, long> keyConverter)
    {
        if (values.Length == 0)
        {
            return values;
        }

        var maxVal = values.Max(keyConverter);
        var temp = new T[values.Length];
        for (long exp = 1; maxVal / exp > 0; exp *= Base)
        {
            CountingSortByDigit(values, keyConverter, exp, temp);
        }

        return values;
    }

    private static void CountingSortByDigit(T[] values, Func<T, long> keyConverter, long exp, T[] temp)
    {
        var digitsCount = new int[Base];
        for (int i = 0; i < values.Length; ++i)
        {
            var digit = (byte) (keyConverter(values[i]) / exp % Base);
            ++digitsCount[digit];
        }

        for (int k = 1; k < digitsCount.Length; ++k)
        {
            digitsCount[k] += digitsCount[k - 1];
        }

        // Key here for stability is that we're starting from the end of the array,
        // Two equal values will always be placed in the original order, because last will stay last.
        // Because the algorithm is stable, already sorted number will stay in the same order
        //  and we will reorder only by the next (more significant) digit
        for (int i = values.Length - 1; i >= 0; --i)
        {
            var index = (byte) (keyConverter(values[i]) / exp % Base);
            temp[digitsCount[index] - 1] = values[i];
            --digitsCount[index];
        }

        Array.Copy(temp, values, values.Length);
    }
}
