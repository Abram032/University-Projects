using System;
using System.Collections.Generic;

namespace AOP.Sorting.Utils
{
    public static class Utils
    {
        public static void Print<T>(IEnumerable<T> array)
        {
            foreach(var value in array) 
            {
                Console.Write($"{value} ");
            }
            Console.WriteLine();
        }
    }
}