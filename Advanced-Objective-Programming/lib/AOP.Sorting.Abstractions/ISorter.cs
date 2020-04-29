using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AOP.Sorting.Models;

namespace AOP.Sorting.Abstractions
{
    public interface ISorter<T> where T : IComparable<T>
    {
        IList<T> Sort(IList<T> values);
    }
}