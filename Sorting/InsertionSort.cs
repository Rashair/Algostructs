namespace Sorting;

public class InsertionSort<T> : ISortLowHigh<T>
{
    private readonly Func<T, int> _keyConverter;

    public InsertionSort(Func<T, int> keyConverter)
    {
        _keyConverter = keyConverter;
    }

    public IList<T> Apply(IList<T> values)
    {
        return Apply(values, 0, values.Count - 1);
    }

    public IList<T> Apply(IList<T> values, int low, int high)
    {
        for (int i = low; i <= high; ++i)
        {
            var t = values[i];
            int tKey = _keyConverter(t);
            int j;
            for (j = i; j > low && _keyConverter(values[j - 1]) > tKey; --j)
            {
                values[j] = values[j - 1];
            }

            values[j] = t;
        }

        return values;
    }
}
