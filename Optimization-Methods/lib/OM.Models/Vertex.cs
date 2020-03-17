using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OM.Models
{
    public class Vertex
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }


        //!To avoid loops and nested properties
        [JsonIgnore]
        public ICollection<Vertex> NeighbouringVertices { get; set; }
        [JsonIgnore]
        public ICollection<Edge> ConnectedEdges { get; set; }
    }
}