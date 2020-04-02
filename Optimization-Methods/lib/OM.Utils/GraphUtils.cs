using System;
using System.Collections.Generic;
using System.Linq;
using OM.Models;

namespace OM.Utils
{
    public class GraphUtils
    {
        public bool IsGraphBipartite(Graph graph, out List<Vertex> v1, out List<Vertex> v2)
        {
            v1 = null;
            v2 = null;
            bool isBipartite = true;
            foreach(var vertex in graph.Vertices)
            {
                if(vertex.NeighbouringVertices.Any(v => v.Color == vertex.Color))
                {
                    isBipartite = false;
                    break;
                }
            }
            if(isBipartite)
            {
                v1 = graph.Vertices.Where(v => v.Color == Color.Blue).ToList();
                v2 = graph.Vertices.Where(v => v.Color == Color.Red).ToList();
            }
            return isBipartite;
        }

        public void ReverseMatching(Graph graph, List<Vertex> vertices)
        {
            var edges = new List<Edge>();
            for(int i = 1; i < vertices.Count; i++)
            {
                edges.Add(graph.Edges.FirstOrDefault(
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
    }
}
