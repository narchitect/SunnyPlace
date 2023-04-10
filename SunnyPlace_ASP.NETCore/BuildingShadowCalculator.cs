using System;
using System.Collections.Generic;
using System.Numerics;
using SunCalcSharp;

namespace SunnyPlace_ASP.NETCore.Models
{
    public class BuildingShadowCalculator
    {
        public List<Vector2> CalculateShadow(List<Vector2> buildingVertices, float buildingHeight, DateTime time)
        {
            // Replace these with the actual latitude and longitude of the building
            double latitude = 40.7128;
            double longitude = -74.0060;

            // Get the sun position using the SunCalcNet library
            var sunPosition = SunCalc.GetPosition(time, latitude, longitude);

            // Calculate the shadow length using the building's height and sun elevation
            double shadowLength = buildingHeight / Math.Tan(sunPosition.Altitude);

            // Calculate the shadow offset using the shadow length and sun azimuth
            Vector2 shadowOffset = new Vector2(
                (float)(shadowLength * Math.Sin(sunPosition.Azimuth)),
                (float)(shadowLength * Math.Cos(sunPosition.Azimuth))
            );

            // Calculate the shadow positions for each vertex by adding the shadow offset to the vertex position
            List<Vector2> shadowPositions = new List<Vector2>();
            foreach (Vector2 vertex in buildingVertices)
            {
                shadowPositions.Add(vertex + shadowOffset);
            }

            return shadowPositions;
        }
    }
}

