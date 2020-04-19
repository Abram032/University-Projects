using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AOP.Sorting.Models;

namespace AOP.Sorting.Abstractions
{
    public interface ISorter
    {
        Task<Result<T>> Sort<T>(IList<T> values) where T : struct, IComparable<T>, IEquatable<T>, IConvertible;
    }
}