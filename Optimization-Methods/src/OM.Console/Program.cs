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
            //! File operations
            string path;
            if(args.Length > 0)
            {
                path = args.FirstOrDefault();
            }
            else if(Directory.Exists($"{Directory.GetCurrentDirectory()}/graphs"))
            {
                path = $"{Directory.GetCurrentDirectory()}/graphs";
            }
            else 
            {
                System.Console.WriteLine("Could not locate ./graphs directory.");
                return;
            }

            var files = Directory.GetFiles(path);
            var graphs = new List<Graph>();
            foreach(var file in files)
            {
                var content = File.ReadAllText(file);
                var filename = Path.GetFileName(file);
                if(Graph.TryDeserialize(content, out var graph))
                {
                    graph.Name = filename.Split('.').FirstOrDefault();;
                    graphs.Add(graph);
                }
                else
                {
                    System.Console.WriteLine($"Could not parse: {filename}");
                }
            }

            if(graphs.Count == 0) 
            {
                System.Console.WriteLine("Could not find any graphs.");
                return;
            }

            //! Euler Cycle
            var ec = new EulerCycle();
            foreach(var graph in graphs)
            {
                var result = ec.Search(graph.Copy());
                System.Console.WriteLine($"Euler Cycle - {graph.Name}:");
                System.Console.WriteLine($"{result}");
            }

            System.Console.WriteLine();

            //! Maximal Matching
            var bfs = new BFS();
            var maxmatch = new MaximalMatching();
            foreach(var graph in graphs)
            {
                var result = maxmatch.Search(graph.Copy());
                System.Console.WriteLine($"Maximal Matching - {graph.Name}:");
                System.Console.WriteLine($"{result}");
            }

            //!Hungarian method
            var hungarianMethod = new HungarianMethod();
            foreach(var graph in graphs)
            {
                var result = hungarianMethod.Resolve(graph.Copy());
                System.Console.WriteLine($"Hungarian Method - {graph.Name}:");
                System.Console.WriteLine($"{result}");
            }
        }
    }
}
