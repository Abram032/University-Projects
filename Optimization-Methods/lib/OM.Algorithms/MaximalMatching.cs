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
        public void Search(Graph graph, List<Vertex> v1, List<Vertex> v2)
        {
            var bfs = new BFS();
            var utils = new GraphUtils();
            foreach(var vertex in v1)
            {
                if(vertex.IsMatched == false)
                {
                    var match = vertex.NeighbouringVertices.FirstOrDefault(v => v2.Contains(v) && v.IsMatched == false);
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
                        utils.ReverseMatching(graph, path);
                    }
                }
            }
        }
    }
}