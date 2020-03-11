using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AOP.Sorting.Algorithms
{
    public static class BucketSort
    {
        public static async Task Sort<T>(IList<T> array) where T : IComparable<T>, IEquatable<T>, IConvertible
        {
            const int k = 10;
            var minValue = array.First();
            var maxValue = array.First();

            foreach(var value in array)
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

            for (int i = 0; i < array.Count; i++)
            {
                buckets[((k - 1) * Convert.ToInt64(array[i]) / Convert.ToInt64(maxValue))].Add(array[i]);
            }
            
            // foreach(var bucket in buckets)
            // {
            //     await InsertionSort.Sort(bucket);
            // }

            Parallel.ForEach(buckets, async (bucket) => await InsertionSort.Sort(bucket));

            for(int i = 0, ai = 0; i < buckets.Length; i++)
            {
                for(int j = 0; j < buckets[i].Count; j++, ai++)
                {
                    array[ai] = buckets[i][j];
                }
            }
        }
    }
}