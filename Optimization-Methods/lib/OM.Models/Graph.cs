using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace OM.Models
{
    public class Graph
    {
        public ICollection<Vertex> Vertices { get; set; }
        public ICollection<Edge> Edges { get; set; }

        public static Graph Deserialize(string json)
        {
            var graph = JsonConvert.DeserializeObject<Graph>(json);
            foreach(var edge in graph.Edges) 
            {
                var a = graph.Vertices.FirstOrDefault(v => v.Name.Equals(edge.VertexA_Name));
                var b = graph.Vertices.FirstOrDefault(v => v.Name.Equals(edge.VertexB_Name));
                edge.ConnectVertices(a, b, edge.IsDirected);
            }
            foreach(var vertex in graph.Vertices)
            {
                vertex.Color = Color.None;
                vertex.IsVisited = false;
                vertex.IsMatched = false;
            }
            return graph;
        }

        public static string Serialize(Graph graph)
        {
            return JsonConvert.SerializeObject(graph, Formatting.Indented);
        }
    }
}