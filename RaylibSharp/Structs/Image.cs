namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> Image, pixel data stored in CPU memory (RAM) </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Image
{
    /// <summary> Image raw data </summary>
    public void* Data;
    /// <summary> Image base width </summary>
    public int Width;
    /// <summary> Image base height </summary>
    public int Height;
    /// <summary> Mipmap levels, 1 by default </summary>
    public int Mipmaps;
    /// <summary> Data format (PixelFormat type) </summary>
    public int Format;
}

#pragma warning restore CA1711,IDE0005
