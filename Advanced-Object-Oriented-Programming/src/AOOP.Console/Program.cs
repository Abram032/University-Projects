using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AOOP.Sorting.Abstractions;
using AOOP.Sorting.Algorithms;
using AOOP.Sorting.Models;
using AOOP.Sorting.Utils;
using System.Linq;
using System.Threading;

namespace AOOP.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var values = Helpers.GenerateArray<int>(0, 250, 100);

            System.Console.WriteLine("Starting algorithms...");

            var algorithms = new List<ISorter<int>>
            {
                new BubbleSort<int>(),
                new BucketSort<int>(),
                new CountingSort<int>(),
                new InsertionSort<int>(),
                new MergeSort<int>(),
                new QuickSort<int>()
            };

            var comparer = new AlgorithmComparer<int>();
            comparer.PrepareThreads(algorithms, values);
            Helpers.PrintResultValues(comparer.Results);
            comparer.Start();
            comparer.Join();
            Helpers.PrintResultValues(comparer.Results);
        }
    }
}
