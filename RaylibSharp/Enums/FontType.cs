namespace Raylib;

#pragma warning disable CA1711

/// <summary> Font type, defines generation method </summary>
public enum FontType
{
    /// <summary> Default font generation, anti-aliased </summary>
    FontDefault = 0,
    /// <summary> Bitmap font generation, no anti-aliasing </summary>
    FontBitmap = 1,
    /// <summary> SDF font generation, requires external shader </summary>
    FontSdf = 2,
}
#pragma warning restore CA1711
