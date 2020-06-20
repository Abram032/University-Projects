using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OM.Models
{
    public class Ant
    {
        public Ant()
        {
            LatestTrail = new List<int>();
            TrailCost = 0;
            CanMove = true;
        }

        public Ant(int location)
        {
            CurrentLocation = location;
            LatestTrail = new List<int>();
            LatestTrail.Add(location);
            TrailCost = 0;
            CanMove = true;
        }

        public int CurrentLocation { get; set; }
        public List<int> LatestTrail { get; set; }
        public int TrailCost { get; set; }
        public bool CanMove { get; set; }

        public void Move(int[,] matrix, float[,] pheromoneMatrix)
        {
            var location = PickNextLocation(matrix, pheromoneMatrix);
            if(location == -1)
            {
                CanMove = false;
                return;
            }
            var cost = matrix[CurrentLocation, location];

            CurrentLocation = location;
            LatestTrail.Add(location);
            TrailCost += cost;
        }

        private int PickNextLocation(int[,] matrix, float[,] pheromoneMatrix)
        {
            var locations = GetAvailableLocations(matrix);

            if(locations.Count == 0)
            {
                return -1;
            }

            var probabilites = CalculateProbability(matrix, pheromoneMatrix, locations);
            var cumulativeSums = CalculateCumulativeSums(probabilites);
            
            var rng = new Random();
            var number = rng.NextDouble() * cumulativeSums.FirstOrDefault();
            var index = 0;

            for(int i = 1; i < cumulativeSums.Count; i++)
            {
                if(number <= cumulativeSums[i - 1] && number > cumulativeSums[i])
                {
                    index = i;
                    break;
                }
            }

            var location = locations[index];
            return location;
        }

        private List<int> GetAvailableLocations(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            var locations = new List<int>();

            for(int i = 0; i < n; i++) {
                if(CurrentLocation != i && matrix[CurrentLocation, i] != 0 && LatestTrail.Contains(i) == false) {
                    locations.Add(i);
                }
            }

            return locations;
        }

        private List<float> CalculateProbability(int[,] matrix, float[,] pheromoneMatrix, List<int> locations)
        {
            var probabilities = new List<float>();
            float sum = 0;

            foreach(var location in locations)
            {
                sum += pheromoneMatrix[CurrentLocation, location] * (1F / matrix[CurrentLocation, location]);
            }

            foreach(var location in locations)
            {
                var probability = (pheromoneMatrix[CurrentLocation, location] * (1F / matrix[CurrentLocation, location])) / sum;
                probabilities.Add(probability);
            }

            return probabilities;
        }

        private List<float> CalculateCumulativeSums(List<float> probabilites)
        {
            var sums = new List<float>();

            for(int i = 0; i < probabilites.Count; i++)
            {
                var sum = 0F;
                for(int j = i; j < probabilites.Count; j++)
                {
                    sum += probabilites[j];
                }
                sums.Add(sum);
            }

            return sums;
        }
    }
}
