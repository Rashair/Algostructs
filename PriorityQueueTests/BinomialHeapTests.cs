using PriorityQueue;

namespace PriorityQueueTests
{
    [TestFixture]
    public class BinomialHeapTests : MergeablePriorityQueueTests<BinomialHeap<int>>
    {
        public override BinomialHeap<int> CreateMergeableInstance() => new BinomialHeap<int>();
    }
}
