using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OM.Models;

namespace OM.Algorithms
{
    public class EulerCycle
    {
        public string Search(Graph g)
        {
            var path = new Stack<Vertex>();
            var result = new StringBuilder();

            var isValid = IsGraphValid(g);
            if(!isValid) {
                return "Graph doesn't contain euler cycle.\n";
            }

            while(g.Vertices.Any(v => v.ConnectedEdges.Any()))
            {
                var vertex = g.Vertices.FirstOrDefault(v => v.ConnectedEdges.Any());
                result.Append($"{vertex.Name} -> ");

                do 
                {
                    while(vertex.ConnectedEdges.Any()) 
                    {
                        var edge = vertex.ConnectedEdges.FirstOrDefault();
                        var nextVertex = edge.GetOpposingVertex(vertex);
                        path.Push(vertex);
                        edge.DisconnectVertices();
                        vertex = nextVertex;
                    }

                    if(path.Count > 0)
                    {
                        vertex = path.Pop();
                        if(path.Count > 0)
                            result.Append($"{vertex.Name} -> ");                        
                        else
                            result.Append($"{vertex.Name}");
                    }

                } while(path.Count > 0);

                result.AppendLine();
            }         
            
            return result.ToString();     
        }

        private bool IsGraphValid(Graph g)
        {
            if(g.Vertices.Any(v => v.ConnectedEdges.Count % 2 != 0) &&
                g.Edges.All(e => e.IsDirected == false))
            {
                return false;
            }

            if(g.Edges.Any(e => e.IsDirected == true))
            {
                foreach(var vertex in g.Vertices)
                {
                    //Amount of directed edges for which vertex is ending point
                    var incomingEdges = g.Edges.Count(e => e.VertexB == vertex && e.IsDirected);
                    //Amount of undirected edges to which vertex is connected
                    var outgoingEdges = vertex.ConnectedEdges.Count;

                    if(incomingEdges != outgoingEdges) {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
