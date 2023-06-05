namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> RayCollision, ray hit information </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct RayCollision
{
    /// <summary> Did the ray hit something? </summary>
    public bool /* bool */ Hit;
    /// <summary> Distance to the nearest hit </summary>
    public float /* float */ Distance;
    /// <summary> Point of the nearest hit </summary>
    public Vector3 /* Vector3 */ Point;
    /// <summary> Surface normal of hit </summary>
    public Vector3 /* Vector3 */ Normal;
}

#pragma warning restore CA1711,IDE0005
