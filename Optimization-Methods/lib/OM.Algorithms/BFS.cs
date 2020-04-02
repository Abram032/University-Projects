using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OM.Models;

namespace OM.Algorithms
{
    public class BFS
    {
        public void ColorGraph(Graph graph, Vertex vertex)
        {
            foreach(var v in graph.Vertices)
            {
                v.IsVisited = false;
            }
            var queue = new Queue<Vertex>();
            vertex.Color = Color.Blue;
            queue.Enqueue(vertex);
            while(queue.Count > 0)
            {
                var v = queue.Dequeue();
                v.IsVisited = true;
                foreach(var neighbour in v.NeighbouringVertices)
                {
                    if(neighbour.Color == Color.None)
                    {
                        neighbour.Color = v.Color == Color.Blue ? Color.Red : Color.Blue;
                    }
                    if(neighbour.IsVisited == false)
                    {
                        queue.Enqueue(neighbour);
                    }
                }
            }
        }

        public List<Vertex> FindPathToNearestUnmatched(Graph graph, Vertex vertex)
        {
            foreach(var v in graph.Vertices)
            {
                v.IsVisited = false;
            }

            var paths = new Queue<List<Vertex>>();
            paths.Enqueue(new List<Vertex> { vertex });
            
            while(paths.Count > 0)
            {
                var path = paths.Dequeue();
                var lastVertex = path.Last();

                lastVertex.IsVisited = true;
                
                if(lastVertex.IsMatched == false && lastVertex.Color != vertex.Color)
                {
                    return path;
                }

                foreach(var neighbour in lastVertex.NeighbouringVertices.Where(v => v.IsVisited == false))
                {
                    var newPath = new List<Vertex>(path);
                    newPath.Add(neighbour);
                    paths.Enqueue(newPath);    
                }
            }
            
            return null;
        }
    }
}