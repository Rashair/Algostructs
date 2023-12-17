using System;

namespace PriorityQueues;

public class BinomialHeap<T> : IMergeablePriorityQueue<BinomialHeap<T>, T>
    where T : IComparable<T>
{
    private Node _root = null;

    public BinomialHeap()
    {
        Size = 0;
    }

    private BinomialHeap(Node newRoot, int newSize)
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

        var max = FindMax();
        if (max == null)
        {
            return;
        }

        if (max.Next != null)
        {
            max.Next.Prev = max.Prev;
        }
        else
        {
            _root.Prev = max.Prev;
        }

        if (max != _root)
        {
            max.Prev.Next = max.Next;
        }
        else
        {
            _root = _root.Next;
        }

        _root = Union(_root, max.Child);
        Size -= 1;
    }

    public void Insert(T val)
    {
        _root = Union(_root, new Node(val));
        Size += 1;
    }

    public T Max()
    {
        if(Size == 0)
        {
            throw new InvalidOperationException("Queue is empty");
        }

        return FindMax().Key;
    }

    public BinomialHeap<T> Union(BinomialHeap<T> queue)
    {
        return new BinomialHeap<T>(Union(_root, queue._root), Size + queue.Size);
    }

    private Node FindMax()
    {
        var it = _root.Next;
        var max = _root;
        while (it != null)
        {
            if (max.CompareTo(it) < 0)
            {
                max = it;
            }

            it = it.Next;
        }

        return max;
    }
    private static Node Union(Node node1, Node node2)
    {
        if (node2 == null)
        {
            return node1;
        }
        if (node1 == null)
        {
            return node2;
        }

        if (node1.Rank > node2.Rank)
        {
            (node1, node2) = (node2, node1);
        }

        Node tree1, tree2, tree3;
        Node node3;

        if (node1.Rank < node2.Rank)
        {
            tree1 = Extract(ref node1);
            node3 = Union(node1, node2);

            tree1.Prev = node3.Prev;
            tree1.Next = node3;
            node3.Prev = tree1;

            return tree1;
        }

        tree1 = Extract(ref node1);
        tree2 = Extract(ref node2);
        tree3 = MergeTrees(tree1, tree2);

        node3 = Union(node1, node2);

        return Union(node3, tree3);
    }

    private static Node MergeTrees(Node node1, Node node2) // Both equal ranks and not null
    {
        if (node1.CompareTo(node2) < 0)
        {
            (node1, node2) = (node2, node1);
        }

        var newRoot = node1;
        if (newRoot.Child == null)
        {
            newRoot.Child = node2;
        }
        else
        {
            node2.Prev = newRoot.Child.Prev;

            newRoot.Child.Prev.Next = node2;
            newRoot.Child.Prev = node2;
        }

        newRoot.Rank += 1;

        return newRoot;
    }

    private static Node Extract(ref Node node)
    {
        var nodeNext = node.Next;
        if (nodeNext != null)
        {
            nodeNext.Prev = node.Prev;
            node.Prev = node;
            node.Next = null;
        }

        (node, nodeNext) = (nodeNext, node);

        return nodeNext;
    }

    private class Node : IComparable<Node>
    {
        public T Key;

        public int Rank;

        public Node Prev, Next, Child;

        public Node(T val)
        {
            Key = val;
            Rank = 0;

            Prev = this;
            Next = null;
            Child = null;
        }

        public int CompareTo(Node other)
        {
            return Key.CompareTo(other.Key);
        }
    }
}
