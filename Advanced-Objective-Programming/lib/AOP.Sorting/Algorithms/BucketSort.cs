using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AOP.Sorting.Algorithms
{
    public static class BucketSort
    {
        public static void Sort<T>(IList<T> array) where T : IComparable<T>, IEquatable<T>
        {
            var maxValue = array[0];
            var minValue = array[0];

            foreach(var value in array)
            {
                if(value.CompareTo(maxValue) > 0)
                    maxValue = value;
                if(value.CompareTo(minValue) < 0)
                    minValue = value;
            }
            
            var numberOfBuckets = Add(Subtract(maxValue, minValue), 1);
            var buckets = new List<T>[numberOfBuckets];

            for(int i = 0; i < numberOfBuckets; i++)
            {
                buckets[i] = new List<T>();
            }

            for (int i = 0; i < array.Count; i++)
            {
                buckets[Add(Subtract(array[i], minValue), 0)].Add(array[i]);
            }
            
            int k = 0;
            for (int i = 0; i < buckets.Length; i++)
            {
                if (buckets[i].Count > 0)
                {
                    for (int j = 0; j < buckets[i].Count; j++)
                    {
                        array[k] = buckets[i][j];
                        k++;
                    }
                }
            }
        }
        
        static T Add<T>(T a, T b) where T : IComparable<T>, IEquatable<T>
        {
            var paramA = Expression.Parameter(typeof(T), "a");
            var paramB = Expression.Parameter(typeof(T), "b");
            var body = Expression.Add(paramA, paramB);
            var func = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();
            return func(a, b);
        }

        static int Add<T>(T a, int b) where T : IComparable<T>, IEquatable<T>
        {
            var paramA = Expression.Parameter(typeof(T), "a");
            var paramB = Expression.Parameter(typeof(int), "b");
            var body = Expression.Add(paramA, paramB);
            var func = Expression.Lambda<Func<T, int, int>>(body, paramA, paramB).Compile();
            return func(a, b);
        }

        static T Subtract<T>(T a, T b) where T : IComparable<T>, IEquatable<T>
        {
            var paramA = Expression.Parameter(typeof(T), "a");
            var paramB = Expression.Parameter(typeof(T), "b");
            var body = Expression.Subtract(paramA, paramB);
            var func = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();
            return func(a, b);
        }
    }
}