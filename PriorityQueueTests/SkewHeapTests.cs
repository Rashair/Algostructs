using PriorityQueue;

namespace PriorityQueueTests
{
    [TestFixture]
    public class SkewHeapTests : MergeablePriorityQueueTests<SkewHeap<int>>
    {
        public override SkewHeap<int> CreateMergeableInstance() => new SkewHeap<int>();
    }
}
