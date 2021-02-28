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
    public class CountingSort<T> : ISorter<T> where T : IComparable<T>, IConvertible
    {
        #region AllowedTypes
        private static readonly Type[] allowedTypes = 
            {
                typeof(Byte),
                typeof(Int16), 
                typeof(Int32), 
                typeof(Int64),
                typeof(SByte),
                typeof(UInt16), 
                typeof(UInt32), 
                typeof(UInt64),
                typeof(Char)
            };
        #endregion

        public void Sort(object values) 
        {
            Sort(values as IList<T>);
        }

        public IList<T> Sort(IList<T> values) 
        {
            if (values == null) {
                return default;
            }

            if (!allowedTypes.Contains(typeof(T)))
            {
                throw new TypeNotAllowedException($"Type of {typeof(T)} is not allowed in this method.");
            }

            var minValue = values.First();
            var maxValue = values.First();

            foreach(var value in values)
            {
                if (value.CompareTo(maxValue) > 0)
                    maxValue = value;
                if (value.CompareTo(minValue) < 0)
                    minValue = value;
            }
            
            var counts = new int[Convert.ToInt64(maxValue) - Convert.ToInt64(minValue) + 1];

            foreach(var value in values)
            {
                counts[Convert.ToInt64(value) - Convert.ToInt64(minValue)] += 1;
            }

            for(int i = 0, ai = 0; i < counts.Length; i++)
            {
                for(int j = 0; j < counts[i]; j++, ai++)
                {
                    values[ai] = (T)Convert.ChangeType(i + Convert.ToInt64(minValue), typeof(T));
                }
            }

            return values;
        }
    }
}