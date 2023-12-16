using PriorityQueue;

namespace PriorityQueueTests;

[TestFixture]
public class HeapTests : PriorityQueueTests
{
    protected override IPriorityQueue<int> CreateInstance()
    {
        return new Heap<int>();
    }

    [Test]
    public void HeapBuildSizeTest()
    {
        var tab = new[] { 1, 2, 3, 4, 5, -5100, 33, 3, 4, 4, 56, 7, 55 };

        var heap = new Heap<int>(tab);

        Assert.That(heap.Size, Is.EqualTo(tab.Length));
    }

    [Test]
    public void HeapBuildMaxTest()
    {
        var tab = new[] { 1, 2, 3, 4, 5, -5100, 33, 3, 4, 4, 56, 7, 55 };

        var heap = new Heap<int>(tab);

        Assert.That(tab.Max(), Is.EqualTo(heap.Max()));
    }

    [Test]
    public void HeapBuildOrderTest()
    {
        var tab = new[] { 1, 2, 3, 4, 5, -5100, 33, 3, 4, 4, 56, 7, 55 };

        var heap = new Heap<int>(tab);

        Array.Sort(tab);
        Array.Reverse(tab);

        for (int i = 0; i < tab.Length; ++i)
        {
            int max = heap.Max();
            heap.DeleteMax();

            Assert.That(tab[i], Is.EqualTo(max));
        }
    }

    [Test]
    public void HeapDeleteTest()
    {
        var tab = new[] { 1, 2, 3, 4, 5, -5100, 33, 3, 4, 4, 56, 7, 55 };

        var heap = new Heap<int>(tab);

        heap.Delete(heap.Size - 1);
        heap.Delete((heap.Size - 1 - 1) / 2);
        heap.Delete(1);

        Assert.Multiple(() =>
        {
            Assert.That(tab.Length - 3, Is.EqualTo(heap.Size));
            Assert.That(heap.Max(), Is.EqualTo(tab.Max()));
        });
    }
}
