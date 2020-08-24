using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AOOP.Sorting.Abstractions;
using AOOP.Sorting.Models;
using AOOP.Sorting.Utils;

namespace AOOP.Sorting.Algorithms
{
    public class QuickSort<T> : ISorter<T> where T : IComparable<T>
    {
        public IList<T> Sort(IList<T> values)
        {
            QSort(values, 0, values.Count - 1);
            
            return values;
        }

        private void QSort(IList<T> array, int low, int high)
        {
            if(low < high)
            {
                int pivot = Partiton(array, low, high);
                QSort(array, low, pivot - 1);
                QSort(array, pivot + 1, high);
            }
        }

        private int Partiton(IList<T> array, int low, int high)
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