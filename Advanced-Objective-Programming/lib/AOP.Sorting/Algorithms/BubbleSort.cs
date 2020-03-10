using System;
using System.Collections.Generic;

namespace AOP.Sorting.Algorithms
{
    public static class BubbleSort
    {
        public static void Sort<T>(IList<T> array) where T : IComparable<T>, IEquatable<T>, IConvertible
        {
            for(int i = 0; i < array.Count - 1; i++) 
            {
                for(int j = 0; j < array.Count - i - 1; j++)
                {
                    if(array[j].CompareTo(array[j + 1]) > 0)
                    {
                        var temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }
    }
}