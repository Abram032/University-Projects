using System;
using System.Collections.Generic;
using System.Text;
using AOOP.Sorting.Abstractions;
using AOOP.Sorting.Models;

namespace AOOP.Sorting.Utils
{
    public static class Helpers
    {
        public static void Print<T>(IEnumerable<T> values)
        {
            foreach(var value in values) 
            {
                Console.Write($"{value} ");
            }
            Console.WriteLine();
        }

        public static void PrintResultValues<T>(IEnumerable<Result<T>> results) 
        {    
            foreach(var result in results) 
            {
                PrintResultValues(result);
            }
        }

        public static void PrintResultValues<T>(Result<T> result) 
        {
            var sb = new StringBuilder();
            foreach (var value in result.Values) {
                sb.Append($"{value},");
            }
            Console.WriteLine($"{result.Algorithm}: {sb.ToString()}");
        }

        public static void PrintResult<T>(Result<T> result)
        {
            System.Console.WriteLine($"{result.Algorithm}:");
            System.Console.WriteLine($"Succeeded: {result.Succeded}");
            System.Console.WriteLine($"Time elapsed: {result.TimeElapsed} ms");
            System.Console.WriteLine($"Ticks elapsed: {result.TicksElapsed}");
            if(!result.Succeded)
            {   
                System.Console.WriteLine($"Errors:");
                foreach(var error in result.Errors)
                {
                    System.Console.WriteLine($"\t{error}");
                }
            }
            System.Console.WriteLine();
        }

        public static void PrintResults<T>(IEnumerable<Result<T>> results)
        {
            foreach(var result in results)
            {
                Helpers.PrintResult<T>(result);
            }
        }

        public static bool Validate<T>(IList<T> values) where T : IComparable<T>
        {
            if(values == null)
            {
                throw new ArgumentNullException();
            }

            for(int i = 1; i < values.Count; i++)
            {
                if(values[i-1].CompareTo(values[i]) > 0)
                    return false;
            }
            return true;
        }

        public static T[] GenerateArray<T>(double min, double max, int length) where T : IConvertible
        {
            var rng = new Random((int)DateTime.Now.Ticks);
            var array = new T[length];
            for(int i = 0; i < length; i++)
            {
                array[i] = GenerateValue<T>(rng, min, max);
            }
            return array;
        }

        public static T GenerateValue<T>(Random rng, double min, double max) where T : IConvertible
            => (T)Convert.ChangeType((rng.NextDouble() * (max - min) + min), typeof(T));
    }
}