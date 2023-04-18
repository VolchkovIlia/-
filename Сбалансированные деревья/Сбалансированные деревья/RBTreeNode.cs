using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сбалансированные_деревья
{
    public class RBTreeNode<T>
    {
        public T Value { get; set; }
        public NodeColor Color { get; set; }
        public RBTreeNode<T> Parent { get; set; }
        public RBTreeNode<T> Left { get; set; }
        public RBTreeNode<T> Right { get; set; }
        public RBTreeNode(T value, NodeColor color)
        {
            Value = value;
            Color = color;
            Parent = null;
            Left = null;
            Right = null;
        }
    }
 }
