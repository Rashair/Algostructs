using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriorityQueue;

namespace PriorityQueueTests
{
    [TestClass]
    public class SkewHeapTests : MergeablePriorityQueueTests<SkewHeap<int>>
    {
        public override SkewHeap<int> CreateMergeableInstance() => new SkewHeap<int>();
    }
}
