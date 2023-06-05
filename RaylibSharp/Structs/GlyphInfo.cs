namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> GlyphInfo, font characters glyphs info </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct GlyphInfo
{
    /// <summary> Character value (Unicode) </summary>
    public int /* int */ Value;
    /// <summary> Character offset X when drawing </summary>
    public int /* int */ Offsetx;
    /// <summary> Character offset Y when drawing </summary>
    public int /* int */ Offsety;
    /// <summary> Character advance position X </summary>
    public int /* int */ Advancex;
    /// <summary> Character image data </summary>
    public Image /* Image */ Image;
}

#pragma warning restore CA1711,IDE0005
