using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Queues.Tests;

[TestFixture]
[TestOf(typeof(TwoStacksQueue<>))]
public class TwoStacksQueueTests
{
    private readonly Random _random = new(21434);

    private static TwoStacksQueue<int> CreateQueue()
    {
        return new();
    }

    [Test]
    public void Add_Remove_SingleValue_BehavesCorrectly()
    {
        var queue = CreateQueue();
        var value = 5;

        queue.Add(value);
        Assert.That(queue.IsEmpty(), Is.False);
        Assert.That(queue.Peek(), Is.EqualTo(value));

        var poppedValue = queue.Remove();
        Assert.That(poppedValue, Is.EqualTo(value));
        Assert.That(queue.IsEmpty(), Is.True);
    }

    [Test]
    public void Remove_EmptyQueue_ThrowsInvalidOperationException()
    {
        var queue = CreateQueue();

        Assert.Throws<InvalidOperationException>(() => queue.Remove());
    }

    [Test]
    public void Peek_EmptyQueue_ThrowsInvalidOperationException()
    {
        var queue = CreateQueue();

        Assert.Throws<InvalidOperationException>(() => queue.Peek());
    }

    [Test]
    public void IsEmpty_Initially_ReturnsTrue()
    {
        var queue = CreateQueue();

        Assert.That(queue.IsEmpty(), Is.True);
    }

    [Test]
    public void Add_Remove_BehavesCorrectly()
    {
        var queue = CreateQueue();
        queue.Add(10);
        queue.Add(20);
        queue.Add(30);

        Assert.That(queue.Remove(), Is.EqualTo(10));
        Assert.That(queue.Remove(), Is.EqualTo(20));
        Assert.That(queue.Remove(), Is.EqualTo(30));
    }

    [Test]
    public void Add_Remove_Reverse_BehavesCorrectly()
    {
        var queue = CreateQueue();
        queue.Add(30);
        queue.Add(20);
        queue.Add(10);

        Assert.That(queue.Remove(), Is.EqualTo(30));
        Assert.That(queue.Remove(), Is.EqualTo(20));
        Assert.That(queue.Remove(), Is.EqualTo(10));
    }

    [Test]
    public void Add_Remove_Alternating_BehavesCorrectly()
    {
        var queue = CreateQueue();
        queue.Add(30);
        queue.Remove();
        queue.Add(20);
        queue.Remove();
        queue.Add(10);
        queue.Remove();

        queue.Add(30);

        Assert.That(queue.Peek(), Is.EqualTo(30));
        Assert.That(queue.Peek(), Is.EqualTo(30));
        Assert.That(queue.Remove(), Is.EqualTo(30));
    }

    [Test]
    public void Add_Remove_FullToEmpty_BehavesCorrectly()
    {
        var queue = CreateQueue();
        queue.Add(30);
        queue.Add(20);
        queue.Remove();
        queue.Remove();
        queue.Add(40);
        queue.Add(60);
        queue.Add(70);
        queue.Remove();
        queue.Remove();

        Assert.That(queue.Peek(), Is.EqualTo(70));
        Assert.That(queue.Peek(), Is.EqualTo(70));
        Assert.That(queue.Remove(), Is.EqualTo(70));
    }

    [Test]
    public void Add_Remove_SameValue_BehavesCorrectly()
    {
        var queue = CreateQueue();
        const int value = int.MaxValue;
        queue.Add(value);
        queue.Add(value);
        queue.Remove();
        queue.Add(value);
        queue.Add(value);
        queue.Add(value);

        Assert.That(queue.Peek(), Is.EqualTo(value));
        Assert.That(queue.Peek(), Is.EqualTo(value));
        Assert.That(queue.Remove(), Is.EqualTo(value));
    }

    [Test]
    public void IsEmpty_AfterAddRemove_ReturnsTrue()
    {
        var queue = CreateQueue();
        queue.Add(10);
        queue.Remove();

        Assert.That(queue.IsEmpty(), Is.True);
    }

    [Test]
    public void Peek_AfterMultipleAdd_ReturnsLastElement()
    {
        var queue = CreateQueue();
        queue.Add(10);
        queue.Add(20);

        Assert.That(queue.Peek(), Is.EqualTo(10));
    }

    [Test]
    public void AreIsEmpty_WithNonEmptyQueues_ReturnsFalse()
    {
        var queue = CreateQueue();
        queue.Add(10);

        Assert.That(queue.IsEmpty(), Is.False);
    }

    [TestCase(10)]
    [TestCase(100)]
    [TestCase(100_000)]
    [TestCase(100_000)]
    [TestCase(100_000)]
    [TestCase(1_000_000)]
    public void Add_Peek_Remove_Random_BehavesCorrectly(int countToAdd)
    {
        var queue = CreateQueue();

        var firstValue = _random.Next(0, 1_000);
        var values = new List<int> {firstValue};
        queue.Add(firstValue);
        
        for (int i = 1; i < countToAdd; ++i)
        {
            var value = _random.Next(0, 1_000);
            queue.Add(value);
            Assert.That(queue.Peek(), Is.EqualTo(firstValue));
            values.Add(value);
        }
        
        int k = 0;
        while (!queue.IsEmpty())
        {
            Assert.That(queue.Remove(), Is.EqualTo(values[k]));
            ++k;
        }
    }
}
