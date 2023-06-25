namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Font, font texture and GlyphInfo array data </summary>
[NativeMarshalling(typeof(FontMarshaller))]
public unsafe partial struct Font
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
    public RectangleF[] Recs;
    /// <summary> Glyphs info data </summary>
    public GlyphInfo[] Glyphs;
}

/// <summary> Font, font texture and GlyphInfo array data </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct UnmanagedFont
{
    /// <summary> Base size (default chars height) </summary>
    public int Basesize;
    /// <summary> Number of glyph characters </summary>
    public int Glyphcount;
    /// <summary> Padding around the glyph characters </summary>
    public int Glyphpadding;
    /// <summary> Texture atlas containing the glyphs </summary>
    public UnmanagedTexture Texture;
    /// <summary> Rectangles in texture for the glyphs </summary>
    public RectangleF* Recs;
    /// <summary> Glyphs info data </summary>
    public UnmanagedGlyphInfo* Glyphs;
}

#pragma warning restore CA1711,IDE0005
