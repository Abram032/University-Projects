using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AOOP.Sorting.Abstractions;
using AOOP.Sorting.Models;
using AOOP.Sorting.Utils;

namespace AOOP.Sorting.Algorithms
{
    public class MergeSort<T> : ISorter<T> where T : IComparable<T>
    {
        public IList<T> Sort(IList<T> array)
        {
            if (array.Count <= 1) 
            {
                return array;
            }

            int middle = array.Count / 2;

            IList<T> leftArray = new List<T>();
            IList<T> rightArray = new List<T>();

            for (int i = 0; i < middle; i++)
            {
                leftArray.Add(array[i]);
            }
            for (int i = middle; i < array.Count; i++)
            {
                rightArray.Add(array[i]);
            }

            leftArray = Sort(leftArray);
            rightArray = Sort(rightArray);
            return Merge(leftArray, rightArray);
        }

        private IList<T> Merge(IList<T> leftArray, IList<T> rightArray)
        {
            var result = new List<T>();
            
            while (leftArray.Any() || rightArray.Any())
            {
                if (leftArray.Any() && rightArray.Any())
                {
                    if (leftArray.First().CompareTo(rightArray.First()) <= 0)
                    {
                        result.Add(leftArray.First());
                        leftArray.Remove(leftArray.First());      
                    }
                    else
                    {
                        result.Add(rightArray.First());
                        rightArray.Remove(rightArray.First());
                    }
                }
                else if (leftArray.Any())
                {
                    result.Add(leftArray.First());
                    leftArray.Remove(leftArray.First());
                }
                else if (rightArray.Any())
                {
                    result.Add(rightArray.First());
                    rightArray.Remove(rightArray.First());
                }
            }
            return result;
        }
    }
}