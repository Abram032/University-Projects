using System;
using System.Collections.Generic;

namespace AOP.Sorting.Algorithms
{
    public static class QuickSort
    {
        public static void Sort<T>(IList<T> array) where T : IComparable<T>, IEquatable<T>, IConvertible => QSort(array, 0, array.Count - 1);

        private static void QSort<T>(IList<T> array, int low, int high) where T : IComparable<T>, IEquatable<T>, IConvertible
        {
            if(low < high)
            {
                int pivot = Partiton<T>(array, low, high);
                QSort(array, low, pivot - 1);
                QSort(array, pivot + 1, high);
            }
        }

        private static int Partiton<T>(IList<T> array, int low, int high) where T : IComparable<T>, IEquatable<T>, IConvertible
        {
            var pivot = array[high];
            int i = low - 1;
            for(int j = low; j < high; j++)
            {
                if(array[j].CompareTo(pivot) < 0)
                {
                    i++;
                    var _temp = array[i];
                    array[i] = array[j];
                    array[j] = _temp;
                }
            }
            var temp = array[i+1];
            array[i+1] = array[high];
            array[high] = temp;

            return i+1;
        }
    }
}