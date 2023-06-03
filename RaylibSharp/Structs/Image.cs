namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;

/// <summary> Image, pixel data stored in CPU memory (RAM) </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Image
{
    /// <summary> Image raw data </summary>
    public IntPtr /*void **/ Data;
    /// <summary> Image base width </summary>
    public int /*int*/ Width;
    /// <summary> Image base height </summary>
    public int /*int*/ Height;
    /// <summary> Mipmap levels, 1 by default </summary>
    public int /*int*/ Mipmaps;
    /// <summary> Data format (PixelFormat type) </summary>
    public int /*int*/ Format;
}
#pragma warning restore CA1711,IDE0005

