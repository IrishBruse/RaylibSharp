namespace RaylibSharp.GL;

#pragma warning disable CA1711

/// <summary> OpenGL version </summary>
public enum GlVersion
{
    /// <summary> OpenGL 1.1 </summary>
    Opengl11 = 1,
    /// <summary> OpenGL 2.1 (GLSL 120) </summary>
    Opengl21 = 2,
    /// <summary> OpenGL 3.3 (GLSL 330) </summary>
    Opengl33 = 3,
    /// <summary> OpenGL 4.3 (using GLSL 330) </summary>
    Opengl43 = 4,
    /// <summary> OpenGL ES 2.0 (GLSL 100) </summary>
    OpenglEs20 = 5,
    /// <summary> OpenGL ES 3.0 (GLSL 300 es) </summary>
    OpenglEs30 = 6,
}

#pragma warning restore CA1711
