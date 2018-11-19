using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriorityQueue;

namespace PriorityQueueTests
{
    [TestClass]
    public class FibonacciHeapTests : MergeablePriorityQueueTests<FibonacciHeap<int>>
    {
        public override FibonacciHeap<int> CreateMergeableInstance() => new FibonacciHeap<int>();
    }
}
