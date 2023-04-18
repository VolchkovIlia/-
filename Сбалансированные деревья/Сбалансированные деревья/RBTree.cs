using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сбалансированные_деревья
{
    using System;

    public enum NodeColor { Red, Black }

    public class RBTree<T> where T : IComparable<T>
    {
        private RBTreeNode<T> _root;

        public RBTree()
        {
            _root = null;
        }

        public void Insert(T value)
        {
            if (_root == null)
            {
                _root = new RBTreeNode<T>(value, NodeColor.Black);
            }
            else
            {
                RBTreeNode<T> newNode = new RBTreeNode<T>(value, NodeColor.Red);
                RBTreeNode<T> parent = null;
                RBTreeNode<T> current = _root;

                while (current != null)
                {
                    parent = current;

                    if (newNode.Value.CompareTo(current.Value) < 0)
                    {
                        current = current.Left;
                    }
                    else
                    {
                        current = current.Right;
                    }
                }

                newNode.Parent = parent;

                if (parent == null)
                {
                    _root = newNode;
                }
                else if (newNode.Value.CompareTo(parent.Value) < 0)
                {
                    parent.Left = newNode;
                }
                else
                {
                    parent.Right = newNode;
                }

                InsertFixup(newNode);
            }
        }

        public RBTreeNode<T> Search(T value)
        {
            RBTreeNode<T> current = _root;

            while (current != null && current.Value.CompareTo(value) != 0)
            {
                if (value.CompareTo(current.Value) < 0)
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }

            return current;
        }

        private void InsertFixup(RBTreeNode<T> node)
        {
            while (node.Parent != null && node.Parent.Color == NodeColor.Red)
            {
                if (node.Parent == node.Parent.Parent.Left)
                {
                    RBTreeNode<T> uncle = node.Parent.Parent.Right;

                    if (uncle != null && uncle.Color == NodeColor.Red)
                    {
                        node.Parent.Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        node.Parent.Parent.Color = NodeColor.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Right)
                        {
                            node = node.Parent;
                            RotateLeft(node);
                        }

                        node.Parent.Color = NodeColor.Black;
                        node.Parent.Parent.Color = NodeColor.Red;
                        RotateRight(node.Parent.Parent);
                    }
                }
                else
                {
                    RBTreeNode<T> uncle = node.Parent.Parent.Left;

                    if (uncle != null && uncle.Color == NodeColor.Red)
                    {
                        node.Parent.Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        node.Parent.Parent.Color = NodeColor.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Left)
                        {
                            node = node.Parent;
                            RotateRight(node);
                        }

                        node.Parent.Color = NodeColor.Black;
                        node.Parent.Parent.Color = NodeColor.Red;
                        RotateLeft(node.Parent.Parent);
                    }
                }
            }

            _root.Color = NodeColor.Black;
        }

        private void RotateLeft(RBTreeNode<T> node)
        {
            RBTreeNode<T> rightChild = node.Right;
            node.Right = rightChild.Left;

            if (rightChild.Left != null)
            {
                rightChild.Left.Parent = node;
            }

            rightChild.Parent = node.Parent;

            if (node.Parent == null)
            {
                _root = rightChild;
            }
            else if (node == node.Parent.Left)
            {
                node.Parent.Left = rightChild;
            }
            else
            {
                node.Parent.Right = rightChild;
            }
            rightChild.Left = node;
            node.Parent = rightChild;
        }

        private void RotateRight(RBTreeNode<T> node)
        {
            RBTreeNode<T> leftChild = node.Left;
            node.Left = leftChild.Right;

            if (leftChild.Right != null)
            {
                leftChild.Right.Parent = node;
            }

            leftChild.Parent = node.Parent;

            if (node.Parent == null)
            {
                _root = leftChild;
            }
            else if (node == node.Parent.Left)
            {
                node.Parent.Left = leftChild;
            }
            else
            {
                node.Parent.Right = leftChild;
            }

            leftChild.Right = node;
            node.Parent = leftChild;
        }

    }
}