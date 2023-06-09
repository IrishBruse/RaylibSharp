namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> Ray, ray for raycasting </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Ray
{
    /// <summary> Ray position (origin) </summary>
    public Vector3 Position;
    /// <summary> Ray direction </summary>
    public Vector3 Direction;
}

#pragma warning restore CA1711,IDE0005
