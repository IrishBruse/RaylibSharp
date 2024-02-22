namespace RaylibSharp;

using System.Runtime.InteropServices;

/// <summary> Material, includes shader and maps </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Material
{
    /// <summary> Material shader </summary>
    public Shader Shader;
    /// <summary> Material maps array (MAX_MATERIAL_MAPS) </summary>
    public MaterialMap* maps;
    /// <summary> Material generic parameters (if required) </summary>
    public fixed float Params[4];

    /// <inheritdoc cref="maps"/>
    public readonly Span<MaterialMap> Maps => new(maps, Raylib.MAX_MATERIAL_MAPS);
}
