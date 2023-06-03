namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;

/// <summary> Camera2D, defines position/orientation in 2d space </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Camera2D
{
    /// <summary> Camera offset (displacement from target) </summary>
    public Vector2 /*Vector2*/ Offset;
    /// <summary> Camera target (rotation and zoom origin) </summary>
    public Vector2 /*Vector2*/ Target;
    /// <summary> Camera rotation in degrees </summary>
    public float /*float*/ Rotation;
    /// <summary> Camera zoom (scaling), should be 1.0f by default </summary>
    public float /*float*/ Zoom;
}
#pragma warning restore CA1711,IDE0005

