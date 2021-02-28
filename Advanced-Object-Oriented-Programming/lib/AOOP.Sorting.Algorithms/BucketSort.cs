using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AOOP.Sorting.Abstractions;
using AOOP.Sorting.Models;
using AOOP.Sorting.Utils;

namespace AOOP.Sorting.Algorithms
{
    public class BucketSort<T> : ISorter<T>, IBucketSort<T> where T : IComparable<T>, IConvertible
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

        public void Sort(object values) 
        {
            Sort(values as IList<T>);
        }

        public IList<T> Sort(IList<T> values) 
        {
            return Sort(values, new QuickSort<T>());
        }

        public IList<T> Sort(IList<T> values, ISorter<T> algorithm)
        {
            if (values == null || algorithm == null) {
                return default;
            }

            if (!allowedTypes.Contains(typeof(T)))
            {
                throw new TypeNotAllowedException($"Type of {typeof(T)} is not allowed in this method.");
            }

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
                algorithm.Sort(bucket);
            }

            //Parallel.ForEach(buckets, async (bucket) => await algorithm.Sort(bucket));

            for(int i = 0, ai = 0; i < buckets.Length; i++)
            {
                for(int j = 0; j < buckets[i].Count; j++, ai++)
                {
                    values[ai] = buckets[i][j];
                }
            }

            return values;
        }
    }
}