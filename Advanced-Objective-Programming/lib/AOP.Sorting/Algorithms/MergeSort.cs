using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AOP.Sorting.Abstractions;

namespace AOP.Sorting.Algorithms
{
    public static class MergeSort
    {
        public static void Sort<T>(IList<T> array) where T : IComparable<T>, IEquatable<T>, IConvertible
            => MSort(array, 0, array.Count - 1);

        private static void MSort<T>(IList<T> array, int left, int right) where T : IComparable<T>, IEquatable<T>, IConvertible
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
        
                MSort(array, left, middle);
                MSort(array, middle + 1, right);
        
                Merge(array, left, middle, right);
            }
        }

        private static void Merge<T>(IList<T> array, int left, int middle, int right) where T : IComparable<T>, IEquatable<T>, IConvertible
        {
            var leftArray = new T[middle - left + 1];
            var rightArray = new T[right - middle];
        
            Array.Copy(array.ToArray(), left, leftArray, 0, middle - left + 1);
            Array.Copy(array.ToArray(), middle + 1, rightArray, 0, right - middle);
        
            int i = 0;
            int j = 0;
            for (int k = left; k < right + 1; k++)
            {
                if (i == leftArray.Length)
                {
                    array[k] = rightArray[j];
                    j++;
                }
                else if (j == rightArray.Length)
                {
                    array[k] = leftArray[i];
                    i++;
                }
                else if (leftArray[i].CompareTo(rightArray[j]) <= 0)
                {
                    array[k] = leftArray[i];
                    i++;
                }
                else
                {
                    array[k] = rightArray[j];
                    j++;
                }
            }
        }
    }
}