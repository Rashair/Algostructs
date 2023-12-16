using PriorityQueue;

namespace PriorityQueueTests;

[TestFixture]
public abstract class PriorityQueueTests
{
    protected readonly Random Rand = new(1234);

    protected abstract IPriorityQueue<int> CreateInstance();


    [Test]
    public void SizeTest()
    {
        var priorityQueue = CreateInstance();

        int[] values = { 3, 2, 3, 1, 4, 5, 8, 1, -5, 0, -45, 1, 15 };
        for (int i = 0; i < values.Length; ++i)
        {
            priorityQueue.Insert(values[i]);
        }

        Assert.That(priorityQueue.Size, Is.EqualTo(values.Length), $"Size of the queue should be {values.Length}");
    }

    [Test]
    public void MaxTest()
    {
        var priorityQueue = CreateInstance();
        Assert.Throws<InvalidOperationException>(() => priorityQueue.Max());

        int[] values = { -45, 5, 6, 7, 9, 9, 9, 24, 5, 0, 0 };
        for (int i = 0; i < values.Length; ++i)
        {
            priorityQueue.Insert(values[i]);
        }

        int max = values.Max();

        Assert.That(priorityQueue.Max(), Is.EqualTo(max), $"Max should equal to {max}");
    }

    [Test]
    public void DeleteMaxTest()
    {
        var priorityQueue = CreateInstance();
        Assert.Throws<InvalidOperationException>(() => priorityQueue.DeleteMax());

        int[] values = { -45, 5, 6, 7, 9, 9, 9, 24, 5, 0, 0 };
        for (int i = 0; i < values.Length; ++i)
        {
            priorityQueue.Insert(values[i]);
        }

        int max = values.Max();
        priorityQueue.DeleteMax();

        Assert.That(max, Is.Not.EqualTo(priorityQueue.Max()), $"Max should not equal to {max}");
    }

    [Test]
    public void OrderTest()
    {
        var priorityQueue = CreateInstance();

        int[] values = { 3, 2, 3, 1, 4, 5, 8, 1, -5, 0, -45, 1, 15 };
        for (int i = 0; i < values.Length; ++i)
        {
            priorityQueue.Insert(values[i]);
        }

        Array.Sort(values);
        Array.Reverse(values);

        int j = 0;
        while (priorityQueue.Size > 0)
        {
            int val = priorityQueue.Max();
            priorityQueue.DeleteMax();

            Console.WriteLine($"{val} ");

            Assert.That(val,  Is.EqualTo(values[j]), $"Current max should be {values[j]}");
            j += 1;
        }

        Assert.That(values, Has.Length.EqualTo(j), $"Should have popped {j} values.");
    }

    [Test, CancelAfter(3500)]
    public void RandTest()
    {
        var priorityQueue = CreateInstance();

        const int n = 100_000;

        var values = new List<int>(n);
        for (int i = 0; i < n; ++i)
        {
            values.Add(Rand.Next());
            priorityQueue.Insert(values[i]);
        }

        Assert.That(priorityQueue.Size,  Is.EqualTo(n), $"Size should be {n}");

        values.Sort((a, b) => -1 * a.CompareTo(b));

        Assert.That(priorityQueue.Max(), Is.EqualTo(values[0]), $"Max should be {values[0]}");

        int j = 0;
        while (priorityQueue.Size > 0)
        {
            int val = priorityQueue.Max();
            priorityQueue.DeleteMax();

            Console.WriteLine($"{val} ");

            Assert.That(val, Is.EqualTo(values[j]), $"Current max should be {values[j]}");
            j += 1;
        }

        Assert.That(j,  Is.EqualTo(n), $"Should have popped {j} values.");
    }
}
