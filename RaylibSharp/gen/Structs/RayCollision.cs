namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> RayCollision, ray hit information </summary>
public unsafe partial struct RayCollision
{
    /// <summary> Did the ray hit something? </summary>
    public bool Hit;
    /// <summary> Distance to the nearest hit </summary>
    public float Distance;
    /// <summary> Point of the nearest hit </summary>
    public Vector3 Point;
    /// <summary> Surface normal of hit </summary>
    public Vector3 Normal;
}

/// <summary> RayCollision, ray hit information </summary>
[StructLayout(LayoutKind.Sequential)]
unsafe struct UnmanagedRayCollision
{
    /// <summary> Did the ray hit something? </summary>
    public bool Hit;
    /// <summary> Distance to the nearest hit </summary>
    public float Distance;
    /// <summary> Point of the nearest hit </summary>
    public Vector3 Point;
    /// <summary> Surface normal of hit </summary>
    public Vector3 Normal;
}

#pragma warning restore CA1711,IDE0005
