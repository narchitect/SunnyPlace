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


