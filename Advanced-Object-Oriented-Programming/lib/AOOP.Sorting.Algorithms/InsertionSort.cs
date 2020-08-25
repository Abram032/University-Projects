using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AOOP.Sorting.Abstractions;
using AOOP.Sorting.Models;
using AOOP.Sorting.Utils;

namespace AOOP.Sorting.Algorithms
{
    public class InsertionSort<T> : ISorter<T> where T : IComparable<T>
    {
        public IList<T> Values { get; set; }
        public State State { get; set; }
        public InsertionSort() { State = State.Created; }
        public InsertionSort(IList<T> values) 
        {
            State = State.Created;
            Values = values;
        }

        public void Sort()
        {
            Values = Sort(Values);
        }

        public void Sort(object values) 
        {
            Values = values as IList<T>;
            Sort();
        }

        public IList<T> Sort(IList<T> values) 
        {
            State = State.Running;

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

            State = State.Finished;

            return values;
        }
    }
}
