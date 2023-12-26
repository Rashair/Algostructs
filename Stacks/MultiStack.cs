namespace Stacks;

public class MultiStack<T> : IStack<T>
{
    private readonly T[] _values;
    private readonly MyStack[] _stacks;

    public MultiStack(int stacksNum)
    {
        _values = Array.Empty<T>();
        _stacks = new MyStack[stacksNum];
    }

    public void Push(T value)
    {
        throw new NotImplementedException();
    }

    public T Pop()
    {
        throw new NotImplementedException();
    }

    public T Peek()
    {
        throw new NotImplementedException();
    }

    public bool IsEmpty()
    {
        throw new NotImplementedException();
    }
    
    private class MyStack
    {
        public int First { get; set; }
        public int Size { get; set; }
        public int Capacity { get; set; }
        public int Last => First - Size + 1;
    }
}
