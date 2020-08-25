using AOOP.Sorting.Abstractions;
using AOOP.Sorting.Algorithms;
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
            var values = Helpers.GenerateArray<int>(0, 250, 1000);

            System.Console.WriteLine("Starting algorithms...");

            var algorithms = new List<ISorter<int>>
            {
                new BubbleSort<int>(values.Clone() as IList<int>),
                new BucketSort<int>(values.Clone() as IList<int>),
                new CountingSort<int>(values.Clone() as IList<int>),
                new InsertionSort<int>(values.Clone() as IList<int>),
                new MergeSort<int>(values.Clone() as IList<int>),
                new QuickSort<int>(values.Clone() as IList<int>)
            };

            var analyzer = new PerformanceAnalyzer<int>();

            var threads = analyzer.StartThreads(algorithms);
            //Helpers.Print(algorithms);
            analyzer.StopThreads(threads);
            Helpers.Print(algorithms);
            analyzer.ResumeThrads(threads);
            Thread.Sleep(2);
            analyzer.StopThreads(threads);
            Helpers.Print(algorithms);
            analyzer.ResumeThrads(threads);
            analyzer.JoinThreads(threads);
            Helpers.Print(algorithms);

            //var results = analyzer.Measure(algorithms, array);
            //Helpers.PrintResults<int>(results);
        }
    }
}
