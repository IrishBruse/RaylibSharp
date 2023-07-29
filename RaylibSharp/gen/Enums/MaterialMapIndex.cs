namespace RaylibSharp;

#pragma warning disable CA1711

/// <summary> Material map index </summary>
public enum MaterialMapIndex
{
    /// <summary> Albedo material (same as: MATERIAL_MAP_DIFFUSE) </summary>
    Albedo = 0,
    /// <summary> Metalness material (same as: MATERIAL_MAP_SPECULAR) </summary>
    Metalness = 1,
    /// <summary> Normal material </summary>
    Normal = 2,
    /// <summary> Roughness material </summary>
    Roughness = 3,
    /// <summary> Ambient occlusion material </summary>
    Occlusion = 4,
    /// <summary> Emission material </summary>
    Emission = 5,
    /// <summary> Heightmap material </summary>
    Height = 6,
    /// <summary> Cubemap material (NOTE: Uses GL_TEXTURE_CUBE_MAP) </summary>
    Cubemap = 7,
    /// <summary> Irradiance material (NOTE: Uses GL_TEXTURE_CUBE_MAP) </summary>
    Irradiance = 8,
    /// <summary> Prefilter material (NOTE: Uses GL_TEXTURE_CUBE_MAP) </summary>
    Prefilter = 9,
    /// <summary> Brdf material </summary>
    Brdf = 10,
}

#pragma warning restore CA1711
