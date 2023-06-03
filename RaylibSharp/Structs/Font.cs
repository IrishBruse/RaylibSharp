namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;

/// <summary> Font, font texture and GlyphInfo array data </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Font
{
    /// <summary> Base size (default chars height) </summary>
    public int /*int*/ Basesize;
    /// <summary> Number of glyph characters </summary>
    public int /*int*/ Glyphcount;
    /// <summary> Padding around the glyph characters </summary>
    public int /*int*/ Glyphpadding;
    /// <summary> Texture atlas containing the glyphs </summary>
    public Texture2D /*Texture2D*/ Texture;
    /// <summary> Rectangles in texture for the glyphs </summary>
    public IntPtr /*Rectangle **/ Recs;
    /// <summary> Glyphs info data </summary>
    public IntPtr /*GlyphInfo **/ Glyphs;
}
#pragma warning restore CA1711,IDE0005

