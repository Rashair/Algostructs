using System;
using System.Collections.Generic;

namespace PriorityQueue;

public class Beap<T> : IPriorityQueue<T>
    where T : IComparable<T>
{
    private readonly List<T> _values = [];

    public int Size => _values.Count;

    public T this[int k] => _values[k];

    public static int RowCol2Index(int row, int col)
    {
        checked
        {
            return (row * (row + 1)) / 2 + col;
        }
    }
    public static (int, int) Index2RowCol(int ind)
    {
        int row = 0;
        int sum = 0;

        while (sum <= ind)
        {
            sum += row + 1;
            ++row;
        }

        row = row - 1;
        sum = sum - row - 1;
        int col = ind - sum;

        return (row, col);
    }


    public void DeleteMax()
    {
        if (Size == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        (_values[0], _values[Size - 1]) = (_values[Size - 1], _values[0]);
        _values.RemoveAt(Size - 1);

        DownBeap();
    }
    public void Insert(T val)
    {
        _values.Add(val);

        UpBeap();
    }
    public T Max()
    {
        if (Size == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        return _values[0];
    }

    public int Search(T val)
    {
        int k = Size - 1;
        (int i, int j) = Index2RowCol(k);
        if(i != j)
        {
            i -= 1;
            j = i;
            k = RowCol2Index(i, j);
        }

        while (j >= 0 && val.CompareTo(_values[k]) != 0)
        {
            if (val.CompareTo(_values[k]) > 0)
            {
                k = k - i - 1;
                i -= 1;
                j -= 1;
            }
            else if (k + i + 1 < Size)
            {
                i += 1;
                k = k + i;
            }
            else
            {
                j -= 1;
                k -= 1;
            }
        }

        return j == -1 ? -1 : k;
    }

    private void UpBeap()
    {
        int k = Size - 1;
        var val = _values[k];
        (int i, int j) = Index2RowCol(k);

        int kNext = k;
        while (i > 0)
        {
            if (j == 0)
            {
                k = k - i;
            }
            else if (i == j)
            {
                k = k - i - 1;
                j -= 1;
            }
            else
            {
                k = k - i - 1;
                j -= 1;
                if (_values[k].CompareTo(_values[k + 1]) > 0)
                {
                    k += 1;
                    j += 1;
                }
            }

            if (_values[k].CompareTo(val) < 0)
            {
                _values[kNext] = _values[k];
                kNext = k;
                i -= 1;
            }
            else
            {
                break;
            }
        }

        _values[kNext] = val;
    }
    private void DownBeap()
    {
        if(Size < 2)
        {
            return;
        }

        int k = 1;
        int i = 1;
        var val = _values[0];
        int kPrev = 0;

        while (k < Size)
        {
            if (k + 1 < Size && _values[k].CompareTo(_values[k + 1]) < 0)
            {
                k += 1;
            }

            if (val.CompareTo(_values[k]) < 0)
            {
                _values[kPrev] = _values[k];
                kPrev = k;

                i += 1;
                k += i;
            }
            else
            {
                break;
            }
        }

        _values[kPrev] = val;
    }
}
