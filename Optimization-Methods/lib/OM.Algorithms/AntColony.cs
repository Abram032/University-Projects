using OM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OM.Algorithms
{
    public class AntColony
    {
        public string Search(Graph graph)
        {
            var matrix = graph.ToMatrix();
            return Search(matrix);
        }

        public string Search(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            //Pheromone evaporation factor
            float rho = 0.5F;
            int turns = 5;

            var phermoneMatrix = new float[n, m];
            var ants = new List<Ant>();

            for(int i = 0; i < n; i++)
            {
                ants.Add(new Ant(new Coordinate(i, i)));
            }


        }
    }
}