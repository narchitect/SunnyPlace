using System;
using System.Drawing;
using System.Numerics;
using Nager.Date;
using SunCalcSharp;

// Define the location and time of observation
double latitude = 37.7749; // San Francisco
double longitude = -122.4194;
DateTime date = DateTime.UtcNow.Date; // today
TimeSpan time = new TimeSpan(12, 0, 0); // noon

// Calculate the sun's altitude and azimuth using SunCalcSharp
var sunPosition = SunCalc.GetPosition(date, latitude, longitude);
double altitude = sunPosition.Altitude;
double azimuth = sunPosition.Azimuth;

public class ShadowCalculator
{
    // Calculates the shadow of a rectangular building
    // Given the footprint of the building (an array of Vector2 points)
    // The height of the building (in meters)
    // The geographic location of the building
    // The date and time to calculate the shadow
    public static Vector2[] CalculateBuildingShadow(Vector2[] footprint, float height, double latitude, double longitude, DateTime date)
    {
        // Calculate the position of the sun
        var sunPosition = SunCalc.GetPosition(date, latitude, longitude);
        double altitude = sunPosition.Altitude;
        double azimuth = sunPosition.Azimuth;

        var sunAltitude = altitude;
        var sunAzimuth = azimuth;

        Console.WriteLine($"Sun altitude: {sunAltitude:F2} degrees");
        Console.WriteLine($"Sun azimuth: {sunAzimuth:F2} degrees");

        // Convert the altitude and azimuth angles to a 3D vector representing the direction of the sun's rays
        var sunDirection = new Vector3(
            (float)Math.Sin(sunPosition.Altitude),
            (float)Math.Cos(sunPosition.Altitude) * (float)Math.Cos(sunPosition.Azimuth),
            (float)Math.Cos(sunPosition.Altitude) * (float)Math.Sin(sunPosition.Azimuth));

        // Calculate the shadow of the building
        var shadow = new Vector2[footprint.Length];
        for (int i = 0; i < footprint.Length; i++)
        {
            var point1 = footprint[i];
            var point2 = footprint[(i + 1) % footprint.Length];

            // Calculate the direction of the sunlight hitting the face of the building
            var faceDirection = new Vector3(point1.X - point2.X, point1.Y - point2.Y, height).normalized();

            // Calculate the point where the sunlight hits the face of the building
            var intersection = IntersectLinePlane(point1, faceDirection, Vector2.zero, sunDirection);

            // Calculate the length of the shadow
            var shadowLength = (intersection - Vector2.zero).magnitude;

            // Project the shadow onto the ground plane
            var shadowVector = Vector2.Project(intersection, sunDirection);

            // Add the projected shadow to the list
            shadow[i] = shadowVector;
        }

        return shadow;
    }

    // Calculates the intersection point between a line and a plane
    // Given a point on the line, the direction of the line, a point on the plane, and the normal of the plane
    private static Vector3 IntersectLinePlane(Vector2 linePoint, Vector3 lineDirection, Vector2 planePoint, Vector3 planeNormal)
    {
        float t = (planeNormal.x * planePoint.x + planeNormal.y * planePoint.y + planeNormal.z * 0 - planeNormal.x * linePoint.x - planeNormal.y * linePoint.y - planeNormal.z * linePoint.z) / (planeNormal.x * lineDirection.x + planeNormal.y * lineDirection.y + planeNormal.z * lineDirection.z);
        return new Vector3(linePoint.x + lineDirection.x * t, linePoint.y + lineDirection.y * t, 0);
    }
}

public class Vector2
{
    public float X { get; set; }
    public float Y { get; set; }

    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public static Vector2 zero
    {
        get { return new Vector2(0, 0); }
    }

    private static Vector3 CalculateShadowDirection(Vector3 sunPosition, Vector3 buildingPosition)
    {
        return (sunPosition - buildingPosition).Normalize();
    }

    private static float CalculateShadowLength(Vector3 shadowDirection, Building building)
    {
        var maxHeightPoint = new Point { X = 0, Y = 0, Z = building.Height };
        var shadowPlane = new Plane(maxHeightPoint, building.Footprint);

        var intersection = shadowPlane.Raycast(new Ray(maxHeightPoint, shadowDirection));
        if (intersection == null)
        {
            return 0;
        }

        return (float)intersection;
    }

    
 
}
