using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OM.Models;
using OM.Utils;

namespace OM.Algorithms
{
    public class MaximalMatching
    {
        public string Search(Graph graph)
        {
            var bfs = new BFS();

            bfs.ColorGraph(graph, graph.Vertices.FirstOrDefault());
            if(graph.IsGraphBipartite(out var V1, out var V2) == false)
            {
                return "Graph is not bipartite!";
            }

            foreach(var vertex in V1)
            {
                if(vertex.IsMatched == false)
                {
                    var match = vertex.NeighbouringVertices.FirstOrDefault(v => V2.Contains(v) && v.IsMatched == false);
                    if(match != null)
                    {
                        vertex.IsMatched = true;
                        match.IsMatched = true;
                        var edge = graph.Edges.FirstOrDefault(
                            e => 
                                (e.VertexA == vertex && e.VertexB == match) ||
                                (e.VertexB == vertex && e.VertexA == match)
                            );
                        edge.IsMatched = true;
                        var c = 0 == vertex.Color;
                    }
                    else
                    {
                        var path = bfs.FindPathToNearestUnmatched(graph, vertex);
                        graph.ReverseMatching(path);
                    }
                }
            }

            var sb = new StringBuilder();
            foreach(var matchedEdge in graph.Edges.Where(e => e.IsMatched))
            {
                sb.Append($"{matchedEdge.Name} | ");
            }
            if(sb.Length > 2)
            {
                sb.Remove(sb.Length - 3, 3);
            }
            return sb.ToString();
        }
    }
}