using System;
using System.Diagnostics;
using Сбалансированные_деревья;

class Program
{
    static void Main(string[] args)
    {
        const int size = 10000;
        int[] data = new int[size];

        // заполнение массива случайными числами
        Random rand = new Random();
        for (int i = 0; i < size; i++)
        {
            data[i] = rand.Next(100000);
        }

        // создание трех деревьев и вставка элементов
        AVLTree<int> avlTree = new AVLTree<int>();
        BTree<int> bTree = new BTree<int>(9999);
        RBTree<int> rbTree = new RBTree<int>();
        foreach (int value in data)
        {
            avlTree.Insert(value);
            bTree.Insert(value);
            rbTree.Insert(value);
        }

        // измерение времени поиска в AVL-дереве
        Stopwatch avlWatch = new Stopwatch();
        avlWatch.Start();
        foreach (int value in data)
        {
            avlTree.Search(value);
        }
        avlWatch.Stop();

        // измерение времени поиска в B-дереве
        Stopwatch bWatch = new Stopwatch();
        bWatch.Start();
        foreach (int value in data)
        {
            bTree.Search(value);
        }
        bWatch.Stop();

        // измерение времени поиска в Красно-черном дереве
        Stopwatch rbWatch = new Stopwatch();
        rbWatch.Start();
        foreach (int value in data)
        {
            rbTree.Search(value);
        }
        rbWatch.Stop();

        // вывод результатов
        Console.WriteLine($"AVL-дерево: {avlWatch.ElapsedMilliseconds} мс");
        Console.WriteLine($"B-дерево: {bWatch.ElapsedMilliseconds} мс");
        Console.WriteLine($"Красно-черное дерево: {rbWatch.ElapsedMilliseconds} мс");
    }
}
