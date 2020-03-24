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
            var paths = new List<Stack<string>>();
            var vertex = g.Vertices.FirstOrDefault();
            paths.Add(new Stack<string>());
            if(vertex == null) {
                return "Graph is empty";
            }

            bool pathExist = vertex.ConnectedEdges.Any();

            while(pathExist)
            {
                
            }
            

            return PrintPath(paths);      
        }

        private string PrintPath(Stack<string> path)
        {
            var result = new StringBuilder();

            while(path.Count > 0)
            {
                var vertex = path.Pop();
                result.Append(vertex);
                if(path.Count > 0) {
                    result.Append(" -> ");
                }
            }

            return result.ToString();
        }

        private string PrintPath(List<Stack<string>> paths)
        {
            var result = new StringBuilder();

            foreach(var path in paths)
            {
                while(path.Count > 0)
                {
                    var vertex = path.Pop();
                    result.Append(vertex);
                    if(path.Count > 0) {
                        result.Append(" -> ");
                    }
                }
                result.AppendLine();
            }
        
            return result.ToString();
        }
    }
}
