using PriorityQueue;

namespace PriorityQueueTests
{
    [TestFixture]
    public class LeftistHeapTests : MergeablePriorityQueueTests<LeftistHeap<int>>
    {
        public override LeftistHeap<int> CreateMergeableInstance()
        {
            return new LeftistHeap<int>();
        }

    }
}
