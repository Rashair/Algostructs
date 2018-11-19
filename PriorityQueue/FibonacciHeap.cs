using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueue
{
    public class FibonacciHeap<T> : IMergeablePriorityQueue<FibonacciHeap<T>, T>
        where T : IComparable<T>
    {
        private Node root = null;
        private Node max = null;

        public FibonacciHeap()
        {
            Size = 0;
        }

        private FibonacciHeap(Node newRoot, int newSize)
        {
            root = newRoot;
            Size = newSize;
        }

        public int Size { get; private set; }

        public void DeleteMax()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            max.Prev.Next = max.Next;
            AddToEnd(max.Child);
            Size -= 1;
            max = null;

            Consolidate();
        }

        public void Insert(T val)
        {
            AddToEnd(new Node(val));
            Size += 1;
        }

        public T Max()
        {
            if(Size == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            return max.Key;
        }

        public FibonacciHeap<T> Union(FibonacciHeap<T> Queue)
        {
            if (root == null)
            {
                root = Queue.root;
                max = Queue.max;
                Size = Queue.Size;
            }
            else if(Queue.root != null)
            {
                max = max.CompareTo(Queue.max) > 0 ? max : Queue.max;
                root.Prev.Next = Queue.root;
                Size = Size + Queue.Size;
            }

            return this;
        }

        private void Consolidate()
        {
            var heightToNode = new Dictionary<int, Node>();
            while (root != null)
            {
                Node tree = Extract(ref root);
                while (heightToNode.ContainsKey(tree.Rank) && heightToNode[tree.Rank] != null)
                {
                    tree = MergeTrees(tree, heightToNode[tree.Rank]);
                    heightToNode[tree.Rank - 1] = null;
                }

                heightToNode[tree.Rank] = tree;
            }
            //*head = NULL;
            foreach (var node in heightToNode.Values)
            {
                if (node != null)
                {
                    AddToEnd(node);
                }
            }
        }
        private void AddToEnd(Node node)
        {
            if(node == null)
            {
                return;
            }
            if(root == null)
            {
                root = node;
                max = node;
                return;
            }

            root.Prev.Next = node;
            node.Prev = root.Prev;
            root.Prev = node;

            if(max == null || max.CompareTo(node) < 0)
            {
                max = node;
            }
        }

        private static Node MergeTrees(Node node1, Node node2) // Both equal ranks and not null
        {
            if (node1.CompareTo(node2) < 0)
            {
                (node1, node2) = (node2, node1);
            }

            Node newRoot = node1;
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
            Node nodeNext = node.Next;
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
            public int Mark;

            public Node Prev, Next, Child;

            public Node(T val)
            {
                Key = val;
                Rank = 0;
                Mark = 0;

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
}
