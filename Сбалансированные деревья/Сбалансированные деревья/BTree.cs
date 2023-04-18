using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сбалансированные_деревья
{
    public class BTree<T> where T : IComparable<T>
    {
        private int _degree;

        private BTreeNode<T> _root;

        public BTree(int degree)
        {
            _degree = degree;
            _root = new BTreeNode<T>(_degree);
            _root.IsLeaf = true;
        }

        public void Insert(T value)
        {
            BTreeNode<T> root = _root;

            // специальный случай: корень переполнен
            if (root.IsFull)
            {
                // создаем новый корень и делаем его родителем текущего корня
                BTreeNode<T> newRoot = new BTreeNode<T>(_degree);
                newRoot.IsLeaf = false;
                newRoot.Children.Add(root);
                root.Parent = newRoot;

                // разбиваем текущий корень на две части и добавляем новое значение в нужную половину
                BTreeNode<T> left = root;
                BTreeNode<T> right = new BTreeNode<T>(_degree);
                right.IsLeaf = left.IsLeaf;
                left.Split(_degree, right);
                if (value.CompareTo(root.Values[0]) < 0)
                {
                    left.InsertNonFull(value);
                }
                else
                {
                    right.InsertNonFull(value);
                }

                // делаем новый корень текущим корнем
                _root = newRoot;
            }
            else
            {
                root.InsertNonFull(value);
            }
        }

        public bool Search(T value)
        {
            BTreeNode<T> currentNode = _root;

            while (currentNode != null)
            {
                int i = 0;
                while (i < currentNode.Values.Count && value.CompareTo(currentNode.Values[i]) > 0)
                {
                    i++;
                }
                if (i < currentNode.Values.Count && value.CompareTo(currentNode.Values[i]) == 0)
                {
                    return true;
                }
                else if (currentNode.IsLeaf)
                {
                    return false;
                }
                else
                {
                    currentNode = currentNode.Children[i];
                }
            }

            return false;
        }


    }

   
}
