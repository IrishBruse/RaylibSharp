namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;

/// <summary> Ray, ray for raycasting </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Ray
{
    /// <summary> Ray position (origin) </summary>
    public Vector3 /*Vector3*/ Position;
    /// <summary> Ray direction </summary>
    public Vector3 /*Vector3*/ Direction;
}
#pragma warning restore CA1711,IDE0005

