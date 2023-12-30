namespace Queues;

public interface IQueue<T>
{
    void Add(T value);
    T Remove();
    T Peek();
    bool IsEmpty();
}

public class TwoStacksQueue<T> : IQueue<T>
{
    private readonly Stack<T> _stackPush = new();
    private readonly Stack<T> _stackPop = new();

    public void Add(T value)
    {
        _stackPush.Push(value);
    }

    public T Remove()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        BalanceStacks();
        return _stackPop.Pop();
    }

    public T Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        BalanceStacks();
        return _stackPop.Peek();
    }

    private void BalanceStacks()
    {
        if (_stackPop.Count > 0)
        {
            return;
        }

        while (_stackPush.Count > 0)
        {
            _stackPop.Push(_stackPush.Pop());
        }
    }

    public bool IsEmpty()
    {
        return _stackPush.Count == 0 && _stackPop.Count == 0;
    }
}
