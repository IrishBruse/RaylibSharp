namespace Raylib;

#pragma warning disable CA1711

/// <summary> Cubemap layouts </summary>
public enum CubemapLayout
{
    /// <summary> Automatically detect layout type </summary>
    AutoDetect = 0,
    /// <summary> Layout is defined by a vertical line with faces </summary>
    LineVertical = 1,
    /// <summary> Layout is defined by a horizontal line with faces </summary>
    LineHorizontal = 2,
    /// <summary> Layout is defined by a 3x4 cross with cubemap faces </summary>
    CrossThreeByFour = 3,
    /// <summary> Layout is defined by a 4x3 cross with cubemap faces </summary>
    CrossFourByThree = 4,
    /// <summary> Layout is defined by a panorama image (equirrectangular map) </summary>
    Panorama = 5,
}
#pragma warning restore CA1711
