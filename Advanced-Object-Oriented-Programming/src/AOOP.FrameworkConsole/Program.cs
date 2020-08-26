using AOOP.Sorting.Abstractions;
using AOOP.Sorting.Algorithms;
using AOOP.Sorting.Models;
using AOOP.Sorting.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AOOP.FrameworkConsole 
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

            var analyzer = new PerformanceAnalyzer<int>();
            var threads = analyzer.StartThreads(algorithms, values, out var results);
            analyzer.StopThreads(threads);
            //Helpers.PrintResultValues(results);
            analyzer.ResumeThrads(threads);
            analyzer.JoinThreads(threads);
            //Helpers.PrintResultValues(results);
        }
    }
}
