namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> Shader </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Shader
{
    /// <summary> Shader program id </summary>
    public uint Id;
    /// <summary> Shader locations array (RL_MAX_SHADER_LOCATIONS) </summary>
    public IntPtr Locs;
}

#pragma warning restore CA1711,IDE0005
