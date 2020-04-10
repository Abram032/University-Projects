using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AOP.Sorting.Abstractions;
using AOP.Sorting.Algorithms;
using AOP.Sorting.Models;
using AOP.Sorting.Utils;

namespace AOP.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            byte[] array = Helpers.GenerateArray<byte>(100000);
            System.Console.WriteLine("Generated array:");
            //Helpers.Print(array);
            System.Console.WriteLine();

            List<ISorter> algorithms = new List<ISorter>
            {
                new BubbleSort(),
                new BucketSort(),
                new CountingSort(),
                new InsertionSort(),
                new MergeSort(),
                new QuickSort()
            };

            System.Console.WriteLine("Running algorithms, one after another: ");
            System.Console.WriteLine();

            var stopwatch = new Stopwatch();
            var results = new List<Result<byte>>();

            stopwatch.Start();

            foreach(var algorithm in algorithms)
            {
                IList<byte> clone = array.Clone() as IList<byte>;             
                try 
                {
                    results.Add(await algorithm.Sort(clone));       
                }
                catch(Exception e)
                {
                    results.Add(new Result<byte> {
                        Algorithm = algorithm.GetType().Name,
                        Succeded = false,
                        Errors = new List<string> { e.Message },
                        Values = null,
                        TicksElapsed = 0,
                        TimeElapsed = 0
                    });
                }            
            }

            stopwatch.Stop();

            Helpers.PrintResults<byte>(results);

            System.Console.WriteLine($"Total time: {stopwatch.ElapsedMilliseconds} ms");
            System.Console.WriteLine($"Total ticks: {stopwatch.ElapsedTicks}");
            System.Console.WriteLine();

            // stopwatch.Restart();

            // System.Console.WriteLine("Running algorithms, in parallel: ");
            // System.Console.WriteLine();
            
            // stopwatch.Start();
            // Parallel.ForEach(algorithms, async (algorithm) => {
            //     IList<byte> clone = array.Clone() as IList<byte>;             
            //     try 
            //     {
            //         results.Add(await algorithm.Sort(clone));       
            //     }
            //     catch(Exception e)
            //     {
            //         results.Add(new Result<byte> {
            //             Algorithm = algorithm.GetType().Name,
            //             Succeded = false,
            //             Errors = new List<string> { e.Message },
            //             Values = null,
            //             TicksElapsed = 0,
            //             TimeElapsed = 0
            //         });
            //     }
            // });
            // stopwatch.Stop();

            // Helpers.PrintResults<byte>(results);

            // System.Console.WriteLine($"Total time: {stopwatch.ElapsedMilliseconds} ms");
            // System.Console.WriteLine($"Total ticks: {stopwatch.ElapsedTicks}");
            // System.Console.WriteLine();
        }
    }
}
