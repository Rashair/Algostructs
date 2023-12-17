namespace Sorting;

public class InsertionSort<T> : ISort<T>
{
    private readonly Func<T, int> _keyConverter;

    public InsertionSort(Func<T, int> keyConverter)
    {
        _keyConverter = keyConverter;
    }

    public IList<T> Apply(IList<T> values)
    {
        for (int i = 0; i < values.Count; ++i)
        {
            var t = values[i];
            int tKey = _keyConverter(t);
            int j;
            for (j = i; j > 0 && _keyConverter(values[j - 1]) > tKey; --j)
            {
                values[j] = values[j - 1];
            }

            values[j] = t;
        }

        return values;
    }
}
