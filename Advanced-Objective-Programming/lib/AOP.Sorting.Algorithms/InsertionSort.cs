using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AOP.Sorting.Abstractions;
using AOP.Sorting.Models;
using AOP.Sorting.Utils;

namespace AOP.Sorting.Algorithms
{
    public class InsertionSort<T> : ISorter<T> where T : IComparable<T>
    {
        public IList<T> Sort(IList<T> values)
        {
            for(int i = 1; i < values.Count; i++) 
            {
                var value = values[i];
                int j = i - 1;
                
                while((j >= 0) && (values[j].CompareTo(value) > 0)) 
                {
                    values[j + 1] = values[j];
                    j -= 1;
                }
                values[j + 1] = value;
            }

            return values;
        }
    }
}
