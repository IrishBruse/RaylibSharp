namespace RaylibSharp;

using System.Numerics;

/// <summary> BoundingBox </summary>
public unsafe partial struct BoundingBox
{
    /// <summary> Create new BoundingBox </summary>
    public BoundingBox(Vector3 min, Vector3 max)
    {
        Min = min;
        Max = max;
    }
}
