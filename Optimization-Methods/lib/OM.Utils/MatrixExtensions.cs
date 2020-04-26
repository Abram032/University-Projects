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

        public static Graph ToGraph(this int[,] matrix)
        {
            var n = matrix.GetLength(0);
            var m = matrix.GetLength(1);
            var isDirected = matrix.IsDirected();

            var graph = new Graph() {
                Name = "Graph",
                IsDirected = isDirected,
                Vertices = new List<Vertex>(),
                Edges = new List<Edge>()
            };

            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(matrix[i,j] != 0)
                    {
                        var v1 = graph.Vertices.FirstOrDefault(v => v.Name == i+1);
                        var v2 = graph.Vertices.FirstOrDefault(v => v.Name == j+1);
                        var weight = matrix[i,j];
                        if(v1 == null) {
                            v1 = new Vertex(i+1);
                            graph.Vertices.Add(v1);
                        }
                        if(v2 == null) {
                            v2 = new Vertex(j+1);
                            graph.Vertices.Add(v2);
                        }
                        var e = new Edge(v1, v2, weight, isDirected);
                        graph.Edges.Add(e);
                    }
                }
            }
            return graph;
        }

        public static bool IsDirected(this int[,] matrix)
        {
            var n = matrix.GetLength(0);
            var m = matrix.GetLength(1);

            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(matrix[i,j] != matrix[j,i]) {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}