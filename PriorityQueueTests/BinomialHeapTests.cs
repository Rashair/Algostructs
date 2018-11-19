using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriorityQueue;

namespace PriorityQueueTests
{
    [TestClass]
    public class BinomialHeapTests : MergeablePriorityQueueTests<BinomialHeap<int>>
    {
        public override BinomialHeap<int> CreateMergeableInstance() => new BinomialHeap<int>();
    }
}
