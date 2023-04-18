using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сбалансированные_деревья
{
    public class AVLTree<T> where T : IComparable<T>
    {
        private class Node
        {
            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Height { get; set; }

            public Node(T value)
            {
                Value = value;
                Height = 1;
            }
        }

        private Node _root;

        public void Insert(T value)
        {
            _root = Insert(_root, value);
        }

        private Node Insert(Node node, T value)
        {
            if (node == null)
            {
                return new Node(value);
            }

            int cmp = value.CompareTo(node.Value);
            if (cmp < 0)
            {
                node.Left = Insert(node.Left, value);
            }
            else if (cmp > 0)
            {
                node.Right = Insert(node.Right, value);
            }
            else
            {
                return node; // элемент уже существует
            }

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            int balance = BalanceFactor(node);

            if (balance > 1 && value.CompareTo(node.Left.Value) < 0)
            {
                return RotateRight(node);
            }
            if (balance < -1 && value.CompareTo(node.Right.Value) > 0)
            {
                return RotateLeft(node);
            }
            if (balance > 1 && value.CompareTo(node.Left.Value) > 0)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
            if (balance < -1 && value.CompareTo(node.Right.Value) < 0)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        public bool Search(T value)
        {
            return Search(_root, value);
        }

        private bool Search(Node node, T value)
        {
            if (node == null)
            {
                return false;
            }

            int cmp = value.CompareTo(node.Value);
            if (cmp < 0)
            {
                return Search(node.Left, value);
            }
            else if (cmp > 0)
            {
                return Search(node.Right, value);
            }
            else
            {
                return true;
            }
        }

        private static int Height(Node node)
        {
            return node == null ? 0 : node.Height;
        }

        private static int BalanceFactor(Node node)
        {
            return node == null ? 0 : Height(node.Left) - Height(node.Right);
        }

        private static Node RotateRight(Node node)
        {
            Node left = node.Left;
            node.Left = left.Right;
            left.Right = node;

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
            left.Height = 1 + Math.Max(Height(left.Left), Height(left.Right));

            return left;
        }

        private static Node RotateLeft(Node node)
        {
            Node right = node.Right;
            node.Right = right.Left;
            right.Left = node;

            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
            right.Height = 1 + Math.Max(Height(right.Left), Height(right.Right));

            return right;
        }
    }

}
