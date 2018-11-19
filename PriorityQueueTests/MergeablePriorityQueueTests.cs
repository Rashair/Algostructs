using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PriorityQueue;

namespace PriorityQueueTests
{
    [TestClass]
    public abstract class MergeablePriorityQueueTests<TQ> : PriorityQueueTests
        where TQ : IMergeablePriorityQueue<TQ, int>
    {
        public override IPriorityQueue<int> CreateInstance()
        {
            return CreateMergeableInstance();
        }
        public abstract TQ CreateMergeableInstance();

        [TestMethod]
        public void UnionSizeTest()
        {
            var Q1 = CreateMergeableInstance();
            int[] values1 = new int[] { 3, 2, 3, 1, 4, 5, 8, 1, -5, 0, -45, 1, 15 };
            foreach (var val in values1)
            {
                Q1.Insert(val);
            }

            var Q2 = CreateMergeableInstance();
            int[] values2 = new int[] { -3, -2, -3, -1, -4, -5, -8, -1, 5, 0, 45, -1, -15 };
            foreach (var val in values2)
            {
                Q2.Insert(val);
            }


            var Q3 = Q1.Union(Q2);

            Assert.AreEqual(values1.Length + values2.Length, Q3.Size);
        }
    }
}
