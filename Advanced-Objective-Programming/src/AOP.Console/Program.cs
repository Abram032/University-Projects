using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AOP.Sorting.Algorithms;
using AOP.Sorting.Utils;

namespace AOP.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = {10, 17, 23, 3, 14, 57, 2345, -17};

            System.Console.WriteLine("Insertion sort:");
            Utils.Print<int>(array);
            InsertionSort.Sort<int>(array);
            Utils.Print<int>(array);

            array = new int[] {10, 17, 23, 3, 14, 57, 2345, -17};
            
            System.Console.WriteLine("Bubble sort:");
            Utils.Print<int>(array);
            BubbleSort.Sort<int>(array);
            Utils.Print<int>(array);

            array = new int[] {10, 17, 23, 3, 14, 57, 2345, -17};

            System.Console.WriteLine("Quick sort:");
            Utils.Print<int>(array);
            QuickSort.Sort<int>(array);
            Utils.Print<int>(array);

            array = new int[] {10, 17, 23, 3, 14, 57, 2345, -17};

            System.Console.WriteLine("Bucket sort:");
            Utils.Print<int>(array);
            BucketSort.Sort<int>(array);
            Utils.Print<int>(array);

            array = new int[] {10, 17, 23, 3, 14, 57, 2345, -17};

            System.Console.WriteLine("Counting sort:");
            Utils.Print<int>(array);
            CountingSort.Sort<int>(array);
            Utils.Print<int>(array);

            array = new int[] {10, 17, 23, 3, 14, 57, 2345, -17};

            System.Console.WriteLine("Merge sort:");
            Utils.Print<int>(array);
            MergeSort.Sort<int>(array);
            Utils.Print<int>(array);
        }
    }
}
