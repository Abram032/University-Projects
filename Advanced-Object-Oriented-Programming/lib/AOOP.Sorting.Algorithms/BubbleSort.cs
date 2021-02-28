using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AOOP.Sorting.Abstractions;
using AOOP.Sorting.Models;
using AOOP.Sorting.Utils;

namespace AOOP.Sorting.Algorithms
{
    public class BubbleSort<T> : ISorter<T> where T : IComparable<T>
    {
        public void Sort(object values) 
        {
            Sort(values as IList<T>);
        }

        public IList<T> Sort(IList<T> values)
        {
            if(values == null) {
                return default;
            }

            for (int i = 0; i < values.Count - 1; i++) 
            {
                var isChanged = false;
                for(int j = 0; j < values.Count - i - 1; j++)
                {
                    if(values[j].CompareTo(values[j + 1]) > 0)
                    {
                        var temp = values[j];
                        values[j] = values[j + 1];
                        values[j + 1] = temp;
                        isChanged = true;
                    }
                }

                if(!isChanged) 
                {
                    break;
                }
            }

            return values;
        }
    }
}