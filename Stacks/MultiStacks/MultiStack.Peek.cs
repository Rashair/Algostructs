namespace Stacks.MultiStacks;

public partial class MultiStack<T>
{
    public T Peek()
    {
        return Peek(DefaultStackNum);
    }

    public T Peek(int stackNum)
    {
        if (IsEmpty(stackNum))
        {
            throw new InvalidOperationException("Stack is empty.");
        }

        return _values[_stacks[stackNum].First];
    }
}
