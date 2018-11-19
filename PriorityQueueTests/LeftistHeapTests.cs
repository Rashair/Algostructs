using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriorityQueue;

namespace PriorityQueueTests
{
    [TestClass]
    public class LeftistHeapTests : MergeablePriorityQueueTests<LeftistHeap<int>>
    {
        public override LeftistHeap<int> CreateMergeableInstance()
        {
            return new LeftistHeap<int>();
        }

    }
}
