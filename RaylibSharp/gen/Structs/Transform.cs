namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Transform, vertex transformation data </summary>
public unsafe partial struct Transform
{
    /// <summary> Translation </summary>
    public Vector3 Translation;
    /// <summary> Rotation </summary>
    public Quaternion Rotation;
    /// <summary> Scale </summary>
    public Vector3 Scale;
}

/// <summary> Transform, vertex transformation data </summary>
[StructLayout(LayoutKind.Sequential)]
unsafe struct UnmanagedTransform
{
    /// <summary> Translation </summary>
    public Vector3 Translation;
    /// <summary> Rotation </summary>
    public Quaternion Rotation;
    /// <summary> Scale </summary>
    public Vector3 Scale;
}

#pragma warning restore CA1711,IDE0005
