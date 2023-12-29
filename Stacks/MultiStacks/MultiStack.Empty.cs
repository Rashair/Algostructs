namespace Stacks.MultiStacks;

public partial class MultiStack<T>
{
    public bool AreAllStacksEmpty()
    {
        for (int i = 0; i < _stacks.Length; ++i)
        {
            if (!IsEmpty(i))
            {
                return false;
            }
        }

        return true;
    }

    public bool IsEmpty()
    {
        return IsEmpty(DefaultStackNum);
    }

    public bool IsEmpty(int stackNum)
    {
        ValidateStackNum(stackNum);

        return _stacks[stackNum].Size == 0;
    }
}
