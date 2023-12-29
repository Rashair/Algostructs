using System.Diagnostics.CodeAnalysis;

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

        var nextStack = GetNextNonEmptyStack(stackNum);
        if (nextStack != null)
        {
            ShiftStacks(newValues, nextStack, capacityInc);
        }
    }

    private int? GetNextNonEmptyStack(int stackNum)
    {
        for (int i = stackNum + 1; i < _stacks.Length; ++i)
        {
            if (!IsEmpty(i))
            {
                return i;
            }
        }

        return null;
    }

    private void ShiftStacks(T[] newValues, [DisallowNull] int? nextStack, int capacityInc)
    {
        var nextLast = _stacks[nextStack.Value].Last;

        // Shift values to the right
        Array.Copy(newValues, nextLast,
            newValues, nextLast + capacityInc,
            newValues.Length - nextLast - 1);

        for (int i = nextStack.Value; i < _stacks.Length; ++i)
        {
            if (_stacks[i].Size > 0)
            {
                _stacks[i].First += capacityInc;
            }
        }
    }

    private int GetSubsequentFirst(int stackNum)
    {
        var st = _stacks[stackNum];
        if (st.Size > 0)
        {
            return st.First + 1;
        }

        for (int iter = stackNum - 1; iter >= 0; --iter)
        {
            if (_stacks[iter].Size > 0)
            {
                // +1 included in capacity
                return _stacks[iter].Last + _stacks[iter].Capacity;
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
