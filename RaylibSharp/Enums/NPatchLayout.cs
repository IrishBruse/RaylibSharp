namespace Raylib;

#pragma warning disable CA1711

/// <summary> N-patch layout </summary>
public enum NPatchLayout
{
    /// <summary> Npatch layout: 3x3 tiles </summary>
    NpatchNinePatch = 0,
    /// <summary> Npatch layout: 1x3 tiles </summary>
    NpatchThreePatchVertical = 1,
    /// <summary> Npatch layout: 3x1 tiles </summary>
    NpatchThreePatchHorizontal = 2,
}
#pragma warning restore CA1711
