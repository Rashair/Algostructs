namespace PriorityQueue.Tests;

[TestFixture]
public abstract class MergeablePriorityQueueTests<TQ> : PriorityQueueTests
    where TQ : IMergeablePriorityQueue<TQ, int>
{
    protected override IPriorityQueue<int> CreateInstance()
    {
        return CreateMergeableInstance();
    }

    protected abstract TQ CreateMergeableInstance();

    [Test]
    public void UnionSizeTest()
    {
        var q1 = CreateMergeableInstance();
        int[] values1 = { 3, 2, 3, 1, 4, 5, 8, 1, -5, 0, -45, 1, 15 };
        foreach (var val in values1)
        {
            q1.Insert(val);
        }

        var q2 = CreateMergeableInstance();
        int[] values2 = { -3, -2, -3, -1, -4, -5, -8, -1, 5, 0, 45, -1, -15 };
        foreach (var val in values2)
        {
            q2.Insert(val);
        }


        var q3 = q1.Union(q2);

        Assert.That(values1.Length + values2.Length, Is.EqualTo(q3.Size));
    }
}
