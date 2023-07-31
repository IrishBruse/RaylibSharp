namespace RaylibSharp.GL;

#pragma warning disable CA1711

/// <summary> Framebuffer texture attachment type </summary>
public enum FramebufferAttachTextureType
{
    /// <summary> Framebuffer texture attachment type: cubemap, +X side </summary>
    CubemapPositiveX = 0,
    /// <summary> Framebuffer texture attachment type: cubemap, -X side </summary>
    CubemapNegativeX = 1,
    /// <summary> Framebuffer texture attachment type: cubemap, +Y side </summary>
    CubemapPositiveY = 2,
    /// <summary> Framebuffer texture attachment type: cubemap, -Y side </summary>
    CubemapNegativeY = 3,
    /// <summary> Framebuffer texture attachment type: cubemap, +Z side </summary>
    CubemapPositiveZ = 4,
    /// <summary> Framebuffer texture attachment type: cubemap, -Z side </summary>
    CubemapNegativeZ = 5,
    /// <summary> Framebuffer texture attachment type: texture2d </summary>
    Texture2d = 100,
    /// <summary> Framebuffer texture attachment type: renderbuffer </summary>
    Renderbuffer = 200,
}

#pragma warning restore CA1711
