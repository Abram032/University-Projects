using System;
using System.Collections.Generic;
using AOP.Sorting.Models;

namespace AOP.Sorting.Utils
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
            System.Console.WriteLine($"Values:");
            System.Console.WriteLine();
        }

        public static void PrintResults<T>(IList<Result<T>> results)
        {
            foreach(var result in results)
            {
                Helpers.PrintResult<T>(result);
            }
        }

        public static bool Validate<T>(IList<T> values) where T : IComparable<T>
        {
            for(int i = 1; i < values.Count; i++)
            {
                if(values[i-1].CompareTo(values[i]) > 0)
                    return false;
            }
            return true;
        }

        public static T[] GenerateArray<T>(int length) where T : struct, IComparable<T>, IEquatable<T>, IConvertible
        {
            var array = new T[length];
            for(int i = 0; i < length; i++)
            {
                array[i] = GenerateValue<T>();
            }
            return array;
        }

        public static T GenerateValue<T>() where T : struct, IComparable<T>, IEquatable<T>, IConvertible
        {
            var rng = new Random((int)DateTime.Now.Ticks);
            var type = typeof(T);
            switch(type)
            {
                case var t when t == typeof(Byte) || t == typeof(Char):
                    return (T)Convert.ChangeType(rng.Next(Byte.MinValue, Byte.MaxValue), type);
                case var t when t == typeof(SByte):
                    return (T)Convert.ChangeType(rng.Next(SByte.MinValue, SByte.MaxValue), type);
                case var t when t == typeof(Int16):
                    return (T)Convert.ChangeType(rng.Next(Int16.MinValue, Int16.MaxValue), type);
                case var t when t == typeof(UInt16):
                    return (T)Convert.ChangeType(rng.Next(UInt16.MinValue, UInt16.MaxValue), type);
                case var t when t == typeof(Int32):
                    return (T)Convert.ChangeType(rng.Next(), type);
                case var t when t == typeof(UInt32):
                    return (T)Convert.ChangeType(rng.Next((Int32)UInt32.MinValue, Int32.MaxValue), type);
                case var t when t == typeof(Int64):
                    return (T)Convert.ChangeType(rng.Next(), type);
                case var t when t == typeof(UInt64):
                    return (T)Convert.ChangeType((UInt64)rng.Next((Int32)UInt32.MinValue, Int32.MaxValue), type);
                case var t when t == typeof(Single):
                    return (T)Convert.ChangeType(rng.NextDouble() * (Single.MaxValue - Single.MinValue) + Single.MinValue, type);
                case var t when t == typeof(Double):
                    return (T)Convert.ChangeType(rng.NextDouble() * (Double.MaxValue - Double.MinValue) + Double.MinValue, type);
                default:
                    return default(T);
            }
        } 
    }
}