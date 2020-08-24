using System;
using System.Collections.Generic;

namespace AOOP.Sorting.Models
{
    public class Result<T>
    {
        public string Algorithm { get; set; }
        public bool Succeded { get; set; }
        public IList<string> Errors { get; set; }
        public long TimeElapsed { get; set; }
        public long TicksElapsed { get; set; }
        public IList<T> Values { get; set; }
    }
}
