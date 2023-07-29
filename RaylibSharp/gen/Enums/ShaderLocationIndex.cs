namespace RaylibSharp;

#pragma warning disable CA1711

/// <summary> Shader location index </summary>
public enum ShaderLocationIndex
{
    /// <summary> Shader location: vertex attribute: position </summary>
    ShaderLocVertexPosition = 0,
    /// <summary> Shader location: vertex attribute: texcoord01 </summary>
    ShaderLocVertexTexcoord01 = 1,
    /// <summary> Shader location: vertex attribute: texcoord02 </summary>
    ShaderLocVertexTexcoord02 = 2,
    /// <summary> Shader location: vertex attribute: normal </summary>
    ShaderLocVertexNormal = 3,
    /// <summary> Shader location: vertex attribute: tangent </summary>
    ShaderLocVertexTangent = 4,
    /// <summary> Shader location: vertex attribute: color </summary>
    ShaderLocVertexColor = 5,
    /// <summary> Shader location: matrix uniform: model-view-projection </summary>
    ShaderLocMatrixMvp = 6,
    /// <summary> Shader location: matrix uniform: view (camera transform) </summary>
    ShaderLocMatrixView = 7,
    /// <summary> Shader location: matrix uniform: projection </summary>
    ShaderLocMatrixProjection = 8,
    /// <summary> Shader location: matrix uniform: model (transform) </summary>
    ShaderLocMatrixModel = 9,
    /// <summary> Shader location: matrix uniform: normal </summary>
    ShaderLocMatrixNormal = 10,
    /// <summary> Shader location: vector uniform: view </summary>
    ShaderLocVectorView = 11,
    /// <summary> Shader location: vector uniform: diffuse color </summary>
    ShaderLocColorDiffuse = 12,
    /// <summary> Shader location: vector uniform: specular color </summary>
    ShaderLocColorSpecular = 13,
    /// <summary> Shader location: vector uniform: ambient color </summary>
    ShaderLocColorAmbient = 14,
    /// <summary> Shader location: sampler2d texture: albedo (same as: SHADER_LOC_MAP_DIFFUSE) </summary>
    ShaderLocMapAlbedo = 15,
    /// <summary> Shader location: sampler2d texture: metalness (same as: SHADER_LOC_MAP_SPECULAR) </summary>
    ShaderLocMapMetalness = 16,
    /// <summary> Shader location: sampler2d texture: normal </summary>
    ShaderLocMapNormal = 17,
    /// <summary> Shader location: sampler2d texture: roughness </summary>
    ShaderLocMapRoughness = 18,
    /// <summary> Shader location: sampler2d texture: occlusion </summary>
    ShaderLocMapOcclusion = 19,
    /// <summary> Shader location: sampler2d texture: emission </summary>
    ShaderLocMapEmission = 20,
    /// <summary> Shader location: sampler2d texture: height </summary>
    ShaderLocMapHeight = 21,
    /// <summary> Shader location: samplerCube texture: cubemap </summary>
    ShaderLocMapCubemap = 22,
    /// <summary> Shader location: samplerCube texture: irradiance </summary>
    ShaderLocMapIrradiance = 23,
    /// <summary> Shader location: samplerCube texture: prefilter </summary>
    ShaderLocMapPrefilter = 24,
    /// <summary> Shader location: sampler2d texture: brdf </summary>
    ShaderLocMapBrdf = 25,
}

#pragma warning restore CA1711
