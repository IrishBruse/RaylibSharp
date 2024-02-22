namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051,IDE1006,CA1708
/// <summary> Shader </summary>
public unsafe partial struct Shader
{
    /// <summary> Shader program id </summary>
    public uint Id;
    /// <summary> Shader locations array (RL_MAX_SHADER_LOCATIONS) </summary>
    public int* locs;
    /// <inheritdoc cref="locs"/>
    public readonly Span<int> Locs => new(locs, Raylib.RL_MAX_SHADER_LOCATIONS);
}
#pragma warning restore CA1711,IDE0005,CA1051,IDE1006,CA1708
