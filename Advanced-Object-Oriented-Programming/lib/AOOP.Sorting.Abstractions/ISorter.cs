using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AOOP.Sorting.Models;

namespace AOOP.Sorting.Abstractions
{
    public interface ISorter<T> where T : IComparable<T>
    {
        IList<T> Values { get; set; }
        State State { get; }
        //void Sort();
        void Sort(object values);
        IList<T> Sort(IList<T> values);
    }
}