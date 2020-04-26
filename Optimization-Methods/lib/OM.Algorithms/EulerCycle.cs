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
            var stack = new Stack<Vertex>();
            var path = new List<Vertex>();

            var isValid = IsGraphValid(g);
            if(!isValid) {
                return "Graph doesn't contain euler cycle.";
            }

            stack.Push(g.Vertices.FirstOrDefault());

            while(stack.Count > 0)
            {
                var vertex = stack.Peek();
                if(vertex.NeighbouringVertices.Count > 0)
                {
                    var edge = vertex.ConnectedEdges.FirstOrDefault();
                    var neighbour = edge.GetOpposingVertex(vertex);
                    edge.DisconnectVertices();
                    stack.Push(neighbour);
                }
                else
                {
                    stack.Pop();
                    path.Insert(0, vertex);
                }
            }

            if(g.Vertices.Any(e => e.ConnectedEdges.Count > 0))
            {
                return "Graph doesn't contain euler cycle.";
            }

            var sb = new StringBuilder();
            foreach(var vertex in path) 
            {
                sb.Append($"{vertex.Name} -> ");
            }
            if(sb.Length > 3)
            {
                sb.Remove(sb.Length - 4, 4);
            }
            
            return sb.ToString();     
        }

        private bool IsGraphValid(Graph g)
        {
            if(g.Vertices.Any(v => v.ConnectedEdges.Count % 2 != 0) &&
                g.IsDirected == false)
            {
                return false;
            }

            if(g.Vertices.Any(v => v.NeighbouringVertices == null || 
                v.NeighbouringVertices.Count == 0 || 
                v.ConnectedEdges == null ||
                v.ConnectedEdges.Count == 0))
            {
                return false;
            }

            if(g.IsDirected)
            {
                foreach(var vertex in g.Vertices)
                {
                    //Amount of outgoing edges to which vertex is connected
                    var outgoingEdges = vertex.ConnectedEdges.Count;
                    //Amount of incoming edges for which vertex is ending point
                    var incomingEdges = g.Edges.Count(e => e.VertexB == vertex);

                    if(incomingEdges != outgoingEdges) {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
