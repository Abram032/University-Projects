using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OM.Models
{
    public class Edge
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public bool IsDirected { get; set; }
        ///*Serialization use only
        public Guid VertexA_Id { get; set; }
        ///*Serialization use only
        public Guid VertexB_Id { get; set; }

        //!To avoid loops and nested properties
        [JsonIgnore]
        public Vertex VertexA { get; set; }
        [JsonIgnore]
        public Vertex VertexB { get; set; }

        public void ConnectVertices(Vertex a, Vertex b, bool isDirected = false)
        {
            VertexA = a;
            VertexB = b;
            VertexA_Id = a.Id;
            VertexB_Id = b.Id;
            IsDirected = isDirected;

            if(a.ConnectedEdges == null) {
                a.ConnectedEdges = new List<Edge>();
            }
            if(b.ConnectedEdges == null) {
                b.ConnectedEdges = new List<Edge>();
            }
            a.ConnectedEdges.Add(this);
            b.ConnectedEdges.Add(this);

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
    }
}