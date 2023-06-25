namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Shader </summary>
[NativeMarshalling(typeof(ShaderMarshaller))]
public unsafe partial struct Shader
{
    /// <summary> Shader program id </summary>
    public uint Id;
    /// <summary> Shader locations array (RL_MAX_SHADER_LOCATIONS) </summary>
    public int[] Locs;
}

/// <summary> Shader </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct UnmanagedShader
{
    /// <summary> Shader program id </summary>
    public uint Id;
    /// <summary> Shader locations array (RL_MAX_SHADER_LOCATIONS) </summary>
    public int* Locs;
}

#pragma warning restore CA1711,IDE0005
