using System;
using OM.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace OM.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Vertex 
            {
                Id = Guid.NewGuid(),
                Name = "A",
                Weight = 1
            };

            var b = new Vertex
            {
                Id = Guid.NewGuid(),
                Name = "B",
                Weight = 2
            };

            var c = new Vertex
            {
                Id = Guid.NewGuid(),
                Name = "C",
                Weight = 3
            };

            var e1 = new Edge
            {
                Id = Guid.NewGuid(),
                Name = "E1",
                Weight = 1
            };

            var e2 = new Edge
            {
                Id = Guid.NewGuid(),
                Name = "E2",
                Weight = 1
            };

            var e3 = new Edge
            {
                Id = Guid.NewGuid(),
                Name = "E3",
                Weight = 1
            };

            var graph = new Graph
            {
                Vertices = new List<Vertex> {a, b, c},
                Edges = new List<Edge> {e1, e2, e3}
            };

            e1.ConnectVertices(a, b);
            e2.ConnectVertices(b, c);
            e3.ConnectVertices(c, a);

            var json = Graph.Serialize(graph);
            using(var sw = new StreamWriter("output.json"))
            {
                sw.Write(json);
            }
            var g = JsonConvert.DeserializeObject<Graph>(json);
            var g2 = Graph.Deserialize(json);
        }
    }
}
