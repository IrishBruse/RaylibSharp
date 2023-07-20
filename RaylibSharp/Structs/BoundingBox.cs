namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> BoundingBox </summary>
public unsafe partial struct BoundingBox
{
    /// <summary> Minimum vertex box-corner </summary>
    public Vector3 Min;
    /// <summary> Maximum vertex box-corner </summary>
    public Vector3 Max;
}

/// <summary> BoundingBox </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct UnmanagedBoundingBox
{
    /// <summary> Minimum vertex box-corner </summary>
    public Vector3 Min;
    /// <summary> Maximum vertex box-corner </summary>
    public Vector3 Max;
}

#pragma warning restore CA1711,IDE0005
