using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AOP.Sorting.Abstractions;
using AOP.Sorting.Algorithms;
using AOP.Sorting.Models;
using AOP.Sorting.Utils;
using System.Linq;

namespace AOP.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = Helpers.GenerateArray<int>(0, 250, 10000);

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

            var analyzer = new PerformanceAnalyzer<int>();
            var results = analyzer.Measure(algorithms, array);
            Helpers.PrintResults<int>(results);
        }
    }
}
