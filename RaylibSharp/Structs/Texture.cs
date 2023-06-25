namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Texture, tex data stored in GPU memory (VRAM) </summary>
public unsafe partial struct Texture
{
    /// <summary> OpenGL texture id </summary>
    public uint Id;
    /// <summary> Texture base width </summary>
    public int Width;
    /// <summary> Texture base height </summary>
    public int Height;
    /// <summary> Mipmap levels, 1 by default </summary>
    public int Mipmaps;
    /// <summary> Data format (PixelFormat type) </summary>
    public int Format;
}

/// <summary> Texture, tex data stored in GPU memory (VRAM) </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct UnmanagedTexture
{
    /// <summary> OpenGL texture id </summary>
    public uint Id;
    /// <summary> Texture base width </summary>
    public int Width;
    /// <summary> Texture base height </summary>
    public int Height;
    /// <summary> Mipmap levels, 1 by default </summary>
    public int Mipmaps;
    /// <summary> Data format (PixelFormat type) </summary>
    public int Format;
}

#pragma warning restore CA1711,IDE0005
