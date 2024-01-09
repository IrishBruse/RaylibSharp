namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> MaterialMap </summary>
public unsafe partial struct MaterialMap
{
    /// <summary> Material map texture </summary>
    public Texture Texture;
    /// <summary> Material map color </summary>
    public Color Color;
    /// <summary> Material map value </summary>
    public float Value;
}

/// <summary> MaterialMap </summary>
[StructLayout(LayoutKind.Sequential)]
unsafe struct UnmanagedMaterialMap
{
    /// <summary> Material map texture </summary>
    public Texture Texture;
    /// <summary> Material map color </summary>
    public uint Color;
    /// <summary> Material map value </summary>
    public float Value;
}

#pragma warning restore CA1711,IDE0005
