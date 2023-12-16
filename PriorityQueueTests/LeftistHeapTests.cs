using PriorityQueue;

namespace PriorityQueueTests;

[TestFixture]
public class LeftistHeapTests : MergeablePriorityQueueTests<LeftistHeap<int>>
{
    protected override LeftistHeap<int> CreateMergeableInstance()
    {
        return new();
    }
}
