using System;
using System.Collections.Generic;
using System.Linq;
using OM.Models;

namespace OM.Utils
{
    public static class MatrixExtensions
    {
        public static T[] GetRow<T>(this T[,] matrix, int row)
        {
            var n = matrix.GetLength(0);
            var result = new T[n];
            for(int i = 0; i < n; i++)
            {
                result[i] = matrix[row, i];
            }
            return result;
        }

        public static T[] GetColumn<T>(this T[,] matrix, int column)
        {
            var m = matrix.GetLength(1);
            var result = new T[m];
            for(int i = 0; i < m; i++)
            {
                result[i] = matrix[i, column];
            }
            return result;
        }

        public static int Count<T>(this T[,] matrix, Func<T, bool> predicate)
        {
            var n = matrix.GetLength(0);
            var m = matrix.GetLength(1);
            var counter = 0;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(predicate(matrix[i,j])) 
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        public static void Print<T>(this T[,] matrix)
        {
            var n = matrix.GetLength(0);
            var m = matrix.GetLength(1);
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    System.Console.Write($"{matrix[i,j]}\t");
                }
                System.Console.WriteLine();
            }
        }
    }
}