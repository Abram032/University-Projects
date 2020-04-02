using System;
using OM.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using OM.Algorithms;
using System.Linq;
using OM.Utils;

namespace OM.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var utils = new GraphUtils();
            const string basePath = "../../resources/graphs";
            var graphPath1 = $"{basePath}/graph-1.json";
            var graphPath2 = $"{basePath}/graph-2.json";
            var graphPath3 = $"{basePath}/graph-3.json";
            var graphPath4 = $"{basePath}/graph-4.json";
            var graphPath5 = $"{basePath}/graph-5.json";

            var json1 = File.ReadAllText(graphPath1);
            var json2 = File.ReadAllText(graphPath2);
            var json3 = File.ReadAllText(graphPath3);
            var json4 = File.ReadAllText(graphPath4);
            var json5 = File.ReadAllText(graphPath5);
            
            var graph1 = Graph.Deserialize(json1);
            var graph2 = Graph.Deserialize(json2);
            var graph3 = Graph.Deserialize(json3);   
            var graph4 = Graph.Deserialize(json4);        
            var graph5 = Graph.Deserialize(json5);        

            var ec = new EulerCycle();
            var result1 = ec.Search(graph1);
            var result2 = ec.Search(graph2);
            var result3 = ec.Search(graph3);
            var result4 = ec.Search(graph4);
            var result5 = ec.Search(graph5);

            System.Console.WriteLine($"Graph 1:\n{result1}");
            System.Console.WriteLine($"Graph 2:\n{result2}");
            System.Console.WriteLine($"Graph 3:\n{result3}");
            System.Console.WriteLine($"Graph 4:\n{result4}");
            System.Console.WriteLine($"Graph 5:\n{result5}");

            var bfs = new BFS();
            var maxmatch = new MaximalMatching();
        
            bfs.ColorGraph(graph2, graph2.Vertices.FirstOrDefault());
            if(utils.IsGraphBipartite(graph2, out var G2V1, out var G2V2))
            {               
                maxmatch.Search(graph2, G2V1, G2V2);
                System.Console.WriteLine("Graph 2 - Maximum matching:");
                foreach(var matchedEdge in graph2.Edges.Where(e => e.IsMatched))
                {
                    System.Console.WriteLine($"{matchedEdge.Name}");
                }
            }

            bfs.ColorGraph(graph4, graph4.Vertices.FirstOrDefault());
            if(utils.IsGraphBipartite(graph4, out var G4V1, out var G4V2))
            {
                maxmatch.Search(graph4, G4V1, G4V2);
                System.Console.WriteLine("Graph 4 - Maximum matching:");
                foreach(var matchedEdge in graph4.Edges.Where(e => e.IsMatched))
                {
                    System.Console.WriteLine($"{matchedEdge.Name}");
                }
            }

            bfs.ColorGraph(graph5, graph5.Vertices.FirstOrDefault());
            if(utils.IsGraphBipartite(graph5, out var G5V1, out var G5V2))
            {
                maxmatch.Search(graph5, G5V1, G5V2);
                System.Console.WriteLine("Graph 5 - Maximum matching:");
                foreach(var matchedEdge in graph5.Edges.Where(e => e.IsMatched))
                {
                    System.Console.WriteLine($"{matchedEdge.Name}");
                }
            }
        }
    }
}
