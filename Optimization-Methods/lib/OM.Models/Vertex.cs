namespace OM.Models
{
    public class Vertex
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public ICollection<Vertex> NeighbouringVertices { get; set; }
    }
}