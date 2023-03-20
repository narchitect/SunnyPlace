using System;
using System.Collections.Generic;
using System.Linq;
using SunCalcSharp;

using System;
using System.Collections.Generic;
using SunCalcSharp;
using BuildingShadow;

namespace BuildingShadeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define the building footprint
            List<double[]> footprint = new List<double[]>();
            footprint.Add(new double[] { 0, 0 });
            footprint.Add(new double[] { 10, 0 });
            footprint.Add(new double[] { 10, 20 });
            footprint.Add(new double[] { 0, 20 });

            // Define the height of the building
            double buildingHeight = 50;

            // Define the observation location
            double latitude = 52.5200;
            double longitude = 13.4050;

            // Define the time (optional)
            DateTime dateTime = DateTime.UtcNow;

            // Calculate the sun position
            SunPosition sunPosition = SunCalc.GetPosition(dateTime, latitude, longitude);

            // Define the direction of the sun
            double sunAltitude = sunPosition.Altitude;
            double sunAzimuth = sunPosition.Azimuth;

            // Create a list to store the coordinates of the shadow
            List<double[]> shadowCoordinates = new List<double[]>();

            // Loop through each edge of the building footprint
            for (int i = 0; i < footprint.Count; i++)
            {
                // Get the two vertices that make up the edge
                double[] vertex1 = footprint[i];
                double[] vertex2 = footprint[(i + 1) % footprint.Count];

                // Create a ray from the vertex towards the sun
                Ray ray = new Ray(new Vector(vertex1[0], vertex1[1], 0), new Vector(sunAltitude, sunAzimuth));

                // Find the intersection point between the ray and the ground plane
                Vector intersection = GetIntersection(ray);

                // If the intersection point is above the height of the building, 
                // add the original vertex to the list of shadow coordinates
                if (intersection.Z >= buildingHeight)
                {
                    shadowCoordinates.Add(vertex1);
                }
                else
                {
                    // Otherwise, add the intersection point to the list of shadow coordinates
                    shadowCoordinates.Add(new double[] { intersection.X, intersection.Y });
                }
            }

            // Print the coordinates of the shadow
            Console.WriteLine("Shadow Coordinates:");
            foreach (double[] coordinate in shadowCoordinates)
            {
                Console.WriteLine("(" + coordinate[0] + ", " + coordinate[1] + ")");
            }
        }

        // Define the plane of the ground as a constant
        private const double GroundPlaneZ = 0;

        // Find the intersection point between a ray and the ground plane
        private static Vector GetIntersection(Ray ray)
        {
            double t = -ray.Origin.Z / ray.Direction.Z;
            double x = ray.Origin.X + t * ray.Direction.X;
            double y = ray.Origin.Y + t * ray.Direction.Y;
            return new Vector(x, y, GroundPlaneZ);
        }
    }

    // Define a class to represent a 3D vector
    public class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector Normalized()
        {
            double magnitude = Math.Sqrt(X * X + Y * Y + Z * Z);
            return new Vector(X / magnitude, Y / magnitude, Z / magnitude);
        }
    }

