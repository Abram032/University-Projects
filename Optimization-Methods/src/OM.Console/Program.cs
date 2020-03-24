using System;
using OM.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace OM.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph1 = "../../resources/graphs/graph-1.json";
            var graph2 = "../../resources/graphs/graph-2.json";
            var graph3 = "../../resources/graphs/graph-3.json";

            var json = File.ReadAllText(graph1);

            var graph = Graph.Deserialize(json);

            
        }
    }
}
