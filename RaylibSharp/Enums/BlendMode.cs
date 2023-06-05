namespace Raylib;

#pragma warning disable CA1711

/// <summary> Color blending modes (pre-defined) </summary>
public enum BlendMode
{
    /// <summary> Blend textures considering alpha (default) </summary>
    BlendAlpha = 0,
    /// <summary> Blend textures adding colors </summary>
    BlendAdditive = 1,
    /// <summary> Blend textures multiplying colors </summary>
    BlendMultiplied = 2,
    /// <summary> Blend textures adding colors (alternative) </summary>
    BlendAddColors = 3,
    /// <summary> Blend textures subtracting colors (alternative) </summary>
    BlendSubtractColors = 4,
    /// <summary> Blend premultiplied textures considering alpha </summary>
    BlendAlphaPremultiply = 5,
    /// <summary> Blend textures using custom src/dst factors (use rlSetBlendFactors()) </summary>
    BlendCustom = 6,
    /// <summary> Blend textures using custom rgb/alpha separate src/dst factors (use rlSetBlendFactorsSeparate()) </summary>
    BlendCustomSeparate = 7,
}
#pragma warning restore CA1711
