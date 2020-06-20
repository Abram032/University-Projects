using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace OM.Models
{
    public class Graph
    {
        public string Name { get; set; }
        public ICollection<Vertex> Vertices { get; set; }
        public ICollection<Edge> Edges { get; set; }
        public bool IsDirected { get; set; }

        public Graph()
        {
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();
        }

        public Graph Copy()
        {
            var _graph = new Graph() {
                Name = this.Name,
                IsDirected = this.IsDirected,
                Vertices = new List<Vertex>(),
                Edges = new List<Edge>()
            };

            foreach(var vertex in Vertices)
            {
                var _vertex = new Vertex() {
                    Name = vertex.Name,
                    IsMatched = vertex.IsMatched,
                    IsVisited = vertex.IsVisited,
                    Color = vertex.Color,
                    NeighbouringVertices = new List<Vertex>(),
                    ConnectedEdges = new List<Edge>()
                };
                _graph.Vertices.Add(_vertex);
            }

            foreach(var edge in Edges)
            {
                var _v1 = _graph.Vertices.FirstOrDefault(v => v.Name == edge.VertexA.Name);
                var _v2 = _graph.Vertices.FirstOrDefault(v => v.Name == edge.VertexB.Name);
                var _edge = new Edge() {
                    Name = edge.Name,
                    Weight = edge.Weight,
                    IsMatched = edge.IsMatched
                };
                _edge.ConnectVertices(_v1, _v2, _graph.IsDirected);
                _graph.Edges.Add(_edge);
            }
            return _graph;
        }

        public int[,] ToMatrix()
        {
            var n = Vertices.Count;
            var matrix = new int[n,n];
            foreach(var edge in Edges) {
                var v1 = edge.VertexA;
                var v2 = edge.VertexB;
                matrix[v1.Name - 1, v2.Name - 1] = edge.Weight;
                if(!IsDirected) {
                    matrix[v2.Name - 1, v1.Name - 1] = edge.Weight;
                }
            }
            return matrix;
        }

        public bool IsBipartite(out List<Vertex> v1, out List<Vertex> v2)
        {
            v1 = null;
            v2 = null;
            bool isBipartite = true;
            foreach(var vertex in Vertices)
            {
                if(vertex.NeighbouringVertices.Any(v => v.Color == vertex.Color))
                {
                    isBipartite = false;
                    break;
                }
            }
            if(isBipartite)
            {
                v1 = Vertices.Where(v => v.Color == Color.Blue).ToList();
                v2 = Vertices.Where(v => v.Color == Color.Red).ToList();
            }
            return isBipartite;
        }

        public void ReverseMatching(List<Vertex> vertices)
        {
            var edges = new List<Edge>();
            for(int i = 1; i < vertices.Count; i++)
            {
                edges.Add(Edges.FirstOrDefault(
                    e => 
                        (e.VertexA == vertices[i-1] && e.VertexB == vertices[i]) ||
                        (e.VertexB == vertices[i-1] && e.VertexA == vertices[i])
                    ));
            }
            foreach(var edge in edges)
            {
                edge.IsMatched = edge.IsMatched ? false : true;
            }
            foreach(var vertex in vertices)
            {
                vertex.IsMatched = vertex.ConnectedEdges.Any(e => e.IsMatched);
            }
        }

        public static bool TryDeserialize(string file, out Graph graph)
        {
            graph = new Graph();
            
            var lines = file.Split('\n');
            
            for(int i = 0; i < lines.Length; i++)
            {
                switch(lines[i])
                {
                    case "#DIGRAPH":

                        i+=1;
                        var value = lines[i];
                        if(bool.TryParse(value, out bool isDirected))
                        {
                            graph.IsDirected = isDirected;
                        }
                        else
                        {
                            Console.WriteLine($"Could not parse \'{lines[i]}\' to bool.");
                            graph = null;
                            return false;
                        }
                        break;

                    case "#EDGES":
                        break;
                    
                    case var line when Regex.IsMatch(line, "^\\d+ \\d+ \\d+\\n?$"):

                        var values = lines[i].Split(' ');
                        Vertex vertexA = null;
                        Vertex vertexB = null;

                        if(int.TryParse(values[0], out int v1_name))
                        {
                            vertexA = graph.Vertices.FirstOrDefault(v => v.Name == v1_name);
                            if(vertexA == null)
                            {
                                vertexA = new Vertex(v1_name);
                                graph.Vertices.Add(vertexA);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Could not parse \'{values[0]}\' to int.");
                            graph = null;
                            return false;
                        }

                        if(int.TryParse(values[1], out int v2_name))
                        {
                            vertexB = graph.Vertices.FirstOrDefault(v => v.Name == v2_name);
                            if(vertexB == null)
                            {
                                vertexB = new Vertex(v2_name);
                                graph.Vertices.Add(vertexB);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Could not parse \'{values[1]}\' to int.");
                            graph = null;
                            return false;
                        }

                        if(int.TryParse(values[2], out int weight))
                        {
                            var edge = new Edge(vertexA, vertexB, weight, graph.IsDirected);
                            graph.Edges.Add(edge);
                        }
                        else
                        {
                            Console.WriteLine($"Could not parse \'{values[2]}\' to int.");
                            graph = null;
                            return false;
                        }
                        break;

                    default:
                        graph = null;
                        return false;
                }
            }
            return true;
        }

        public static string Serialize(Graph graph)
        {
            var sb = new StringBuilder();
            sb.AppendLine("#DIGRAPH");
            sb.AppendLine($"{graph.IsDirected}");
            sb.AppendLine("#EDGES");
            foreach(var edge in graph.Edges)
            {
                var v1 = edge.VertexA.Name;
                var v2 = edge.VertexB.Name;
                var w = edge.Weight;
                sb.AppendLine($"{v1} {v2} {w}");
            }
            return sb.ToString();
        }
    }
}