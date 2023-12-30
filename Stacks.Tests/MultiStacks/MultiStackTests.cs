using System;
using System.Collections.Generic;
using NUnit.Framework;
using Stacks.MultiStacks;

namespace Stacks.Tests.MultiStacks;

[TestFixture]
public class MultiStackTests
{
    private readonly Random _random = new(21434);

    private static MultiStack<int> CreateStack(int stacksNum)
    {
        return new(stacksNum);
    }
    
    [Test]
    public void Constructor_WithPositiveStackNum_InitializesCorrectly()
    {
        var stacksNum = 3;
        var multiStack = CreateStack(stacksNum);

        Assert.That(multiStack, Is.Not.Null);
        Assert.That(multiStack.AreAllStacksEmpty(), Is.True);
    }

    [Test]
    public void Push_Pop_SingleStack_BehavesCorrectly()
    {
        var multiStack = CreateStack(1);
        var value = 5;

        multiStack.Push(value);
        Assert.That(multiStack.IsEmpty(), Is.False);
        Assert.That(multiStack.Peek(), Is.EqualTo(value));

        var poppedValue = multiStack.Pop();
        Assert.That(poppedValue, Is.EqualTo(value));
        Assert.That(multiStack.IsEmpty(), Is.True);
    }

    [Test]
    public void Push_InvalidStackNum_ThrowsArgumentOutOfRangeException()
    {
        var multiStack = CreateStack(1);
        var invalidStackNum = 2;
        var value = 5;

        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => multiStack.Push(invalidStackNum, value));
        Assert.That(ex!.ParamName, Is.EqualTo("stackNum"));
    }

    [Test]
    public void Pop_EmptyStack_ThrowsInvalidOperationException()
    {
        var multiStack = CreateStack(1);

        Assert.Throws<InvalidOperationException>(() => multiStack.Pop());
    }

    [Test]
    public void Peek_EmptyStack_ThrowsInvalidOperationException()
    {
        var multiStack = CreateStack(1);

        Assert.Throws<InvalidOperationException>(() => multiStack.Peek());
    }

    [Test]
    public void AreAllStacksEmpty_Initially_ReturnsTrue()
    {
        var multiStack = CreateStack(3);

        Assert.That(multiStack.AreAllStacksEmpty(), Is.True);
    }

    [Test]
    public void IsEmpty_SpecificStack_Initially_ReturnsTrue()
    {
        var multiStack = CreateStack(3);

        Assert.That(multiStack.IsEmpty(1), Is.True);
    }

    [Test]
    public void Push_Pop_MultipleStacks_BehavesCorrectly()
    {
        var multiStack = CreateStack(3);
        multiStack.Push(0, 10);
        multiStack.Push(1, 20);
        multiStack.Push(2, 30);

        Assert.That(multiStack.Pop(0), Is.EqualTo(10));
        Assert.That(multiStack.Pop(1), Is.EqualTo(20));
        Assert.That(multiStack.Pop(2), Is.EqualTo(30));
    }

    [Test]
    public void Push_Pop_Reverse_MultipleStacks_BehavesCorrectly()
    {
        var multiStack = CreateStack(3);
        multiStack.Push(2, 10);
        multiStack.Push(1, 20);
        multiStack.Push(0, 30);

        Assert.That(multiStack.Pop(0), Is.EqualTo(30));
        Assert.That(multiStack.Pop(1), Is.EqualTo(20));
        Assert.That(multiStack.Pop(2), Is.EqualTo(10));
    }

    [Test]
    public void IsEmpty_AfterPushPop_ReturnsTrue()
    {
        var multiStack = CreateStack(1);
        multiStack.Push(10);
        multiStack.Pop();

        Assert.That(multiStack.IsEmpty(), Is.True);
    }

    [Test]
    public void Peek_AfterMultiplePush_ReturnsLastElement()
    {
        var multiStack = CreateStack(1);
        multiStack.Push(10);
        multiStack.Push(20);

        Assert.That(multiStack.Peek(), Is.EqualTo(20));
    }

    [Test]
    public void ValidateStackNum_InvalidStackNum_ThrowsArgumentOutOfRangeException()
    {
        var multiStack = CreateStack(2);

        Assert.Throws<ArgumentOutOfRangeException>(() => multiStack.IsEmpty(3));
    }

    [Test]
    public void Constructor_NegativeStackNum_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateStack(-1));
    }

    [Test]
    public void AreAllStacksEmpty_WithNonEmptyStacks_ReturnsFalse()
    {
        var multiStack = CreateStack(2);
        multiStack.Push(0, 10);

        Assert.That(multiStack.AreAllStacksEmpty(), Is.False);
    }

    [Test]
    public void IsEmpty_NonDefaultStack_ReturnsCorrectValue()
    {
        var multiStack = CreateStack(2);
        multiStack.Push(1, 20);

        Assert.That(multiStack.IsEmpty(0), Is.True);
        Assert.That(multiStack.IsEmpty(1), Is.False);
    }

    [Test]
    public void Push_OnFullStack_ThrowsExceptionOrHandlesGracefully()
    {
        // Assuming the capacity is set and known. Implement the test accordingly.
    }

    [Test]
    public void Pop_SpecificStack_ReturnsCorrectValue()
    {
        var multiStack = CreateStack(2);
        multiStack.Push(1, 20);

        Assert.That(multiStack.Pop(1), Is.EqualTo(20));
    }

    [Test]
    public void Peek_SpecificStack_ReturnsTopElement()
    {
        var multiStack = CreateStack(2);
        multiStack.Push(1, 30);
        multiStack.Push(1, 40);

        Assert.That(multiStack.Peek(1), Is.EqualTo(40));
    }

    [Test]
    public void ValidateStackNum_OutOfRange_ThrowsArgumentOutOfRangeException()
    {
        var multiStack = CreateStack(2);

        Assert.Throws<ArgumentOutOfRangeException>(() => multiStack.IsEmpty(3));
    }

    [TestCase(4, 10)]
    [TestCase(3, 100)]
    [TestCase(3, 100_000)]
    [TestCase(27, 100_000)]
    [TestCase(16, 100_000)]
    [TestCase(99, 1_000_000)]
    public void Push_Peek_Pop_Random_Test(int stacksNum, int countToPush)
    {
        var multiStack = CreateStack(stacksNum);

        var values = new Dictionary<int, List<int>>();
        for (int i = 0; i < countToPush; ++i)
        {
            var stackNum = _random.Next(0, stacksNum);
            var value = _random.Next(0, 1_000);
            multiStack.Push(stackNum, value);
            Assert.That(multiStack.Peek(stackNum), Is.EqualTo(value));

            values.TryAdd(stackNum, []);
            values[stackNum].Add(value);
        }

        for (int i = 0; i < stacksNum; ++i)
        {
            var stackNum = _random.Next(0, stacksNum);
            var valuesForStack = values[stackNum];
            int k = valuesForStack.Count - 1;
            while (!multiStack.IsEmpty(stackNum))
            {
                Assert.That(multiStack.Pop(stackNum), Is.EqualTo(valuesForStack[k]));
                --k;
            }
        }
    }
}
