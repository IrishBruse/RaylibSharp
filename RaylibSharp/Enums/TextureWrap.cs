namespace Raylib;

#pragma warning disable CA1711

/// <summary> Texture parameters: wrap mode </summary>
public enum TextureWrap
{
    /// <summary> Repeats texture in tiled mode </summary>
    Repeat = 0,
    /// <summary> Clamps texture to edge pixel in tiled mode </summary>
    Clamp = 1,
    /// <summary> Mirrors and repeats the texture in tiled mode </summary>
    MirrorRepeat = 2,
    /// <summary> Mirrors and clamps to border the texture in tiled mode </summary>
    MirrorClamp = 3,
}
#pragma warning restore CA1711

