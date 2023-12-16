using PriorityQueue;

namespace PriorityQueueTests;

[TestFixture]
public class SkewHeapTests : MergeablePriorityQueueTests<SkewHeap<int>>
{
    protected override SkewHeap<int> CreateMergeableInstance() => new SkewHeap<int>();
}
