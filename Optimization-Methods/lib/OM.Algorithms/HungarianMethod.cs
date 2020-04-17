using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OM.Models;
using OM.Utils;

namespace OM.Algorithms
{
    public class HungarianMethod
    {
        public int[,] Resolve(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            //Subtract minimum from each row
            for(int i = 0; i < n; i++) 
            {
                var min = matrix.GetRow(i).Min();
                for(int j = 0; j < n; j++)
                {
                    matrix[i,j] -= min;
                }
            }
            //Subtract minimum from each column
            for(int i = 0; i < m; i++)
            {
                var min = matrix.GetColumn(i).Min();
                for(int j = 0; j < m; j++)
                {
                    matrix[j,i] -= min;
                }
            }

            var result = new int[n,m];
            var rowsCovered = new bool[n];
            var columnsCovered = new bool[m];
            while(!IsResolved(result))
            {
                Array.Clear(result, 0, result.Length);
                Array.Clear(rowsCovered, 0, rowsCovered.Length);
                Array.Clear(columnsCovered, 0, columnsCovered.Length);

                //Finding independent zeros
                for(int i = 0; i < n; i++)
                {
                    for(int j = 0; j < m; j++)
                    {
                        if(matrix[i,j] == 0 && !rowsCovered[i] && !columnsCovered[j])
                        {
                            result[i, j] = 1;
                            rowsCovered[i] = true;
                            columnsCovered[j] = true;
                        }
                    }
                }

                //Reducing cover lines to minimum
                var zeros = matrix.Count(v => v == 0);
                var independentZeros = result.Count(v => v == 1);
                var linesCount = rowsCovered.Count(v => v == true) + columnsCovered.Count(v => v == true);
                for(int i = 0; i < n; i++)
                {
                    if(rowsCovered[i] == true)
                    {
                        rowsCovered[i] = false;
                        var coveredZeros = CountCoveredZeros(matrix, rowsCovered, columnsCovered);
                        if(zeros != coveredZeros)
                        {
                            rowsCovered[i] = true;
                        }
                    }

                    if(columnsCovered[i] == true)
                    {
                        columnsCovered[i] = false;
                        var coveredZeros = CountCoveredZeros(matrix, rowsCovered, columnsCovered);
                        if(zeros != coveredZeros)
                        {
                            columnsCovered[i] = true;
                        }
                    }
                }
                linesCount = rowsCovered.Count(v => v == true) + columnsCovered.Count(v => v == true);

                if(IsResolved(result))
                {
                    break;
                }

                //Finding minimum not covered by lines
                var minimum = int.MaxValue;
                for(int i = 0; i < n; i++)
                {
                    for(int j = 0; j < m; j++)
                    {
                        if((rowsCovered[i] || columnsCovered[j]) == false && matrix[i,j] < minimum)
                        {
                            minimum = matrix[i,j];
                        }
                    }
                }

                //Adding to double covered and subtracting from not covered
                for(int i = 0; i < n; i++)
                {
                    for(int j = 0; j < m; j++)
                    {
                        if((rowsCovered[i] || columnsCovered[j]) == false)
                        {
                            matrix[i,j] -= minimum;
                        }
                        if(rowsCovered[i] && columnsCovered[j])
                        {
                            matrix[i,j] += minimum;
                        }
                    }
                }
            }

            return result;
        }

        private bool IsCovered(int[,] matrix, int i, int j)
            => matrix.GetRow(i).Contains(1) || matrix.GetColumn(j).Contains(1);

        private bool IsDoubleCovered(int[,] matrix, int i, int j)
            => matrix.GetRow(i).Contains(1) && matrix.GetColumn(j).Contains(1);

        private bool IsResolved(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            var count = 0;

            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(matrix[i,j] == 1)
                    {
                        count++;
                        if(matrix.GetRow(i).Count(v => v == 1) > 1 || matrix.GetColumn(j).Count(v => v == 1) > 1)
                        {
                            return false;
                        }
                    }
                }
            }

            return count == n;
        }

        private int CountCoveredZeros(int[,] matrix, bool[] rowsCovered, bool[] columnsCovered)
        {
            var counter = 0;
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(matrix[i,j] == 0 && (rowsCovered[i] || columnsCovered[j]))
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }
    }
}