using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AOP.Sorting.Abstractions;
using AOP.Sorting.Algorithms;
using AOP.Sorting.Utils;

namespace AOP.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //int[] array = {10, 17, 23, 3, 14, 57, 2345, -17};
            int[] array = Helpers.GenerateArray<int>(100);
            System.Console.WriteLine("Generated array:");
            Helpers.Print(array);

            List<ISorter> algorithms = new List<ISorter>
            {
                new BubbleSort(),
                new BucketSort(),
                new CountingSort(),
                new InsertionSort(),
                new MergeSort(),
                new QuickSort()
            };

            foreach(var algorithm in algorithms)
            {
                IList<int> clone = array.Clone() as IList<int>;
                var result = await algorithm.Sort(clone);
                System.Console.WriteLine($"{algorithm.GetType().Name}:");
                System.Console.WriteLine($"Succeeded: {result.Succeded}");
                System.Console.WriteLine($"Time elapsed: {result.TimeElapsed} ms");
                System.Console.WriteLine($"Ticks elapsed: {result.TicksElapsed}");
                if(!result.Succeded)
                {   
                    System.Console.WriteLine($"Errors:");
                    foreach(var error in result.Errors)
                    {
                        System.Console.WriteLine($"\t{error}");
                    }
                }
                System.Console.WriteLine($"Values:");
                Helpers.Print(result.Values);              
                System.Console.WriteLine();
            }
        }
    }
}
