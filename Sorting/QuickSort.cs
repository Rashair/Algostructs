namespace Sorting;

public class QuickSort<T> : ISort<T>
{
    private readonly Func<T, int> _keyConverter;

    public QuickSort(Func<T, int> keyConverter)
    {
        _keyConverter = keyConverter;
    }

    public IList<T> Apply(IList<T> values)
    {
        InternalSort(values, 0, values.Count - 1);

        return values;
    }

    private void InternalSort(IList<T> values, int low, int high)
    {
        while (low < high)
        {
            int medianIndex = MedianOf3Pivots(values, low, (low + high) / 2, high);
            Swap(values, medianIndex, high);
            int pivotIndex = Partition(values, low, high);

            if (pivotIndex - low < high - pivotIndex)
            {
                InternalSort(values, low, pivotIndex - 1);
                low = pivotIndex + 1;
            }
            else
            {
                InternalSort(values, pivotIndex + 1, high);
                high = pivotIndex - 1;
            }
        }
    }

    private int MedianOf3Pivots(IList<T> values, int a, int b, int c)
    {
        var valA = _keyConverter(values[a]);
        var valB = _keyConverter(values[b]);
        var valC = _keyConverter(values[c]);

        if (valA < valB)
        {
            if (valB < valC)
            {
                return b;
            }

            return valA < valC ? c : a;
        }

        // valB <= valA
        if (valA < valC)
        {
            return a;
        }

        return valB < valC ? c : b;
    }

    private int Partition(IList<T> values, int low, int high)
    {
        var pivot = _keyConverter(values[high]);
        int i = low - 1;
        for (int j = low; j < high; ++j)
        {
            if (_keyConverter(values[j]) >= pivot)
            {
                continue;
            }

            ++i;
            if(i != j)
            {
                Swap(values, i, j);
            }
        }

        // Swap pivot value in its rightful place
        Swap(values, i + 1, high);

        return i + 1;
    }

    private static void Swap(IList<T> values, int a, int b)
    {
        (values[a], values[b]) = (values[b], values[a]);
    }
}
