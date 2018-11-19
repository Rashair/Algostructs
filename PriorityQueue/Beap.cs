using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueue
{

    public class Beap<T> : IPriorityQueue<T>
        where T : IComparable<T>
    {
        private readonly IList<T> values;

        public Beap()
        {
            values = new List<T>();
        }

        public int Size => values.Count;

        public T this[int k]
        {
            get { return values[k]; }
        }

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

            (values[0], values[Size - 1]) = (values[Size - 1], values[0]);
            values.RemoveAt(Size - 1);

            DownBeap();
        }
        public void Insert(T val)
        {
            values.Add(val);

            UpBeap();
        }
        public T Max()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            return values[0];
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

            while (j >= 0 && val.CompareTo(values[k]) != 0)
            {
                if (val.CompareTo(values[k]) > 0)
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
            T val = values[k];
            (int i, int j) = Index2RowCol(k);

            int k_next = k;
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
                    if (values[k].CompareTo(values[k + 1]) > 0)
                    {
                        k += 1;
                        j += 1;
                    }
                }

                if (values[k].CompareTo(val) < 0)
                {
                    values[k_next] = values[k];
                    k_next = k;
                    i -= 1;
                }
                else
                {
                    break;
                }
            }

            values[k_next] = val;
        }
        private void DownBeap()
        {
            if(Size < 2)
            {
                return;
            }

            int k = 1;
            int i = 1, j = 0;
            T val = values[0];
            int k_prev = 0;

            while (k < Size)
            {
                if (k + 1 < Size && values[k].CompareTo(values[k + 1]) < 0)
                {
                    k += 1;
                    j += 1;
                }

                if (val.CompareTo(values[k]) < 0)
                {
                    values[k_prev] = values[k];
                    k_prev = k;

                    i += 1;
                    k += i;
                }
                else
                {
                    break;
                }
            }

            values[k_prev] = val;
        }
    }
}

