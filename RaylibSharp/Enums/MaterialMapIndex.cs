namespace RaylibSharp;

#pragma warning disable CA1711

/// <summary> Material map index </summary>
public enum MaterialMapIndex
{
    /// <summary> Albedo material (same as: MATERIAL_MAP_DIFFUSE) </summary>
    MaterialMapAlbedo = 0,
    /// <summary> Metalness material (same as: MATERIAL_MAP_SPECULAR) </summary>
    MaterialMapMetalness = 1,
    /// <summary> Normal material </summary>
    MaterialMapNormal = 2,
    /// <summary> Roughness material </summary>
    MaterialMapRoughness = 3,
    /// <summary> Ambient occlusion material </summary>
    MaterialMapOcclusion = 4,
    /// <summary> Emission material </summary>
    MaterialMapEmission = 5,
    /// <summary> Heightmap material </summary>
    MaterialMapHeight = 6,
    /// <summary> Cubemap material (NOTE: Uses GL_TEXTURE_CUBE_MAP) </summary>
    MaterialMapCubemap = 7,
    /// <summary> Irradiance material (NOTE: Uses GL_TEXTURE_CUBE_MAP) </summary>
    MaterialMapIrradiance = 8,
    /// <summary> Prefilter material (NOTE: Uses GL_TEXTURE_CUBE_MAP) </summary>
    MaterialMapPrefilter = 9,
    /// <summary> Brdf material </summary>
    MaterialMapBrdf = 10,
}

#pragma warning restore CA1711
