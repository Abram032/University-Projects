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
                return "Graph doesn't contain euler cycle.\n";
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

            var sb = new StringBuilder();
            foreach(var vertex in path) 
            {
                sb.Append($"{vertex.Name} -> ");
            }
            sb.Remove(sb.Length - 4, 4);
            
            return sb.ToString();     
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
