namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> Texture, tex data stored in GPU memory (VRAM) </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Texture
{
    /// <summary> OpenGL texture id </summary>
    public uint /* unsigned int */ Id;
    /// <summary> Texture base width </summary>
    public int /* int */ Width;
    /// <summary> Texture base height </summary>
    public int /* int */ Height;
    /// <summary> Mipmap levels, 1 by default </summary>
    public int /* int */ Mipmaps;
    /// <summary> Data format (PixelFormat type) </summary>
    public int /* int */ Format;
}

#pragma warning restore CA1711,IDE0005
