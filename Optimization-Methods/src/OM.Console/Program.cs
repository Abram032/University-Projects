using System;
using OM.Models;
using Newtonsoft.Json;

namespace OM.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Vertex 
            {
                Name = "A",
                Weight = 1,
                NeighbouringVertices = new List<Vertex>()
            };

            var b = new Vertex
            {
                Name = "B",
                Weight = 2,
                NeighbouringVertices = new List<Vertex>()
            };

            var c = new Vertex
            {
                Name = "C",
                Weight = 3,
                NeighbouringVertices = new List<Vertex>()
            };

            a.NeighbouringVertices.Add(b);
            b.NeighbouringVertices.Add(c);
            c.NeighbouringVertices.Add(a);

            var graph = new Graph
            {
                Vertices = new List<Vertex> {a, b, c}
            };

            var json = JsonConvert.SerializeObject(graph);
        }
    }
}
