using OM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OM.Algorithms
{
    public class AntColony
    {
        public string Search(Graph graph, float rho, int turns)
        {
            var matrix = graph.ToMatrix();
            return Search(matrix, rho, turns);
        }

        public string Search(int[,] matrix, float rho, int turns)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            var pheromoneMatrix = new float[n, m];
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    pheromoneMatrix[i,j] = 1;
                }
            }

            var ants = new List<Ant>();

            var bestTrail = new List<int>();
            var bestTrailCost = int.MaxValue;

            for(int turn = 0; turn < turns; turn++)
            {
                //Creating ants for turn
                for(int i = 0; i < n; i++)
                {
                    ants.Add(new Ant(i));
                }

                //Moving every ant until they finish trail or get stuck
                while(ants.Any(a => a.CanMove))
                {
                    foreach(var ant in ants)
                    {
                        ant.Move(matrix, pheromoneMatrix);
                    }
                }

                //Removing ants that got stuck without passing each location
                ants.RemoveAll(a => a.LatestTrail.Count != n);
                
                //Pheromone evaporation
                for(int i = 0; i < n; i++)
                {
                    for(int j = 0; j < m; j++)
                    {
                        pheromoneMatrix[i,j] = (1 - rho) * pheromoneMatrix[i,j];
                    }
                }

                //Adding ants pheromone
                foreach(var ant in ants)
                {
                    for(int j = 1; j < ant.LatestTrail.Count; j++)
                    {
                        var i = ant.LatestTrail[j - 1];
                        pheromoneMatrix[i, j] += (1F / ant.TrailCost);
                    }
                }

                //Finding best ant and trail
                var bestAnt = ants.OrderBy(a => a.TrailCost).FirstOrDefault();
                if(bestAnt != null && bestAnt.TrailCost < bestTrailCost)
                {
                    bestTrail = bestAnt.LatestTrail;
                    bestTrailCost = bestAnt.TrailCost;

                    Console.WriteLine($"New best trail found with cost {bestTrailCost} at turn {turn} with path:");
                    Console.WriteLine(BuildTrailString(bestTrail));
                }

                //Removing all ants before next turn
                ants.Clear();
            }

            var sb = new StringBuilder();
            if(bestTrail.Count > 0) 
            {
                sb.AppendLine($"Best trail with cost {bestTrailCost} after {turns} turns at:");
                sb.AppendLine(BuildTrailString(bestTrail));
            }
            else 
            {
                sb.AppendLine("No trail could be found that would pass each location without backtracking.");
            }
            
            return sb.ToString();
        }

        private string BuildTrailString(List<int> locations)
        {
            var sb = new StringBuilder();
            foreach (var location in locations)
            {
                sb.Append($"{location + 1} -> ");
            }
            if(sb.Length > 3)
            {
                sb.Remove(sb.Length - 4, 4);
            }
            return sb.ToString();
        }
    }
}