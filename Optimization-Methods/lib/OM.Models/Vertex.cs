using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace OM.Models
{
    public class Vertex
    {
        public int Name { get; set; }
        public Color Color { get; set; }
        public bool IsVisited { get; set; }
        public bool IsMatched { get; set; }
        public ICollection<Vertex> NeighbouringVertices { get; set; }
        public ICollection<Edge> ConnectedEdges { get; set; }

        public Vertex()
        {          
        }

        public Vertex(int name)
        {
            Name = name;
            Color = Color.None;
            IsVisited = false;
            IsMatched = false;
            NeighbouringVertices = new List<Vertex>();
            ConnectedEdges = new List<Edge>();
        }
    }

    public enum Color
    {
        None,
        Red,
        Blue
    }
}