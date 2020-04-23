using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace OM.Models
{
    public class Vertex
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public Color Color { get; set; }
        public bool IsVisited { get; set; }
        public bool IsMatched { get; set; }
        public ICollection<Vertex> NeighbouringVertices { get; set; }
        public ICollection<Edge> ConnectedEdges { get; set; }

        public Vertex(string name)
        {
            Name = name;
            Color = Color.None;
            IsVisited = false;
            IsMatched = false;
            Weight = 0;
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