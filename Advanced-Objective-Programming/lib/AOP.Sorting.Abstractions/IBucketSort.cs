using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AOP.Sorting.Models;

namespace AOP.Sorting.Abstractions
{
    public interface IBucketSort
    {
        Task<Result<T>> Sort<T>(IList<T> values, ISorter algorithm) where T : struct, IComparable<T>, IEquatable<T>, IConvertible;
    }
}