using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AOP.Sorting.Algorithms
{
    public static class BucketSort
    {
        public static void Sort<T>(IList<T> array) where T : IComparable<T>, IEquatable<T>, IConvertible
        {
            var minValue = array.First();
            var maxValue = array.First();

            foreach(var value in array)
            {
                if (value.CompareTo(maxValue) > 0)
                    maxValue = value;
                if (value.CompareTo(minValue) < 0)
                    minValue = value;
            }

            var buckets = new List<T>[Convert.ToInt64((Subtract(maxValue, minValue))) + 1];

            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new List<T>();
            }

            for (int i = 0; i < array.Count; i++)
            {
                buckets[Convert.ToInt64(Subtract(array[i], minValue))].Add(array[i]);
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
        
        private static U Divide<T, U>(T a, U b) 
            where T : IComparable<T>, IEquatable<T>
            where U : IComparable<U>, IEquatable<U>
        {
            var paramA = Expression.Parameter(typeof(T), "a");
            var paramB = Expression.Parameter(typeof(U), "b");
            var body = Expression.Divide(paramA, paramB);
            var func = Expression.Lambda<Func<T, U, U>>(body, paramA, paramB).Compile();
            return func(a, b);
        }

        private static T Subtract<T>(T a, T b)
            where T : IComparable<T>, IEquatable<T>
        {
            var paramA = Expression.Parameter(typeof(T), "a");
            var paramB = Expression.Parameter(typeof(T), "b");
            var body = Expression.Subtract(paramA, paramB);
            var func = Expression.Lambda<Func<T, T, T>>(body, paramA, paramB).Compile();
            return func(a, b);
        }

        private static U Add<T, U>(T a, U b)
            where T : IComparable<T>, IEquatable<T>
            where U : IComparable<U>, IEquatable<U>
        {
            var paramA = Expression.Parameter(typeof(T), "a");
            var paramB = Expression.Parameter(typeof(U), "b");
            var body = Expression.Add(paramA, paramB);
            var func = Expression.Lambda<Func<T, U, U>>(body, paramA, paramB).Compile();
            return func(a, b);
        }
    }
}