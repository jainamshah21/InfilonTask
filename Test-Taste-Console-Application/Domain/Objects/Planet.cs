using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Test_Taste_Console_Application.Domain.DataTransferObjects;

namespace Test_Taste_Console_Application.Domain.Objects
{
    public class Planet
    {
        public string Id { get; set; }
        public float SemiMajorAxis { get; set; }
        public ICollection<Moon> Moons { get; set; }

        // changed the only get method to get set both
        private float _averageMoonGravity = 0.0f;
        public float AverageMoonGravity
        //{
        //    get => 0.0f;
        //}
        {
            get { return _averageMoonGravity; }
            set { _averageMoonGravity = value; }
        }

        public Planet(PlanetDto planetDto)
        {
            Console.WriteLine("called the planet constructor");
            Id = planetDto.Id;
            SemiMajorAxis = planetDto.SemiMajorAxis;
            Moons = new Collection<Moon>();
            if (planetDto.Moons != null)
            {
                foreach (MoonDto moonDto in planetDto.Moons)
                {
                    Moons.Add(new Moon(moonDto));
                }
            }
            
            AverageMoonGravity = GetAverageMoonGravity();
            Console.WriteLine("Set the Average Moon gravity of plate "+ planetDto .Id+ " from method");
        }

        public Boolean HasMoons()
        {
            return (Moons != null && Moons.Count > 0);
        }

        // Method no 2 for short solution
        // in this applied select instend of foreach and applied ternary for code optimize
        private float GetAverageMoonGravity()
        {
            List<float> gvity = Moons.Select(moon => (moon.MassExponent == 0 && moon.MassValue == 0)
                    ? 0.0f
                    : (float)((6.67430 * Math.Pow(10, -11) * moon.MassValue * Math.Pow(10, moon.MassExponent))
                    / (moon.MeanRadius * moon.MeanRadius * 1000000))).ToList();

            return gvity.Count > 0 ? gvity.Average() : 0;
        }


        // method no 1 for with datatype
        private float GetAverageAllMoonGravity()
        {
            float mass = 0;
            float radiusInMeters = 0;
            float G = (float)(6.67430 * Math.Pow(10, -11));
            List<float> gvity = new List<float>();
            foreach (Moon moon in Moons)
            {
                if (moon.MassExponent == 0 && moon.MassValue == 0)
                {
                    gvity.Add(0.0f);
                }
                else
                {
                    mass = moon.MassValue * (float)Math.Pow(10, moon.MassExponent);
                    radiusInMeters = moon.MeanRadius * 1000;
                    float gravity = (G * mass) / (radiusInMeters * radiusInMeters);
                    gvity.Add(gravity);
                }
            }
            return gvity.Count > 0 ? gvity.Average() : 0;
        }

//        Exercise 2
//          Propose in less than 5 lines an alternative solution to this problem (if possible) and explain a benefit and
//          a drawback versus the solution that you have chosen. 

//          in this case 5 line short solution in better for exucation but for better understanding that normal solution is good
    }
}
