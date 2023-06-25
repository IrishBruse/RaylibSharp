namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> RenderTexture, fbo for texture rendering </summary>
public unsafe partial struct RenderTexture
{
    /// <summary> OpenGL framebuffer object id </summary>
    public uint Id;
    /// <summary> Color buffer attachment texture </summary>
    public Texture Texture;
    /// <summary> Depth buffer attachment texture </summary>
    public Texture Depth;
}

/// <summary> RenderTexture, fbo for texture rendering </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct UnmanagedRenderTexture
{
    /// <summary> OpenGL framebuffer object id </summary>
    public uint Id;
    /// <summary> Color buffer attachment texture </summary>
    public UnmanagedTexture Texture;
    /// <summary> Depth buffer attachment texture </summary>
    public UnmanagedTexture Depth;
}

#pragma warning restore CA1711,IDE0005
