using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PriorityQueue;

namespace PriorityQueueTests
{ 
    [TestClass]
    public class BeapTests : PriorityQueueTests
    {
        public override IPriorityQueue<int> CreateInstance()
        {
            return new Beap<int>();
        }

        [TestMethod]
        public void HelpMethodsTest1()
        {
            Assert.AreEqual(5, Beap<int>.RowCol2Index(2, 2));
            Assert.AreEqual((2, 2), Beap<int>.Index2RowCol(5));
        }

        [TestMethod]
        public void HelpMethodsTest2()
        {
            Assert.AreEqual(4, Beap<int>.RowCol2Index(2, 1));
            Assert.AreEqual((2, 1), Beap<int>.Index2RowCol(4));
        }

        [TestMethod]
        public void HelpMethodsTest3()
        {
            Assert.AreEqual(3, Beap<int>.RowCol2Index(2, 0));
            Assert.AreEqual((2, 0), Beap<int>.Index2RowCol(3));
        }

        [TestMethod]
        public void HelpMethodsTestRand()
        {
            int i = rand.Next() % (int)Math.Sqrt(Int32.MaxValue);
            int j = rand.Next() % (i + 1);
            Assert.AreEqual((i, j), Beap<int>.Index2RowCol(Beap<int>.RowCol2Index(i, j)));

            int k = rand.Next();
            Assert.AreEqual(k, Beap<int>.RowCol2Index(Beap<int>.Index2RowCol(k).Item1, Beap<int>.Index2RowCol(k).Item2));
        }

        [TestMethod]
        public void SearchTest()
        {
            var beap = new Beap<int>();

            int[] values = new int[] { -5, 0, -45, 1, 15, -2, 55, 339, 1};
            for (int i = 0; i < values.Length; ++i)
            {
                beap.Insert(values[i]);
            }

            Assert.AreEqual(beap[beap.Search(15)], 15, "Index of 15 should be 0");
            Assert.AreEqual(beap[beap.Search(0)], 0, "Index of 0 should be 1");
            Assert.AreEqual(beap[beap.Search(1)], 1, "Index of 1 should be 2");
            Assert.AreEqual(beap[beap.Search(-5)], -5, "Index of -5 should be 3");
            Assert.AreEqual(beap[beap.Search(-45)], -45, "Index of -45 should be 4");
            Assert.AreEqual(beap[beap.Search(-2)], -2, "Index of -2 should be 5");

            Assert.AreEqual(beap[beap.Search(339)], 339);

            Assert.AreEqual(-1, beap.Search(-1), "Should not have find value -1 in beap");
            Assert.AreEqual(-1, beap.Search(4), "Should not have find value 4 in beap");
        }

        [TestMethod]
        public void SearchTest1()
        {
            var beap = new Beap<int>();

            int[] values = new int[] { -5, 0, -45, 1, 15};
            for (int i = 0; i < values.Length; ++i)
            {
                beap.Insert(values[i]);
            }

            Assert.AreEqual(beap[beap.Search(15)], 15, "Index of 15 should be 0");
            Assert.AreEqual(beap[beap.Search(0)], 0, "Index of 0 should be 1");
            Assert.AreEqual(beap[beap.Search(1)], 1, "Index of 1 should be 2");
            Assert.AreEqual(beap[beap.Search(-5)], -5, "Index of -5 should be 3");
            Assert.AreEqual(beap[beap.Search(-45)], -45, "Index of -45 should be 4");

            Assert.AreEqual(-1, beap.Search(-2), "Should not have find value -1 in beap");
        }
    }
}
