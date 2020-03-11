using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AOP.Sorting.Algorithms
{
    public static class InsertionSort
    {
        public static async Task Sort<T>(IList<T> array) where T : IComparable<T>, IEquatable<T>, IConvertible
        {
            for(int i = 1; i < array.Count; i++) 
            {
                var value = array[i];
                int j = i - 1;
                
                while((j >= 0) && (array[j].CompareTo(value) > 0)) 
                {
                    array[j + 1] = array[j];
                    j -= 1;
                }
                array[j + 1] = value;
            }
        }
    }
}
