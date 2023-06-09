namespace RaylibSharp;

#pragma warning disable CA1711

/// <summary> Shader attribute data types </summary>
public enum ShaderAttributeDataType
{
    /// <summary> Shader attribute type: float </summary>
    ShaderAttribFloat = 0,
    /// <summary> Shader attribute type: vec2 (2 float) </summary>
    ShaderAttribVec2 = 1,
    /// <summary> Shader attribute type: vec3 (3 float) </summary>
    ShaderAttribVec3 = 2,
    /// <summary> Shader attribute type: vec4 (4 float) </summary>
    ShaderAttribVec4 = 3,
}
#pragma warning restore CA1711
