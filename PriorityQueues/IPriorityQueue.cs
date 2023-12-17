namespace PriorityQueues;

public interface IPriorityQueue<T>
{
    int Size { get; }
    void Insert(T val);
    T Max();
    void DeleteMax();
}

public interface IMergeablePriorityQueue<TQ, T> : IPriorityQueue<T>
    where TQ : IMergeablePriorityQueue<TQ, T>
{
    TQ Union(TQ queue);
}