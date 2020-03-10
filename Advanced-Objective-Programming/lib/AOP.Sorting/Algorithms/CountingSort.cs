using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AOP.Sorting.Algorithms
{
    public static class CountingSort
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
            
            var counts = new int[Convert.ToInt64((Subtract(maxValue, minValue))) + 1];

            foreach(var value in array)
            {
                counts[Convert.ToInt64(Subtract(value, minValue))] += 1;
            }

            var result = new List<T>();
            for(int i = 0; i < counts.Length; i++)
            {
                for(int j = 0; j < counts[i]; j++)
                {
                    result.Add((T)Convert.ChangeType(i, typeof(T)));
                }
            }
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
    }
}