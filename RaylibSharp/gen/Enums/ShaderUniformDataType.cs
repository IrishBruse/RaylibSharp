namespace RaylibSharp;

#pragma warning disable CA1711

/// <summary> Shader uniform data type </summary>
public enum ShaderUniformDataType
{
    /// <summary> Shader uniform type: float </summary>
    ShaderUniformFloat = 0,
    /// <summary> Shader uniform type: vec2 (2 float) </summary>
    ShaderUniformVec2 = 1,
    /// <summary> Shader uniform type: vec3 (3 float) </summary>
    ShaderUniformVec3 = 2,
    /// <summary> Shader uniform type: vec4 (4 float) </summary>
    ShaderUniformVec4 = 3,
    /// <summary> Shader uniform type: int </summary>
    ShaderUniformInt = 4,
    /// <summary> Shader uniform type: ivec2 (2 int) </summary>
    ShaderUniformIvec2 = 5,
    /// <summary> Shader uniform type: ivec3 (3 int) </summary>
    ShaderUniformIvec3 = 6,
    /// <summary> Shader uniform type: ivec4 (4 int) </summary>
    ShaderUniformIvec4 = 7,
    /// <summary> Shader uniform type: sampler2d </summary>
    ShaderUniformSampler2d = 8,
}

#pragma warning restore CA1711
