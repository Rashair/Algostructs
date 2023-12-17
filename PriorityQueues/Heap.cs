using System;
using System.Collections.Generic;

namespace PriorityQueues;

public class Heap<T> : IPriorityQueue<T>
    where T : IComparable<T>
{
    private readonly IList<T> _values;

    public Heap()
    {
        _values = new List<T>();
    }
    public Heap(IEnumerable<T> tab)
    {
        _values = new List<T>(tab);

        int i = (Size - 1 - 1) / 2;
        while (i >= 0)
        {
            DownHeap(i);
            i = i - 1;
        }
    }

    public int Size => _values.Count;

    public void DeleteMax()
    {
        if (Size == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        Delete(0);
    }
    public T Max()
    {
        if (Size == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        return _values[0];
    }
    public void Insert(T val)
    {
        _values.Add(val);
        UpHeap();
    }

    public void Delete(int i)
    {
        (_values[i], _values[Size - 1]) = (_values[Size - 1], _values[i]);
        _values.RemoveAt(Size - 1);

        DownHeap(i);
    }

    private void UpHeap()
    {
        int i = Size - 1;
        while (_values[i].CompareTo(_values[(i - 1) / 2]) > 0)
        {
            (_values[i], _values[(i - 1) / 2]) = (_values[(i - 1) / 2], _values[i]);

            i = (i - 1) / 2;
        }
    }
    private void DownHeap(int i)
    {
        i = 2 * i + 1;
        while (i < Size)
        {
            if (i + 1 < Size)
            {
                if (_values[i + 1].CompareTo(_values[i]) > 0)
                {
                    i = i + 1;
                }
            }

            if (_values[i].CompareTo(_values[(i - 1) / 2]) > 0)
            {
                (_values[i], _values[(i - 1) / 2]) = (_values[(i - 1) / 2], _values[i]);

                i = 2 * i + 1;
            }
            else
            {
                return;
            }
        }
    }
}
