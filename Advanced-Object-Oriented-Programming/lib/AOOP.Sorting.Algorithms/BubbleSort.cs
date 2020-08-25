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
        public IList<T> Values { get; set; }
        public State State { get; set; }

        public BubbleSort() { State = State.Created; }
        public BubbleSort(IList<T> values)     
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

            State = State.Finished;

            return values;
        }
    }
}