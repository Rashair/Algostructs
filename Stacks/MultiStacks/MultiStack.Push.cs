namespace Stacks.MultiStacks;

public partial class MultiStack<T>
{
    public void Push(T value)
    {
        Push(DefaultStackNum, value);
    }

    public void Push(int stackNum, T value)
    {
        ValidateStackNum(stackNum);
        EnsureFreeCapacity(stackNum);

        var nextFirst = GetSubsequentFirst(stackNum);
        _values[nextFirst] = value;
        IncreaseStackSize(stackNum, nextFirst);
    }

    private void EnsureFreeCapacity(int stackNum)
    {
        if (_stacks[stackNum].Size < _stacks[stackNum].Capacity)
        {
            return;
        }

        // 2 * size + 1
        var capacityInc = _stacks[stackNum].Size + 1;
        var newTotalSize = _values.Length + capacityInc;
        var newValues = new T[newTotalSize];
        Array.Copy(_values, newValues, _values.Length);
        _stacks[stackNum].Capacity += capacityInc;
        _values = newValues;

        var nextLast = GetNextStackLast(stackNum);
        if (nextLast != null)
        {
            // Shift next stacks to the right
            Array.Copy(newValues, nextLast.Value,
                newValues, nextLast.Value + capacityInc,
                newValues.Length - nextLast.Value);
        }
    }

    private int? GetNextStackLast(int stackNum)
    {
        for (int i = stackNum + 1; i < _stacks.Length; ++i)
        {
            if (_stacks[i].Size > 0)
            {
                return _stacks[i].Last;
            }
        }

        return null;
    }

    private int GetSubsequentFirst(int stackNum)
    {
        var st = _stacks[stackNum];
        if (st.Size > 0)
        {
            return st.First + 1;
        }

        for (int stackIter = stackNum - 1; stackIter >= 0; --stackIter)
        {
            if (_stacks[stackNum].Size > 0)
            {
                // +1 included in capacity
                return _stacks[stackNum].Last + _stacks[stackNum].Capacity;
            }
        }

        return 0;
    }

    private void IncreaseStackSize(int stackNum, int nextFirst)
    {
        _stacks[stackNum].First = nextFirst;
        _stacks[stackNum].Size += 1;
    }
}
