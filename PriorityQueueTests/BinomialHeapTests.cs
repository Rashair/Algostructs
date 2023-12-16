using PriorityQueue;

namespace PriorityQueueTests;

[TestFixture]
public class BinomialHeapTests : MergeablePriorityQueueTests<BinomialHeap<int>>
{
    protected override BinomialHeap<int> CreateMergeableInstance() => new BinomialHeap<int>();
}