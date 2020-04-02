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
        //!Technical field only
        [JsonIgnore]
        public Color Color { get; set; }
        //!Technical field only
        [JsonIgnore]
        public bool IsVisited { get; set; }
        //!Technical field only
        [JsonIgnore]
        public bool IsMatched { get; set; }
        //!To avoid loops and nested properties
        [JsonIgnore]
        public ICollection<Vertex> NeighbouringVertices { get; set; }
        //!To avoid loops and nested properties
        [JsonIgnore]
        public ICollection<Edge> ConnectedEdges { get; set; }

    }

    public enum Color
    {
        None,
        Red,
        Blue
    }
}