using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AOP.Sorting.Abstractions;
using AOP.Sorting.Models;
using AOP.Sorting.Utils;

namespace AOP.Sorting.Algorithms
{
    public class QuickSort : ISorter
    {
        public async Task<Result<T>> Sort<T>(IList<T> values) where T : struct, IComparable<T>, IEquatable<T>, IConvertible 
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            QSort(values, 0, values.Count - 1);

            stopwatch.Stop();

            var validationResult = Helpers.Validate<T>(values);
            var result = new Result<T>
            {
                Succeded = validationResult,
                Errors = (validationResult) ? new List<string>() : new List<string> { "Values are not sorted." },
                TimeElapsed = stopwatch.ElapsedMilliseconds,
                TicksElapsed = stopwatch.ElapsedTicks,
                Values = values
            };

            return result;
        }

        private void QSort<T>(IList<T> array, int low, int high) where T : IComparable<T>, IEquatable<T>, IConvertible
        {
            if(low < high)
            {
                int pivot = Partiton<T>(array, low, high);
                QSort(array, low, pivot - 1);
                QSort(array, pivot + 1, high);
            }
        }

        private int Partiton<T>(IList<T> array, int low, int high) where T : IComparable<T>, IEquatable<T>, IConvertible
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