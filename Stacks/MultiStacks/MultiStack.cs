namespace Stacks.MultiStacks;

public partial class MultiStack<T> : IStack<T>, IMultiStack<T>
{
    private const int DefaultStackNum = 0;
    private readonly MyStack[] _stacks;
    private T[] _values;

    public MultiStack(int stacksNum)
    {
        if (stacksNum < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(stacksNum), "Stacks number must be greater than 0.");
        }

        _stacks = new MyStack[stacksNum];
        for (int i = 0; i < stacksNum; ++i)
        {
            _stacks[i] = new();
        }

        _values = Array.Empty<T>();
    }

    private void ValidateStackNum(int stackNum)
    {
        if (stackNum < 0 || stackNum >= _stacks.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(stackNum), "Stack number is out of range.");
        }
    }
    
    private class MyStack
    {
        public int First { get; set; }
        public int Size { get; set; }
        public int Capacity { get; set; }
        public int Last => First - Size + 1;
    }
}
