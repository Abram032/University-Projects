using AOOP.Sorting.Abstractions;
using AOOP.Sorting.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AOOP.Sorting.Utils 
{
    public class AlgorithmComparer<T> where T : IComparable<T>
    {
        public IList<Result<T>> Results { get; set; }
        public IList<Thread> Threads { get; set; }

        public void PrepareThreads(IEnumerable<ISorter<T>> algorithms, IList<T> values)
        {
            Results = new List<Result<T>>();
            Threads = new List<Thread>();
            var array = values as T[];

            foreach (var algorithm in algorithms)
            {
                var result = new Result<T>
                {
                    Algorithm = algorithm.GetType().Name,
                    Values = array.Clone() as IList<T>
                };

                var thread = new Thread(algorithm.Sort);
                thread.Name = result.Algorithm;

                Results.Add(result);
                Threads.Add(thread);

                thread.Start(result.Values);
                thread.Suspend();
            }
        }

        public void Stop()
        {
            foreach (var thread in Threads)
            {
                if (thread.ThreadState == System.Threading.ThreadState.Running)
                {
                    thread.Suspend();
                }
            }
        }

        public void Start()
        {
            foreach (var thread in Threads)
            {
                if (thread.ThreadState == System.Threading.ThreadState.Suspended)
                {
                    thread.Resume();
                }
            }
        }

        public void Join()
        {
            foreach (var thread in Threads)
            {
                thread.Join();
            }
        }
    }
}
