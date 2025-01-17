﻿namespace Sorting;

public class BucketSort<T> : ISort<T>
{
    private readonly Func<T, int> _keyConverter;
    private readonly ISort<T> _bucketSortingAlgorithm;

    public BucketSort(Func<T, int> keyConverter)
    {
        _keyConverter = keyConverter;
        _bucketSortingAlgorithm = new QuickSort<T>(keyConverter);
    }

    public IList<T> Apply(IList<T> values)
    {
        if (values.Count == 0)
        {
            return values;
        }

        var (min, max) = FindMinMaxKeys(values);
        if (min == max)
        {
            return values;
        }

        int bucketSize = GetBucketSize(min, max, values.Count);
        var buckets = new List<T>[bucketSize];
        for (int b = 0; b < bucketSize; ++b)
        {
            buckets[b] = new(values.Count / bucketSize);
        }

        for (int i = 0; i < values.Count; ++i)
        {
            var bucketIndex = (ulong) (((long) _keyConverter(values[i]) - min) / ((max + 1L - min) / (double) bucketSize));
            buckets[bucketIndex].Add(values[i]);
        }

        for (int b = 0; b < bucketSize; ++b)
        {
            _bucketSortingAlgorithm.Apply(buckets[b]);
        }

        int valuesIter = 0;
        for (int b = 0; b < buckets.Length; ++b)
        {
            for (int k = 0; k < buckets[b].Count; ++k)
            {
                values[valuesIter] = buckets[b][k];
                ++valuesIter;
            }
        }

        return values;
    }

    private (int, int) FindMinMaxKeys(IList<T> values)
    {
        var min = _keyConverter(values[0]);
        var max = min;
        for (int i = 1; i < values.Count; ++i)
        {
            var key = _keyConverter(values[i]);
            if (key > max)
            {
                max = key;
            }

            if (key < min)
            {
                min = key;
            }
        }

        return (min, max);
    }

    private int GetBucketSize(int min, int max, int valuesLen)
    {
        var diff = (long) max - min;
        var sqrtLen = Math.Sqrt(valuesLen);
        double result;
        if (diff > valuesLen) // if the range is too big, just use Sqrt
        {
            // TODO: How to optimise?
            result = sqrtLen;
        }
        else
        {
            result = diff / sqrtLen;
        }

        return (int) Math.Ceiling(result);
    }
}
