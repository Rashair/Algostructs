namespace Sorting;

public interface ISort<T>
{
    IList<T> Apply(IList<T> values);
}

public interface ISortLowHigh<T> : ISort<T>
{
    IList<T> Apply(IList<T> values, int low, int high);
}

