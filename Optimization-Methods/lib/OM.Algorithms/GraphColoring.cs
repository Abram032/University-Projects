using OM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OM.Algorithms
{
    public class GraphColoring
    {
        public string ColorGraph(Graph g)
        {
            var vertices = new List<Vertex>(g.Vertices);
            int k = 0;

            while (vertices.Count != 0) {
                k += 1;
                var subset = GetMostIndependentSubset(vertices);
                foreach(var vertex in subset)
                {
                    var color = (Color)k;
                    vertex.Color = color;
                    vertices.Remove(vertex);
                }
            }
            var sb = new StringBuilder();
            foreach (var vertex in g.Vertices)
            {
                sb.AppendLine($"{vertex.Name} - {vertex.Color}");
            }
            return sb.ToString();
        }

        private List<Vertex> GetMostIndependentSubset(List<Vertex> vertices)
        {
            var U = new List<Vertex>(vertices);
            var subset = new List<Vertex>();
            while(U.Count != 0)
            {
                var vertex = GetMinDegreeVertex(U);
                subset.Add(vertex);
                U.Remove(vertex);
                U.RemoveAll(v => v.NeighbouringVertices.Contains(vertex));
            }
            return subset;
        }

        private Vertex GetMinDegreeVertex(List<Vertex> vertices)
        {
            Vertex result = null;
            int min = int.MaxValue;
            foreach(var vertex in vertices)
            {
                if(vertex.ConnectedEdges.Count < min)
                {
                    min = vertex.ConnectedEdges.Count;
                    result = vertex;
                }
            }
            return result;
        }
    }
}
