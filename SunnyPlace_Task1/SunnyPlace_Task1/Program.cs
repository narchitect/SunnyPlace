//using System;

//public class BuildingShadeCalculator
//{
//    public static void CalculateShadeFootprint(double buildingHeight, double[] footprintCoordinates, double sunAltitude, double sunAzimuth)
//    {
//        // Convert the sun altitude to radians
//        double sunAltitudeRad = Math.PI * sunAltitude / 180.0;

//        // Calculate the length of the shadow
//        double shadowLength = buildingHeight / Math.Tan(sunAltitudeRad);

//        // Calculate the angle of the shadow
//        double shadowAngle = Math.Atan(footprintCoordinates[3] / shadowLength);

//        // Calculate the length of the shaded area
//        double shadeLength = Math.Sqrt(Math.Pow(footprintCoordinates[3], 2) + Math.Pow(shadowLength, 2));

//        // Calculate the coordinates of the shaded footprint
//        double[] shadedFootprintCoordinates = new double[8];
//        shadedFootprintCoordinates[0] = footprintCoordinates[0] - shadowLength * Math.Sin(sunAzimuth * Math.PI / 180.0) * Math.Cos(shadowAngle);
//        shadedFootprintCoordinates[1] = footprintCoordinates[1] + shadowLength * Math.Cos(sunAzimuth * Math.PI / 180.0) * Math.Cos(shadowAngle);
//        shadedFootprintCoordinates[2] = footprintCoordinates[2] - shadowLength * Math.Sin(sunAzimuth * Math.PI / 180.0) * Math.Sin(shadowAngle);
//        shadedFootprintCoordinates[3] = footprintCoordinates[3] + shadowLength;
//        shadedFootprintCoordinates[4] = footprintCoordinates[4] - shadowLength * Math.Sin(sunAzimuth * Math.PI / 180.0) * Math.Cos(shadowAngle);
//        shadedFootprintCoordinates[5] = footprintCoordinates[5] + shadowLength * Math.Cos(sunAzimuth * Math.PI / 180.0) * Math.Cos(shadowAngle);
//        shadedFootprintCoordinates[6] = footprintCoordinates[6] - shadowLength * Math.Sin(sunAzimuth * Math.PI / 180.0) * Math.Sin(shadowAngle);
//        shadedFootprintCoordinates[7] = footprintCoordinates[7] + shadowLength;

//        Console.WriteLine("Coordinates of shaded footprint: [{0}]", string.Join(", ", shadedFootprintCoordinates));
//    }
//}

//public class Program
//{
//    public static void Main(string[] args)
//    {
//        double buildingHeight = 10.0;
//        double[] footprintCoordinates = new double[] { 0, 0, 10, 0, 10, 10, 0, 10 };
//        double sunAltitude = 45.0;
//        double sunAzimuth = 180.0;

//        BuildingShadeCalculator.CalculateShadeFootprint(buildingHeight, footprintCoordinates, sunAltitude, sunAzimuth);
//    }
//}

using System;
using System.Collections.Generic;
using System.Numerics;

class Program
{
    static void Main(string[] args)
    {
        BuildingShadowCalculator shadowCalculator = new BuildingShadowCalculator();

        List<Vector2> buildingVertices = new List<Vector2>
        {
            new Vector2(10, 10),
            new Vector2(15, 10),
            new Vector2(15, 15),
            new Vector2(12, 18),
            new Vector2(10, 15)
        };
        float buildingHeight = 20; // 20 meters
        DateTime currentTime = DateTime.Now;

        List<Vector2> shadowPositions = shadowCalculator.CalculateShadow(buildingVertices, buildingHeight, currentTime);

        Console.WriteLine("Shadow positions:");
        foreach (Vector2 shadowPosition in shadowPositions)
        {
            Console.WriteLine(shadowPosition);
        }
    }
}


