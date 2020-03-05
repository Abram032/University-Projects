using System;
using AOP.Sorting.Algorithms;
using AOP.Sorting.Utils;

namespace AOP.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = {10, 17, 23, 3, 14, 57};

            System.Console.WriteLine("Insertion sort:");
            Utils.Print<int>(array);
            InsertionSort.Sort<int>(array);
            Utils.Print<int>(array);

            array = new int[] {10, 17, 23, 3, 14, 57};
            
            System.Console.WriteLine("Bubble sort:");
            Utils.Print<int>(array);
            BubbleSort.Sort<int>(array);
            Utils.Print<int>(array);

            array = new int[] {10, 17, 23, 3, 14, 57};

            System.Console.WriteLine("Quick sort:");
            Utils.Print<int>(array);
            QuickSort.Sort<int>(array);
            Utils.Print<int>(array);
        }
    }
}
