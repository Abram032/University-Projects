using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AOP.Sorting.Abstractions;
using AOP.Sorting.Models;
using AOP.Sorting.Utils;

namespace AOP.Sorting.Algorithms
{
    public class BucketSort : ISorter, IBucketSort
    {
        #region AllowedTypes
        private static readonly Type[] allowedTypes = 
            {
                typeof(Byte),
                typeof(Int16), 
                typeof(Int32), 
                typeof(Int64),
                typeof(SByte),
                typeof(UInt16), 
                typeof(UInt32), 
                typeof(UInt64),
                typeof(Char)
            };
        #endregion

        public async Task<Result<T>> Sort<T>(IList<T> values) where T : struct, IComparable<T>, IEquatable<T>, IConvertible
        {
            return await Sort(values, new QuickSort());
        }

        public async Task<Result<T>> Sort<T>(IList<T> values, ISorter algorithm) where T : struct, IComparable<T>, IEquatable<T>, IConvertible
        {
            if(!allowedTypes.Contains(typeof(T)))
            {
                throw new TypeNotAllowedException($"Type of {typeof(T)} is not allowed in this method.");
            }

            var stopwatch = new Stopwatch();

            stopwatch.Start();
            
            const int k = 10;
            var minValue = values.First();
            var maxValue = values.First();

            foreach(var value in values)
            {
                if (value.CompareTo(maxValue) > 0)
                    maxValue = value;
                if (value.CompareTo(minValue) < 0)
                    minValue = value;
            }

            var buckets = new List<T>[k];

            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<T>();
            }

            for (int i = 0; i < values.Count; i++)
            {
                buckets[((k - 1) * Convert.ToInt64(values[i]) / Convert.ToInt64(maxValue))].Add(values[i]);
            }
            
            foreach(var bucket in buckets)
            {
                await algorithm.Sort(bucket);
            }

            //Parallel.ForEach(buckets, async (bucket) => await algorithm.Sort(bucket));

            for(int i = 0, ai = 0; i < buckets.Length; i++)
            {
                for(int j = 0; j < buckets[i].Count; j++, ai++)
                {
                    values[ai] = buckets[i][j];
                }
            }

            stopwatch.Stop();

            var validationResult = Helpers.Validate<T>(values);
            var result = new Result<T>
            {
                Algorithm = this.GetType().Name,
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