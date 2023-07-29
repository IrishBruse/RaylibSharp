namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Material, includes shader and maps </summary>
[NativeMarshalling(typeof(MaterialMarshaller))]
public unsafe partial struct Material
{
    /// <summary> Material shader </summary>
    public Shader Shader;
    /// <summary> Material maps array (MAX_MATERIAL_MAPS) </summary>
    public MaterialMap[] Maps;
    /// <summary> Material generic parameters (if required) </summary>
    public Vector4 Params;
}

/// <summary> Material, includes shader and maps </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct UnmanagedMaterial
{
    /// <summary> Material shader </summary>
    public UnmanagedShader Shader;
    /// <summary> Material maps array (MAX_MATERIAL_MAPS) </summary>
    public UnmanagedMaterialMap* Maps;
    /// <summary> Material generic parameters (if required) </summary>
    public fixed float Params[4];
}

#pragma warning restore CA1711,IDE0005
