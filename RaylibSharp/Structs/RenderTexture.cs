namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> RenderTexture, fbo for texture rendering </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct RenderTexture
{
    /// <summary> OpenGL framebuffer object id </summary>
    public uint Id;
    /// <summary> Color buffer attachment texture </summary>
    public Texture Texture;
    /// <summary> Depth buffer attachment texture </summary>
    public Texture Depth;
}

#pragma warning restore CA1711,IDE0005
