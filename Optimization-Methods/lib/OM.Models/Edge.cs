using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OM.Models
{
    public class Edge
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public bool IsDirected { get; set; }
        ///*Serialization use only
        public string VertexA_Name { get; set; }
        ///*Serialization use only
        public string VertexB_Name { get; set; }

        //!To avoid loops and nested properties
        [JsonIgnore]
        public Vertex VertexA { get; set; }
        //!To avoid loops and nested properties
        [JsonIgnore]
        public Vertex VertexB { get; set; }

        public void ConnectVertices(Vertex a, Vertex b, bool isDirected = false)
        {
            VertexA = a;
            VertexB = b;
            VertexA_Name = a.Name;
            VertexB_Name = b.Name;
            IsDirected = isDirected;

            if(a.ConnectedEdges == null) {
                a.ConnectedEdges = new List<Edge>();
            }
            if(b.ConnectedEdges == null) {
                b.ConnectedEdges = new List<Edge>();
            }
            a.ConnectedEdges.Add(this);

            if(isDirected == false) {
                b.ConnectedEdges.Add(this);
            }

            if(a.NeighbouringVertices == null) {
                a.NeighbouringVertices = new List<Vertex>();
            }
            if(b.NeighbouringVertices == null) {
                b.NeighbouringVertices = new List<Vertex>();
            }

            a.NeighbouringVertices.Add(b);
            if(isDirected == false) {
                b.NeighbouringVertices.Add(a);
            }
        }
        
        public void DisconnectVertices()
        {
            VertexA_Name = null;
            VertexB_Name = null;

            VertexA.ConnectedEdges.Remove(this);
            VertexB.ConnectedEdges.Remove(this);

            VertexA.NeighbouringVertices.Remove(VertexB);
            VertexB.NeighbouringVertices.Remove(VertexA);

            VertexA = null;
            VertexB = null;
        }

        public Vertex GetOpposingVertex(Vertex v) => (v == VertexA) ? VertexB : VertexA;
    }
}