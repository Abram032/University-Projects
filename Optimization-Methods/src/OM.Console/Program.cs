using System;
using OM.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using OM.Algorithms;

namespace OM.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var graphPath1 = "../../resources/graphs/graph-1.json";
            var graphPath2 = "../../resources/graphs/graph-2.json";
            var graphPath3 = "../../resources/graphs/graph-3.json";

            var json1 = File.ReadAllText(graphPath1);
            var json2 = File.ReadAllText(graphPath2);
            var json3 = File.ReadAllText(graphPath3);

            var graph1 = Graph.Deserialize(json1);
            var graph2 = Graph.Deserialize(json2);
            var graph3 = Graph.Deserialize(json3);

            var ec = new EulerCycle();
            var result1 = ec.Search(graph1);
            var result2 = ec.Search(graph2);
            var result3 = ec.Search(graph3);

            System.Console.WriteLine($"Graph 1:\n{result1}");
            System.Console.WriteLine($"Graph 2:\n{result2}");
            System.Console.WriteLine($"Graph 3:\n{result3}");
        }
    }
}
