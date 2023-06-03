namespace Raylib;

#pragma warning disable CA1711

/// <summary> Pixel formats </summary>
public enum PixelFormat
{
    /// <summary> 8 bit per pixel (no alpha) </summary>
    UncompressedGrayscale = 1,
    /// <summary> 8*2 bpp (2 channels) </summary>
    UncompressedGrayAlpha = 2,
    /// <summary> 16 bpp </summary>
    UncompressedR5g6b5 = 3,
    /// <summary> 24 bpp </summary>
    UncompressedR8g8b8 = 4,
    /// <summary> 16 bpp (1 bit alpha) </summary>
    UncompressedR5g5b5a1 = 5,
    /// <summary> 16 bpp (4 bit alpha) </summary>
    UncompressedR4g4b4a4 = 6,
    /// <summary> 32 bpp </summary>
    UncompressedR8g8b8a8 = 7,
    /// <summary> 32 bpp (1 channel - float) </summary>
    UncompressedR32 = 8,
    /// <summary> 32*3 bpp (3 channels - float) </summary>
    UncompressedR32g32b32 = 9,
    /// <summary> 32*4 bpp (4 channels - float) </summary>
    UncompressedR32g32b32a32 = 10,
    /// <summary> 4 bpp (no alpha) </summary>
    CompressedDxt1Rgb = 11,
    /// <summary> 4 bpp (1 bit alpha) </summary>
    CompressedDxt1Rgba = 12,
    /// <summary> 8 bpp </summary>
    CompressedDxt3Rgba = 13,
    /// <summary> 8 bpp </summary>
    CompressedDxt5Rgba = 14,
    /// <summary> 4 bpp </summary>
    CompressedEtc1Rgb = 15,
    /// <summary> 4 bpp </summary>
    CompressedEtc2Rgb = 16,
    /// <summary> 8 bpp </summary>
    CompressedEtc2EacRgba = 17,
    /// <summary> 4 bpp </summary>
    CompressedPvrtRgb = 18,
    /// <summary> 4 bpp </summary>
    CompressedPvrtRgba = 19,
    /// <summary> 8 bpp </summary>
    CompressedAstc4x4Rgba = 20,
    /// <summary> 2 bpp </summary>
    CompressedAstc8x8Rgba = 21,
}
#pragma warning restore CA1711

