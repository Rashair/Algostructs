namespace Sorting;

public interface ISort<T>
{
    IList<T> Apply(IList<T> values);
}
