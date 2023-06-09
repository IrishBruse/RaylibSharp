namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> Font, font texture and GlyphInfo array data </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Font
{
    /// <summary> Base size (default chars height) </summary>
    public int Basesize;
    /// <summary> Number of glyph characters </summary>
    public int Glyphcount;
    /// <summary> Padding around the glyph characters </summary>
    public int Glyphpadding;
    /// <summary> Texture atlas containing the glyphs </summary>
    public Texture Texture;
    /// <summary> Rectangles in texture for the glyphs </summary>
    public IntPtr Recs;
    /// <summary> Glyphs info data </summary>
    public IntPtr Glyphs;
}

#pragma warning restore CA1711,IDE0005
