using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сбалансированные_деревья
{
    public class BTreeNode<T> where T : IComparable<T>
    {
        private int _degree;

        public List<T> Values { get; private set; }

        public List<BTreeNode<T>> Children { get; private set; }

        public BTreeNode<T> Parent { get; set; }

        public bool IsLeaf { get; set; }

        public bool IsFull => Values.Count == 2 * _degree - 1;

        public bool IsRoot => Parent == null;

        public BTreeNode(int degree)
        {
            _degree = degree;
            Values = new List<T>();
            Children = new List<BTreeNode<T>>();
        }

        public void InsertNonFull(T value)
        {
            int i = Values.Count - 1;
            if (IsLeaf)
            {
                // вставка значения в листовой узел
                while (i >= 0 && value.CompareTo(Values[i]) < 0)
                {
                    i--;
                }
                Values.Insert(i + 1, value);
            }
            else
            {
                // вставка значения в нелистовой узел
                while (i >= 0 && value.CompareTo(Values[i]) < 0)
                {
                    i--;
                }
                Children[i + 1].InsertNonFull(value);
            }
        }

        public void Split(int childIndex, BTreeNode<T> right)
        {
            // разбиение дочернего узла на два
            BTreeNode<T> left = Children[childIndex];
            right.Values.AddRange(left.Values.GetRange(_degree, _degree - 1));
            left.Values.RemoveRange(_degree - 1, _degree);
            if (!left.IsLeaf)
            {
                right.Children.AddRange(left.Children.GetRange(_degree, _degree));
                left.Children.RemoveRange(_degree, _degree);
            }
            Children.Insert(childIndex + 1, right);

            // вставка среднего значения в родительский узел
            Values.Insert(childIndex, left.Values[_degree - 1]);
            left.Values.RemoveAt(_degree - 1);
            right.Parent = this;
            left.Parent = this;
        }

        public BTreeNode<T> Search(T value)
        {
            int i = 0;
            while (i < Values.Count && value.CompareTo(Values[i]) > 0)
            {
                i++;
            }
            if (i < Values.Count && value.CompareTo(Values[i]) == 0)
            {
                return this;
            }
            if (IsLeaf)
            {
                return null;
            }
            return Children[i].Search(value);
        }
    }

}
