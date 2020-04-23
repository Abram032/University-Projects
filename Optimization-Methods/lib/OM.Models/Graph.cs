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
        public ICollection<Vertex> Vertices { get; set; }
        public ICollection<Edge> Edges { get; set; }
        public bool IsDirected { get; set; }

        public Graph()
        {
            Vertices = new List<Vertex>();
            Edges = new List<Edge>();
        }

        public static Graph Deserialize(string file)
        {
            var graph = new Graph();
            
            var lines = file.Split('\n');
            
            for(int i = 0; i < lines.Length; i++)
            {
                if(lines[i].Equals("#DIGRAPH"))
                {
                    i+=1;
                    var value = lines[i];
                    if(bool.TryParse(value, out bool result))
                    {
                        graph.IsDirected = result;
                    }
                    else
                    {
                        Console.WriteLine($"Could not parse \'{lines[i]}\' to bool.");
                    }
                }
                else if(Regex.IsMatch(lines[i], "^\\w+ \\w+ \\d+\\n?$"))
                {
                    var values = lines[i].Split(' ');
                    var vertexA = new Vertex(values[0]);
                    var vertexB = new Vertex(values[1]);
                    var edge = new Edge(vertexA, vertexB, isDirected: graph.IsDirected);
                    if(int.TryParse(values[2], out int result))
                    {
                        edge.Weight = result;
                    }
                    else
                    {
                        Console.WriteLine($"Could not parse \'{values[2]}\' to int.");
                    }
                    graph.Vertices.Add(vertexA);
                    graph.Vertices.Add(vertexB);
                    graph.Edges.Add(edge);
                }
            }
            return graph;
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