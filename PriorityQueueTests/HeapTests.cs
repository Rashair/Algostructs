using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriorityQueue;

namespace PriorityQueueTests
{

    [TestClass]
    public class HeapTests : PriorityQueueTests
    {
        public override IPriorityQueue<int> CreateInstance()
        {
            return new Heap<int>();
        }

        [TestMethod]
        public void HeapBuildSizeTest()
        {
            var tab = new int[] { 1, 2, 3, 4, 5, -5100, 33, 3, 4, 4, 56, 7, 55 };

            var heap = new Heap<int>(tab);

            Assert.AreEqual(heap.Size, tab.Length);
        }

        [TestMethod]
        public void HeapBuildMaxTest()
        {
            var tab = new int[] { 1, 2, 3, 4, 5, -5100, 33, 3, 4, 4, 56, 7, 55 };

            var heap = new Heap<int>(tab);

            Assert.AreEqual(heap.Max(), tab.Max());
        }

        [TestMethod]
        public void HeapBuildOrderTest()
        {
            var tab = new int[] { 1, 2, 3, 4, 5, -5100, 33, 3, 4, 4, 56, 7, 55 };

            var heap = new Heap<int>(tab);

            Array.Sort(tab);
            Array.Reverse(tab);

            for (int i = 0; i < tab.Length; ++i)
            {
                int max = heap.Max();
                heap.DeleteMax();

                Assert.AreEqual(max, tab[i]);
            }
        }

        [TestMethod]
        public void HeapDeleteTest()
        {
            var tab = new int[] { 1, 2, 3, 4, 5, -5100, 33, 3, 4, 4, 56, 7, 55 };

            var heap = new Heap<int>(tab);

            heap.Delete(heap.Size - 1);
            heap.Delete((heap.Size - 1 - 1) / 2);
            heap.Delete(1);

            Assert.AreEqual(tab.Length - 3, heap.Size);
            Assert.AreEqual(tab.Max(), heap.Max());
        }
    }

}