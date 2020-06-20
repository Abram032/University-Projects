using System;
using System.Collections.Generic;
using System.Text;

namespace OM.Models
{
    public class Ant
    {
        public Ant()
        {
            LatestTrail = new List<Coordinate>();
        }

        public Ant(Coordinate startingPoint)
        {
            StartingPoint = startingPoint;
            LatestTrail = new List<Coordinate>();
        }

        Coordinate StartingPoint { get; set; }
        List<Coordinate> LatestTrail { get; set; }
    }
}
