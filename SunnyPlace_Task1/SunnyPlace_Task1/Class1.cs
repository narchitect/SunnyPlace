using System;
using System.Numerics;

class ShadowCalculator
{
    // Calculates the shadow of a building given its footprint and height
    // and the position of the sun
    public static Vector2[] Calculate(Vector2[] footprint, float height, Vector3 sunPosition)
    {
        // Calculate the normal vector of the plane containing the building footprint
        var planeNormal = GetPlaneNormal(footprint);

        // Calculate the vector from the sun to the building
        var toBuilding = GetToBuildingVector(height, footprint, sunPosition);

        // Calculate the angle between the sun vector and the plane normal
        var angle = Math.Acos(Vector3.Dot(planeNormal, toBuilding) / (planeNormal.Length() * toBuilding.Length()));

        // Calculate the length of the shadow
        var shadowLength = height / Math.Tan(angle);

        // Project the building footprint onto a plane perpendicular to the sun vector
        var projectionPlane = GetProjectionPlane(toBuilding, planeNormal, sunPosition);
        var projectedFootprint = ProjectOntoPlane(footprint, projectionPlane);

        // Calculate the shadow footprint
        var shadowFootprint = GetShadowFootprint(projectedFootprint, shadowLength, sunPosition);

        return shadowFootprint;
    }

    // Calculates the normal vector of a plane containing a given set of points
    private static Vector3 GetPlaneNormal(Vector2[] points)
    {
        var v1 = new Vector3(points[1] - points[0], 0);
        var v2 = new Vector3(points[2] - points[0], 0);
        var planeNormal = Normalize(Vector3.Cross(v1, v2));
        return planeNormal;

    }

    // Calculates the vector from the sun to the building
    private static Vector3 GetToBuildingVector(float height, Vector2[] footprint, Vector3 sunPosition)
    {
        var centroid = GetCentroid(footprint);
        var toCentroid = new Vector3(centroid.X - sunPosition.X, centroid.Y - sunPosition.Y, height - sunPosition.Z);
        var vectorNomal = Normalize(toCentroid);
        return vectorNomal;

    }

    // Calculates the plane onto which the building footprint will be projected
    private static Vector4 GetProjectionPlane(Vector3 toBuilding, Vector3 planeNormal, Vector3 sunPosition)
    {
        var d = -Vector3.Dot(toBuilding, sunPosition);
        return new Vector4(planeNormal, d);
    }

    // Projects a set of 2D points onto a plane defined by its normal vector and distance from origin
    private static Vector2[] ProjectOntoPlane(Vector2[] points, Vector4 plane)
    {
        var projectedPoints = new Vector2[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            var point = new Vector3(points[i], 0);
            var distance = Vector3.Dot(new Vector4(point, 1), plane) / plane.Length();
            projectedPoints[i] = new Vector2(point - distance * plane.Xyz);
        }
        return projectedPoints;
    }

    // Calculates the shadow footprint by extruding the projected building footprint
    private static Vector2[] GetShadowFootprint(Vector2[] projectedFootprint, float shadowLength, Vector3 sunPosition)
    {
        var shadowFootprint = new Vector2[projectedFootprint.Length];
        for (int i = 0; i < projectedFootprint.Length; i++)
        {
            var point = new Vector3(projectedFootprint[i], 0);
            var shadowPoint = sunPosition + shadowLength * (point - sunPosition);
            shadowFootprint[i] = new Vector2(shadowPoint.X, shadowPoint.Y);
        }
        return shadowFootprint;
    }

    // Calculates the centroid of a set of 2D points
    private static Vector2 GetCentroid(Vector2[] points)
    {
        var centroid = new Vector2();
        for (int i = 0; i < points.Length; i++)
        {
            centroid += points[i];
        }
        return centroid / points.Length;
    }
    // Normalize a vector
    public static Vector3 Normalize(Vector3 v)
    {
        var length = Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        return new Vector3(v.X / length, v.Y / length, v.Z / length);
    }
}