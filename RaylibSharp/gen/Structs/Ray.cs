namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Ray, ray for raycasting </summary>
public unsafe partial struct Ray
{
    /// <summary> Ray position (origin) </summary>
    public Vector3 Position;
    /// <summary> Ray direction </summary>
    public Vector3 Direction;
}

/// <summary> Ray, ray for raycasting </summary>
[StructLayout(LayoutKind.Sequential)]
unsafe struct UnmanagedRay
{
    /// <summary> Ray position (origin) </summary>
    public Vector3 Position;
    /// <summary> Ray direction </summary>
    public Vector3 Direction;
}

#pragma warning restore CA1711,IDE0005
