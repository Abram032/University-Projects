using System;
using System.Collections.Generic;

namespace AOP.Sorting.Utils
{
    public static class Helpers
    {
        public static void Print<T>(IEnumerable<T> values)
        {
            foreach(var value in values) 
            {
                Console.Write($"{value} ");
            }
            Console.WriteLine();
        }

        public static bool Validate<T>(IList<T> values) where T : IComparable<T>
        {
            for(int i = 1; i < values.Count; i++)
            {
                if(values[i-1].CompareTo(values[i]) > 0)
                    return false;
            }
            return true;
        }

        public static T[] GenerateArray<T>(int length) where T : struct, IComparable<T>, IEquatable<T>, IConvertible
        {
            var array = new T[length];
            for(int i = 0; i < length; i++)
            {
                array[i] = GenerateValue<T>();
            }
            return array;
        }

        public static T GenerateValue<T>() where T : struct, IComparable<T>, IEquatable<T>, IConvertible
        {
            var rng = new Random((int)DateTime.Now.Ticks);
            var type = typeof(T);
            return (T)Convert.ChangeType(rng.NextDouble() * (double.MaxValue - double.MinValue) + double.MinValue, type);
        } 
    }
}