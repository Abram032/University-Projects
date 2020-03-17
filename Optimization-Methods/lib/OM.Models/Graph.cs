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
                var a = graph.Vertices.FirstOrDefault(v => v.Id.Equals(edge.VertexA_Id));
                var b = graph.Vertices.FirstOrDefault(v => v.Id.Equals(edge.VertexB_Id));
                edge.ConnectVertices(a, b, edge.IsDirected);
            }
            return graph;
        }

        public static string Serialize(Graph graph)
        {
            return JsonConvert.SerializeObject(graph, Formatting.Indented);
        }
    }
}