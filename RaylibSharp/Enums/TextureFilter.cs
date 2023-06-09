namespace RaylibSharp;

#pragma warning disable CA1711

/// <summary> Texture parameters: filter mode </summary>
public enum TextureFilter
{
    /// <summary> No filter, just pixel approximation </summary>
    Point = 0,
    /// <summary> Linear filtering </summary>
    Bilinear = 1,
    /// <summary> Trilinear filtering (linear with mipmaps) </summary>
    Trilinear = 2,
    /// <summary> Anisotropic filtering 4x </summary>
    Anisotropic4x = 3,
    /// <summary> Anisotropic filtering 8x </summary>
    Anisotropic8x = 4,
    /// <summary> Anisotropic filtering 16x </summary>
    Anisotropic16x = 5,
}
#pragma warning restore CA1711
