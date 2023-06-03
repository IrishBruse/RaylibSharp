namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;

/// <summary> BoundingBox </summary>
[StructLayout(LayoutKind.Sequential)]
public struct BoundingBox
{
    /// <summary> Minimum vertex box-corner </summary>
    public Vector3 /*Vector3*/ Min;
    /// <summary> Maximum vertex box-corner </summary>
    public Vector3 /*Vector3*/ Max;
}
#pragma warning restore CA1711,IDE0005

