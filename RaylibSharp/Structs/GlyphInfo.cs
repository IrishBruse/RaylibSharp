namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> GlyphInfo, font characters glyphs info </summary>
public unsafe partial struct GlyphInfo
{
    /// <summary> Character value (Unicode) </summary>
    public int Value;
    /// <summary> Character offset X when drawing </summary>
    public int Offsetx;
    /// <summary> Character offset Y when drawing </summary>
    public int Offsety;
    /// <summary> Character advance position X </summary>
    public int Advancex;
    /// <summary> Character image data </summary>
    public Image Image;
}

#pragma warning restore CA1711,IDE0005
