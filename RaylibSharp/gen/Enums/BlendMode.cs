namespace RaylibSharp;

#pragma warning disable CA1711

/// <summary> Color blending modes (pre-defined) </summary>
public enum BlendMode
{
    /// <summary> Blend textures considering alpha (default) </summary>
    Alpha = 0,
    /// <summary> Blend textures adding colors </summary>
    Additive = 1,
    /// <summary> Blend textures multiplying colors </summary>
    Multiplied = 2,
    /// <summary> Blend textures adding colors (alternative) </summary>
    AddColors = 3,
    /// <summary> Blend textures subtracting colors (alternative) </summary>
    SubtractColors = 4,
    /// <summary> Blend premultiplied textures considering alpha </summary>
    AlphaPremultiply = 5,
    /// <summary> Blend textures using custom src/dst factors (use rlSetBlendFactors()) </summary>
    Custom = 6,
    /// <summary> Blend textures using custom rgb/alpha separate src/dst factors (use rlSetBlendFactorsSeparate()) </summary>
    CustomSeparate = 7,
}

#pragma warning restore CA1711
