namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> Transform, vertex transformation data </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Transform
{
    /// <summary> Translation </summary>
    public Vector3 Translation;
    /// <summary> Rotation </summary>
    public Quaternion Rotation;
    /// <summary> Scale </summary>
    public Vector3 Scale;
}

#pragma warning restore CA1711,IDE0005
