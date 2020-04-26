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
        public string Resolve(Graph graph)
        {
            var bfs = new BFS();
            bfs.ColorGraph(graph, graph.Vertices.FirstOrDefault());
            if(!graph.IsBipartite(out var V1, out var V2)) 
            {
                return "Graph is not bipartite.";
            }
            if(V1.Count != V2.Count) 
            {
                return "Graph has uneven groups of vertices.";
            }

            //Ordering vertices for later rebuild
            V1 = V1.OrderBy(p => p.Name).ToList();
            V2 = V2.OrderBy(p => p.Name).ToList();
            //Mapping graph to matrix
            var matrix = MapToMatrix(graph, V1, V2);

            var result = Resolve(matrix);

            var sb = new StringBuilder();
            for(int i = 0; i < V1.Count; i++) 
            {
                for(int j = 0; j < V2.Count; j++)
                {
                    if(result[i,j] == 1) 
                    {
                        sb.Append($"{V1[i].Name}-{V2[j].Name} | ");
                    }
                }
            }
            if(sb.Length > 2)
            {
                sb.Remove(sb.Length - 3, 3);
            }

            return sb.ToString();
        }

        public int[,] Resolve(int[,] matrix)
        {
            var reference = matrix.Clone() as int[,];
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
            while(!IsResolved(matrix, reference, out result))
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
                            rowsCovered[i] = true;
                            columnsCovered[j] = true;
                        }
                    }
                }

                //Reducing cover lines to minimum
                var zeros = matrix.Count(v => v == 0);
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
                var linesCount = rowsCovered.Count(v => v == true) + columnsCovered.Count(v => v == true);

                if(linesCount >= n && IsResolved(matrix, reference, out result))
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

        private bool IsResolved(int[,] matrix, int[,] reference, out int[,] result)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            result = new int[n, m];
            var rowsCovered = new bool[n];
            var columnsCovered = new bool[m];

            //Building result, starting with rows
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(matrix[i,j] == 0 && !rowsCovered[i] && !columnsCovered[j])
                    {
                        result[i,j] = 1;
                        rowsCovered[i] = true;
                        columnsCovered[j] = true;
                    }
                }
            }

            //Solution found, all vertices are assigned
            if(result.Count(v => v == 1) == n) {
                return true;
            }

            //Not all vertices got assigned, starting with columns
            Array.Clear(rowsCovered, 0, rowsCovered.Length);
            Array.Clear(columnsCovered, 0, columnsCovered.Length);
            Array.Clear(result, 0, result.Length);
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if(matrix[j, i] == 0 && !rowsCovered[j] && !columnsCovered[i])
                    {
                        result[j, i] = 1;
                        rowsCovered[j] = true;
                        columnsCovered[i] = true;
                    }
                }
            }

            //Solution found, all vertices are assigned
            if(result.Count(v => v == 1) == n) {
                return true;
            }

            //If we got here, vertices still could not be assigned, continuing
            return false;
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

        private int[,] MapToMatrix(Graph graph, List<Vertex> V1, List<Vertex> V2)
        {
            var matrix = new int[V1.Count, V2.Count];

            for(int i = 0; i < V1.Count; i++)
            {
                for(int j = 0; j < V2.Count; j++)
                {
                    if(V1[i].NeighbouringVertices.Contains(V2[j]))
                    {
                        var edge = V1[i].ConnectedEdges
                            .FirstOrDefault(e => e.VertexB == V2[j] || e.VertexA == V2[j]);

                        matrix[i,j] = edge.Weight;
                    }
                }
            }

            return matrix;
        }
    }
}