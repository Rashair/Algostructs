namespace Stacks.MultiStacks;

public partial class MultiStack<T>
{
    public T Pop()
    {
        return Pop(DefaultStackNum);
    }

    public T Pop(int stackNum)
    {
        if (IsEmpty(stackNum))
        {
            throw new InvalidOperationException("Stack is empty.");
        }

        var st = _stacks[stackNum];
        st.Size -= 1;
        st.First -= 1;

        return _values[st.First + 1];
    }
}
