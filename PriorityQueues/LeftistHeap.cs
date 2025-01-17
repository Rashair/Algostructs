﻿using System;

namespace PriorityQueues;

public class LeftistHeap<T> : IMergeablePriorityQueue<LeftistHeap<T>, T>
    where T : IComparable<T>
{
    private Node _root = null;

    public LeftistHeap()
    {
        Size = 0;
    }
    private LeftistHeap(Node newRoot, int newSize)
    {
        _root = newRoot;
        Size = newSize;
    }

    public int Size { get; private set; }

    public void DeleteMax()
    {
        if (Size == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        _root = Union(_root.Left, _root.Right);
        Size -= 1;
    }
    public void Insert(T val)
    {
        _root = Union(_root, new Node(val));
        Size += 1;
    }

    public T Max()
    {
        if (Size == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        return _root.Key;
    }

    public LeftistHeap<T> Union(LeftistHeap<T> q)
    {
        return new LeftistHeap<T>(Union(_root, q._root), Size + q.Size);
    }

    private Node Union(Node node1, Node node2)
    {
        if (node2 == null)
        {
            return node1;
        }
        if (node1 == null)
        {
            return node2;
        }

        Node newRoot;
        if (node1.Key.CompareTo(node2.Key) > 0)
        {
            newRoot = node1;
            newRoot.Right = Union(node1.Right, node2);
        }
        else
        {
            newRoot = node2;
            newRoot.Right = Union(node2.Right, node1);
        }

        if (newRoot.Left == null || newRoot.Left.Npl < newRoot.Right.Npl)
        {
            (newRoot.Left, newRoot.Right) = (newRoot.Right, newRoot.Left);
        }

        // Correct npl
        newRoot.Npl = (newRoot.Right == null ? 0 : newRoot.Right.Npl + 1);

        return newRoot;
    }

    private class Node : IComparable<Node>
    {
        public T Key;

        public Node Left, Right;
        public int Npl;

        public Node(T val)
        {
            Key = val;
            Left = null;
            Right = null;
            Npl = 0;
        }

        public int CompareTo(Node other)
        {
            return Key.CompareTo(other.Key);
        }
    }
}
