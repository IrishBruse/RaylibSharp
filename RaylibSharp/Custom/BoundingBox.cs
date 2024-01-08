namespace RaylibSharp;

using System.Numerics;

#pragma warning disable IDE0290

/// <summary> BoundingBox </summary>
/// <remarks> Create new BoundingBox </remarks>
public unsafe partial struct BoundingBox
{
    /// <summary> Bounding Box Constructor </summary>
    public BoundingBox(Vector3 min, Vector3 max)
    {
        Min = min;
        Max = max;
    }
}

#pragma warning restore IDE0290
