using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AOP.Sorting.Abstractions;
using AOP.Sorting.Models;
using AOP.Sorting.Utils;

namespace AOP.Sorting.Algorithms
{
    public class BubbleSort : ISorter
    {
        public async Task<Result<T>> Sort<T>(IList<T> values) where T : struct, IComparable<T>, IEquatable<T>, IConvertible
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();
            for(int i = 0; i < values.Count - 1; i++) 
            {
                for(int j = 0; j < values.Count - i - 1; j++)
                {
                    if(values[j].CompareTo(values[j + 1]) > 0)
                    {
                        var temp = values[j];
                        values[j] = values[j + 1];
                        values[j + 1] = temp;
                    }
                }
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