using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AOP.Sorting.Abstractions;
using AOP.Sorting.Models;
using AOP.Sorting.Utils;

namespace AOP.Sorting.Algorithms
{
    public class MergeSort : ISorter
    {
        public async Task<Result<T>> Sort<T>(IList<T> values) where T : struct, IComparable<T>, IEquatable<T>, IConvertible
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            MSort(values, 0, values.Count - 1);

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

        private void MSort<T>(IList<T> array, int left, int right) where T : IComparable<T>, IEquatable<T>, IConvertible
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
        
                MSort(array, left, middle);
                MSort(array, middle + 1, right);
        
                Merge(array, left, middle, right);
            }
        }

        private void Merge<T>(IList<T> array, int left, int middle, int right) where T : IComparable<T>, IEquatable<T>, IConvertible
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