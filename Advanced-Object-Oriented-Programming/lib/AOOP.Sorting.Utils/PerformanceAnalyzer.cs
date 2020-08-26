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
    }
}