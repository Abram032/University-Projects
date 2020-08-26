using System;
using System.Collections.Generic;
using AOOP.Sorting.Models;
using AOOP.Sorting.Abstractions;
using System.Diagnostics;
using System.Threading;
using System.Text;

namespace AOOP.Sorting.Utils
{
    public class PerformanceAnalyzer<T> where T : IComparable<T>
    {
        public IEnumerable<Result<T>> Measure(IEnumerable<ISorter<T>> algorithms, IList<T> values)
        {
            var array = values as T[];
            var results = new List<Result<T>>();
            foreach(var algorithm in algorithms)
            {
                results.Add(Measure(algorithm, array.Clone() as IList<T>));
            }

            return results;
        }

        public Result<T> Measure(ISorter<T> algorithm, IList<T> values)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            values = algorithm.Sort(values);
            
            stopwatch.Stop();

            var validationResult = Helpers.Validate<T>(values);
            var result = new Result<T>
            {
                Algorithm = algorithm.GetType().Name,
                Succeded = validationResult,
                Errors = (validationResult) ? new List<string>() : new List<string> { "Values are not sorted." },
                TimeElapsed = stopwatch.ElapsedMilliseconds,
                TicksElapsed = stopwatch.ElapsedTicks,
                Values = values
            };

            return result;
        }

        public IList<Thread> StartThreads(IEnumerable<ISorter<T>> algorithms, IList<T> values, out IList<Result<T>> results) 
        {        
            results = new List<Result<T>>();
            var array = values as T[];
            var threads = new List<Thread>();

            foreach(var algorithm in algorithms) 
            {
                var result = new Result<T> 
                {
                    Algorithm = algorithm.GetType().Name,
                    Values = array.Clone() as IList<T>
                };

                var thread = new Thread(algorithm.Sort);
                thread.Name = result.Algorithm;

                results.Add(result);
                threads.Add(thread);

                thread.Start(result.Values);
            }

            return threads;
        }

        public void StopThreads(IEnumerable<Thread> threads) 
        {
            foreach(var thread in threads) 
            {
                if(thread.ThreadState == System.Threading.ThreadState.Running) 
                {
                    thread.Suspend();
                }
            }
        }

        public void ResumeThrads(IEnumerable<Thread> threads) 
        {
            foreach(var thread in threads) 
            {
                if (thread.ThreadState == System.Threading.ThreadState.Suspended) 
                {
                    thread.Resume();
                }
            }
        }

        public void JoinThreads(IEnumerable<Thread> threads) 
        {
            foreach(var thread in threads) 
            {
                thread.Join();
            }
        }
    }
}