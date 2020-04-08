using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AOP.Sorting.Abstractions;
using AOP.Sorting.Models;
using AOP.Sorting.Utils;

namespace AOP.Sorting.Algorithms
{
    public class InsertionSort : ISorter
    {
        public async Task<Result<T>> Sort<T>(IList<T> values) where T : struct, IComparable<T>, IEquatable<T>, IConvertible
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for(int i = 1; i < values.Count; i++) 
            {
                var value = values[i];
                int j = i - 1;
                
                while((j >= 0) && (values[j].CompareTo(value) > 0)) 
                {
                    values[j + 1] = values[j];
                    j -= 1;
                }
                values[j + 1] = value;
            }

            stopwatch.Stop();

            var validationResult = Helpers.Validate<T>(values);
            var result = new Result<T>
            {
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
