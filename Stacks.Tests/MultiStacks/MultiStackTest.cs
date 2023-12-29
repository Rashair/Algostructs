using System;
using NUnit.Framework;
using Stacks.MultiStacks;

namespace Stacks.Tests.MultiStacks;

[TestFixture]
public class MultiStackTests
{
    [Test]
    public void Constructor_WithPositiveStackNum_InitializesCorrectly()
    {
        var stacksNum = 3;
        var multiStack = new MultiStack<int>(stacksNum);

        Assert.That(multiStack, Is.Not.Null);
        Assert.That(multiStack.AreAllStacksEmpty(), Is.True);
    }

    [Test]
    public void Push_Pop_SingleStack_BehavesCorrectly()
    {
        var multiStack = new MultiStack<int>(1);
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
        var multiStack = new MultiStack<int>(1);
        var invalidStackNum = 2;
        var value = 5;

        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => multiStack.Push(invalidStackNum, value));
        Assert.That(ex!.ParamName, Is.EqualTo("stackNum"));
    }

    [Test]
    public void Pop_EmptyStack_ThrowsInvalidOperationException()
    {
        var multiStack = new MultiStack<int>(1);

        Assert.Throws<InvalidOperationException>(() => multiStack.Pop());
    }

    [Test]
    public void Peek_EmptyStack_ThrowsInvalidOperationException()
    {
        var multiStack = new MultiStack<int>(1);

        Assert.Throws<InvalidOperationException>(() => multiStack.Peek());
    }

    [Test]
    public void AreAllStacksEmpty_Initially_ReturnsTrue()
    {
        var multiStack = new MultiStack<int>(3);

        Assert.That(multiStack.AreAllStacksEmpty(), Is.True);
    }

    [Test]
    public void IsEmpty_SpecificStack_Initially_ReturnsTrue()
    {
        var multiStack = new MultiStack<int>(3);

        Assert.That(multiStack.IsEmpty(1), Is.True);
    }

    [Test]
    public void Push_Pop_MultipleStacks_BehavesCorrectly()
    {
        var multiStack = new MultiStack<int>(3);
        multiStack.Push(0, 10);
        multiStack.Push(1, 20);
        multiStack.Push(2, 30);

        Assert.That(multiStack.Pop(0), Is.EqualTo(10));
        Assert.That(multiStack.Pop(1), Is.EqualTo(20));
        Assert.That(multiStack.Pop(2), Is.EqualTo(30));
    }

    [Test]
    public void IsEmpty_AfterPushPop_ReturnsTrue()
    {
        var multiStack = new MultiStack<int>(1);
        multiStack.Push(10);
        multiStack.Pop();

        Assert.That(multiStack.IsEmpty(), Is.True);
    }

    [Test]
    public void Peek_AfterMultiplePush_ReturnsLastElement()
    {
        var multiStack = new MultiStack<int>(1);
        multiStack.Push(10);
        multiStack.Push(20);

        Assert.That(multiStack.Peek(), Is.EqualTo(20));
    }

    [Test]
    public void ValidateStackNum_InvalidStackNum_ThrowsArgumentOutOfRangeException()
    {
        var multiStack = new MultiStack<int>(2);

        Assert.Throws<ArgumentOutOfRangeException>(() => multiStack.IsEmpty(3));
    }

    [Test]
    public void Constructor_NegativeStackNum_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new MultiStack<int>(-1));
    }
}
