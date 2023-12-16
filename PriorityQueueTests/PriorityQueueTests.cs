using PriorityQueue;

namespace PriorityQueueTests
{

    [TestFixture]
    public abstract class PriorityQueueTests
    {
        public readonly Random rand = new Random(1234);
        public abstract IPriorityQueue<int> CreateInstance();

        [Test]
        public void SizeTest()
        {
            IPriorityQueue<int> priorityQueue = CreateInstance();

            int[] values = new int[] { 3, 2, 3, 1, 4, 5, 8, 1, -5, 0, -45, 1, 15 };
            for (int i = 0; i < values.Length; ++i)
            {
                priorityQueue.Insert(values[i]);
            }

            Assert.That(priorityQueue.Size, Is.EqualTo(values.Length), $"Size of the queue should be {values.Length}");
        }

        [Test]
        public void MaxTest()
        {
            IPriorityQueue<int> priorityQueue = CreateInstance();
            Assert.Throws<InvalidOperationException>(() => priorityQueue.Max());

            int[] values = new int[] { -45, 5, 6, 7, 9, 9, 9, 24, 5, 0, 0 };
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
            IPriorityQueue<int> priorityQueue = CreateInstance();
            Assert.Throws<InvalidOperationException>(() => priorityQueue.DeleteMax());

            int[] values = new int[] { -45, 5, 6, 7, 9, 9, 9, 24, 5, 0, 0 };
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
            IPriorityQueue<int> priorityQueue = CreateInstance();

            int[] values = new int[] { 3, 2, 3, 1, 4, 5, 8, 1, -5, 0, -45, 1, 15 };
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

            Assert.That(values.Length,  Is.EqualTo(j), $"Should have popped {j} values.");
        }

        [Test, CancelAfter(3500)]
        public void RandTest()
        {
            IPriorityQueue<int> priorityQueue = CreateInstance();

            const int N = 100_000;

            var values = new List<int>(N);
            for (int i = 0; i < N; ++i)
            {
                values.Add(rand.Next());
                priorityQueue.Insert(values[i]);
            }

            Assert.That(priorityQueue.Size,  Is.EqualTo(N), $"Size should be {N}");

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

            Assert.That(N,  Is.EqualTo(j), $"Should have popped {j} values.");
        }
    }

}
