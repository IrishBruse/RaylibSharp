namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Image, pixel data stored in CPU memory (RAM) </summary>
public unsafe partial struct Image
{
    /// <summary> Image raw data </summary>
    public IntPtr Data;
    /// <summary> Image base width </summary>
    public int Width;
    /// <summary> Image base height </summary>
    public int Height;
    /// <summary> Mipmap levels, 1 by default </summary>
    public int Mipmaps;
    /// <summary> Data format (PixelFormat type) </summary>
    public PixelFormat Format;
}

#pragma warning restore CA1711,IDE0005
