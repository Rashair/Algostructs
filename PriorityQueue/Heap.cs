using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueue
{

    public class Heap<T> : IPriorityQueue<T>
        where T : IComparable<T>
    {
        private readonly IList<T> values;

        public Heap()
        {
            values = new List<T>();
        }
        public Heap(T[] tab)
        {
            values = new List<T>(tab);

            int i = (Size - 1 - 1) / 2;
            while (i >= 0)
            {
                DownHeap(i);
                i = i - 1;
            }
        }

        public int Size => values.Count;

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

            return values[0];
        }
        public void Insert(T val)
        {
            values.Add(val);
            UpHeap();
        }

        public void Delete(int i)
        {
            (values[i], values[Size - 1]) = (values[Size - 1], values[i]);
            values.RemoveAt(Size - 1);

            DownHeap(i);
        }

        private void UpHeap()
        {
            int i = Size - 1;
            while (values[i].CompareTo(values[(i - 1) / 2]) > 0)
            {
                (values[i], values[(i - 1) / 2]) = (values[(i - 1) / 2], values[i]);

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
                    if (values[i + 1].CompareTo(values[i]) > 0)
                    {
                        i = i + 1;
                    }
                }

                if (values[i].CompareTo(values[(i - 1) / 2]) > 0)
                {
                    (values[i], values[(i - 1) / 2]) = (values[(i - 1) / 2], values[i]);

                    i = 2 * i + 1;
                }
                else
                {
                    return;
                }
            }
        }
    }

}