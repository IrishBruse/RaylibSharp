namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> Material, includes shader and maps </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Material
{
    /// <summary> Material shader </summary>
    public Shader Shader;
    /// <summary> Material maps array (MAX_MATERIAL_MAPS) </summary>
    public IntPtr Maps;
    /// <summary> Material generic parameters (if required) </summary>
    public Vector4 Params;
}

#pragma warning restore CA1711,IDE0005
