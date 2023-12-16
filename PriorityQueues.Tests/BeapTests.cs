namespace PriorityQueues.Tests;

[TestFixture]
public class BeapTests : PriorityQueueTests
{
    protected override IPriorityQueue<int> CreateInstance()
    {
        return new Beap<int>();
    }

    [Test]
    public void HelpMethodsTest1()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Beap<int>.RowCol2Index(2, 2), Is.EqualTo(5));
            Assert.That(Beap<int>.Index2RowCol(5), Is.EqualTo((2,2)));
        });
    }

    [Test]
    public void HelpMethodsTest2()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Beap<int>.RowCol2Index(2, 1), Is.EqualTo(4));
            Assert.That(Beap<int>.Index2RowCol(4), Is.EqualTo((2, 1)));
        });
    }

    [Test]
    public void HelpMethodsTest3()
    {
        Assert.Multiple(() =>
        {
            Assert.That(Beap<int>.RowCol2Index(2, 0), Is.EqualTo(3));
            Assert.That((2, 0), Is.EqualTo(Beap<int>.Index2RowCol(3)));
        });
    }

    [Test]
    public void HelpMethodsTestRand()
    {
        int i = Rand.Next() % (int)Math.Sqrt(Int32.MaxValue);
        int j = Rand.Next() % (i + 1);
        Assert.That((i, j), Is.EqualTo(Beap<int>.Index2RowCol(Beap<int>.RowCol2Index(i, j))));

        int k = Rand.Next();
        Assert.That(k, Is.EqualTo(Beap<int>.RowCol2Index(Beap<int>.Index2RowCol(k).Item1, Beap<int>.Index2RowCol(k).Item2)));
    }

    [Test]
    public void SearchTest()
    {
        var beap = new Beap<int>();

        int[] values = { -5, 0, -45, 1, 15, -2, 55, 339, 1};
        foreach (var t in values)
        {
            beap.Insert(t);
        }

        Assert.Multiple(() =>
        {
            Assert.That(beap[beap.Search(15)], Is.EqualTo(15), "Index of 15 should be 0");
            Assert.That(beap[beap.Search(0)], Is.EqualTo(0), "Index of 0 should be 1");
            Assert.That(beap[beap.Search(1)], Is.EqualTo(1), "Index of 1 should be 2");
            Assert.That(beap[beap.Search(-5)], Is.EqualTo(-5), "Index of -5 should be 3");
            Assert.That(beap[beap.Search(-45)], Is.EqualTo(-45), "Index of -45 should be 4");
            Assert.That(beap[beap.Search(-2)], Is.EqualTo(-2), "Index of -2 should be 5");

            Assert.That(beap[beap.Search(339)], Is.EqualTo(339));

            Assert.That(beap.Search(-1), Is.EqualTo(-1), "Should not have find value -1 in beap");
            Assert.That(beap.Search(4), Is.EqualTo(-1), "Should not have find value 4 in beap");
        });
    }

    [Test]
    public void SearchTest1()
    {
        var beap = new Beap<int>();

        int[] values = { -5, 0, -45, 1, 15};
        foreach (var t in values)
        {
            beap.Insert(t);
        }

        Assert.Multiple(() =>
        {
            Assert.That(beap[beap.Search(15)], Is.EqualTo(15), "Index of 15 should be 0");
            Assert.That(beap[beap.Search(0)], Is.EqualTo(0), "Index of 0 should be 1");
            Assert.That(beap[beap.Search(1)], Is.EqualTo(1), "Index of 1 should be 2");
            Assert.That(beap[beap.Search(-5)], Is.EqualTo(-5), "Index of -5 should be 3");
            Assert.That(beap[beap.Search(-45)], Is.EqualTo(-45), "Index of -45 should be 4");

            Assert.That(beap.Search(-2), Is.EqualTo(-1), "Should not have find value -1 in beap");
        });
    }
}
