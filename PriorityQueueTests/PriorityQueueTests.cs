using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriorityQueue;

namespace PriorityQueueTests
{

    [TestClass]
    public abstract class PriorityQueueTests
    {
        public readonly Random rand = new Random(1234);
        public abstract IPriorityQueue<int> CreateInstance();

        [TestMethod]
        public void SizeTest()
        {
            IPriorityQueue<int> priorityQueue = CreateInstance();

            int[] values = new int[] { 3, 2, 3, 1, 4, 5, 8, 1, -5, 0, -45, 1, 15 };
            for (int i = 0; i < values.Length; ++i)
            {
                priorityQueue.Insert(values[i]);
            }

            Assert.AreEqual(values.Length, priorityQueue.Size, $"Size of the queue should be {values.Length}");
        }

        [TestMethod]
        public void MaxTest()
        {
            IPriorityQueue<int> priorityQueue = CreateInstance();
            Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Max());

            int[] values = new int[] { -45, 5, 6, 7, 9, 9, 9, 24, 5, 0, 0 };
            for (int i = 0; i < values.Length; ++i)
            {
                priorityQueue.Insert(values[i]);
            }

            int max = values.Max();

            Assert.AreEqual(max, priorityQueue.Max(), $"Max should equal to {max}");
        }

        [TestMethod]
        public void DeleteMaxTest()
        {
            IPriorityQueue<int> priorityQueue = CreateInstance();
            Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.DeleteMax());

            int[] values = new int[] { -45, 5, 6, 7, 9, 9, 9, 24, 5, 0, 0 };
            for (int i = 0; i < values.Length; ++i)
            {
                priorityQueue.Insert(values[i]);
            }

            int max = values.Max();
            priorityQueue.DeleteMax();

            Assert.AreNotEqual(max, priorityQueue.Max(), $"Max should not equal to {max}");
        }

        [TestMethod]
        public void OrderTest()
        {
            IPriorityQueue<int> priorityQueue = CreateInstance();

            int[] values = new int[] { 3, 2, 3, 1, 4, 5, 8, 1, -5, 0, -45, 1, 15 };
            for (int i = 0; i < values.Length; ++i)
            {
                priorityQueue.Insert(values[i]);
            }

            Array.Sort(values);
            Array.Reverse(values);

            int j = 0;
            while (priorityQueue.Size > 0)
            {
                int val = priorityQueue.Max();
                priorityQueue.DeleteMax();

                Console.WriteLine($"{val} ");

                Assert.AreEqual(values[j], val, $"Current max should be {values[j]}");
                j += 1;
            }

            Assert.AreEqual(j, values.Length, $"Should have popped {j} values.");
        }

        [TestMethod, Timeout(2500)]
        public void RandTest()
        {
            IPriorityQueue<int> priorityQueue = CreateInstance();

            const int N = 100_000;

            var values = new List<int>(N);
            for (int i = 0; i < N; ++i)
            {
                values.Add(rand.Next());
                priorityQueue.Insert(values[i]);
            }

            Assert.AreEqual(N, priorityQueue.Size, $"Size should be {N}");

            values.Sort((a, b) => -1 * a.CompareTo(b));

            Assert.AreEqual(values[0], priorityQueue.Max(), $"Max should be {values[0]}");

            int j = 0;
            while (priorityQueue.Size > 0)
            {
                int val = priorityQueue.Max();
                priorityQueue.DeleteMax();

                Console.WriteLine($"{val} ");

                Assert.AreEqual(values[j], val, 0, $"Current max should be {values[j]}");
                j += 1;
            }

            Assert.AreEqual(j, N, $"Should have popped {j} values.");
        }
    }

}