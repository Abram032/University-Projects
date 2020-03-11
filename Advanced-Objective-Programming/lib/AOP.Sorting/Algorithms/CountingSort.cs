using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AOP.Sorting.Abstractions;

namespace AOP.Sorting.Algorithms
{
    public static class CountingSort
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

        public static void Sort<T>(IList<T> array) where T : IComparable<T>, IEquatable<T>, IConvertible
        {
            if(!allowedTypes.Contains(typeof(T)))
            {
                throw new TypeNotAllowedException($"Type of {typeof(T)} is not allowed in this method.");
            }

            var minValue = array.First();
            var maxValue = array.First();

            foreach(var value in array)
            {
                if (value.CompareTo(maxValue) > 0)
                    maxValue = value;
                if (value.CompareTo(minValue) < 0)
                    minValue = value;
            }
            
            var counts = new int[Convert.ToInt64(maxValue) - Convert.ToInt64(minValue) + 1];

            foreach(var value in array)
            {
                counts[Convert.ToInt64(value) - Convert.ToInt64(minValue)] += 1;
            }

            var result = new List<T>();
            for(int i = 0; i < counts.Length; i++)
            {
                for(int j = 0; j < counts[i]; j++)
                {
                    result.Add((T)Convert.ChangeType(i + Convert.ToInt64(minValue), typeof(T)));
                }
            }

            for(int i = 0; i < array.Count; i++)
            {
                array[i] = result[i];
            }
        }
    }
}