namespace RaylibSharp.GL;

public static unsafe partial class RLGL
{
    /// <summary> RlglVersion </summary>
    public static readonly string RlglVersion = "4.5";
    /// <summary> RlDefaultBatchBufferElements </summary>
    public static readonly int RlDefaultBatchBufferElements = 8192;
    /// <summary> Default number of batch buffers (multi-buffering) </summary>
    public static readonly int RlDefaultBatchBuffers = 1;
    /// <summary> Default number of batch draw calls (by state changes: mode, texture) </summary>
    public static readonly int RlDefaultBatchDrawcalls = 256;
    /// <summary> Maximum number of textures units that can be activated on batch drawing (SetShaderValueTexture()) </summary>
    public static readonly int RlDefaultBatchMaxTextureUnits = 4;
    /// <summary> Maximum size of Matrix stack </summary>
    public static readonly int RlMaxMatrixStackSize = 32;
    /// <summary> Maximum number of shader locations supported </summary>
    public static readonly int RlMaxShaderLocations = 32;
    /// <summary> Default near cull distance </summary>
    public static readonly double RlCullDistanceNear = 0.01;
    /// <summary> Default far cull distance </summary>
    public static readonly double RlCullDistanceFar = 1000.0;
    /// <summary> GL_TEXTURE_WRAP_S </summary>
    public static readonly int RlTextureWrapS = 10242;
    /// <summary> GL_TEXTURE_WRAP_T </summary>
    public static readonly int RlTextureWrapT = 10243;
    /// <summary> GL_TEXTURE_MAG_FILTER </summary>
    public static readonly int RlTextureMagFilter = 10240;
    /// <summary> GL_TEXTURE_MIN_FILTER </summary>
    public static readonly int RlTextureMinFilter = 10241;
    /// <summary> GL_NEAREST </summary>
    public static readonly int RlTextureFilterNearest = 9728;
    /// <summary> GL_LINEAR </summary>
    public static readonly int RlTextureFilterLinear = 9729;
    /// <summary> GL_NEAREST_MIPMAP_NEAREST </summary>
    public static readonly int RlTextureFilterMipNearest = 9984;
    /// <summary> GL_NEAREST_MIPMAP_LINEAR </summary>
    public static readonly int RlTextureFilterNearestMipLinear = 9986;
    /// <summary> GL_LINEAR_MIPMAP_NEAREST </summary>
    public static readonly int RlTextureFilterLinearMipNearest = 9985;
    /// <summary> GL_LINEAR_MIPMAP_LINEAR </summary>
    public static readonly int RlTextureFilterMipLinear = 9987;
    /// <summary> Anisotropic filter (custom identifier) </summary>
    public static readonly int RlTextureFilterAnisotropic = 12288;
    /// <summary> Texture mipmap bias, percentage ratio (custom identifier) </summary>
    public static readonly int RlTextureMipmapBiasRatio = 16384;
    /// <summary> GL_REPEAT </summary>
    public static readonly int RlTextureWrapRepeat = 10497;
    /// <summary> GL_CLAMP_TO_EDGE </summary>
    public static readonly int RlTextureWrapClamp = 33071;
    /// <summary> GL_MIRRORED_REPEAT </summary>
    public static readonly int RlTextureWrapMirrorRepeat = 33648;
    /// <summary> GL_MIRROR_CLAMP_EXT </summary>
    public static readonly int RlTextureWrapMirrorClamp = 34626;
    /// <summary> GL_MODELVIEW </summary>
    public static readonly int RlModelview = 5888;
    /// <summary> GL_PROJECTION </summary>
    public static readonly int RlProjection = 5889;
    /// <summary> GL_TEXTURE </summary>
    public static readonly int RlTexture = 5890;
    /// <summary> GL_LINES </summary>
    public static readonly int RlLines = 1;
    /// <summary> GL_TRIANGLES </summary>
    public static readonly int RlTriangles = 4;
    /// <summary> GL_QUADS </summary>
    public static readonly int RlQuads = 7;
    /// <summary> GL_UNSIGNED_BYTE </summary>
    public static readonly int RlUnsignedByte = 5121;
    /// <summary> GL_FLOAT </summary>
    public static readonly int RlFloat = 5126;
    /// <summary> GL_STREAM_DRAW </summary>
    public static readonly int RlStreamDraw = 35040;
    /// <summary> GL_STREAM_READ </summary>
    public static readonly int RlStreamRead = 35041;
    /// <summary> GL_STREAM_COPY </summary>
    public static readonly int RlStreamCopy = 35042;
    /// <summary> GL_STATIC_DRAW </summary>
    public static readonly int RlStaticDraw = 35044;
    /// <summary> GL_STATIC_READ </summary>
    public static readonly int RlStaticRead = 35045;
    /// <summary> GL_STATIC_COPY </summary>
    public static readonly int RlStaticCopy = 35046;
    /// <summary> GL_DYNAMIC_DRAW </summary>
    public static readonly int RlDynamicDraw = 35048;
    /// <summary> GL_DYNAMIC_READ </summary>
    public static readonly int RlDynamicRead = 35049;
    /// <summary> GL_DYNAMIC_COPY </summary>
    public static readonly int RlDynamicCopy = 35050;
    /// <summary> GL_FRAGMENT_SHADER </summary>
    public static readonly int RlFragmentShader = 35632;
    /// <summary> GL_VERTEX_SHADER </summary>
    public static readonly int RlVertexShader = 35633;
    /// <summary> GL_COMPUTE_SHADER </summary>
    public static readonly int RlComputeShader = 37305;
    /// <summary> GL_ZERO </summary>
    public static readonly int RlZero = 0;
    /// <summary> GL_ONE </summary>
    public static readonly int RlOne = 1;
    /// <summary> GL_SRC_COLOR </summary>
    public static readonly int RlSrcColor = 768;
    /// <summary> GL_ONE_MINUS_SRC_COLOR </summary>
    public static readonly int RlOneMinusSrcColor = 769;
    /// <summary> GL_SRC_ALPHA </summary>
    public static readonly int RlSrcAlpha = 770;
    /// <summary> GL_ONE_MINUS_SRC_ALPHA </summary>
    public static readonly int RlOneMinusSrcAlpha = 771;
    /// <summary> GL_DST_ALPHA </summary>
    public static readonly int RlDstAlpha = 772;
    /// <summary> GL_ONE_MINUS_DST_ALPHA </summary>
    public static readonly int RlOneMinusDstAlpha = 773;
    /// <summary> GL_DST_COLOR </summary>
    public static readonly int RlDstColor = 774;
    /// <summary> GL_ONE_MINUS_DST_COLOR </summary>
    public static readonly int RlOneMinusDstColor = 775;
    /// <summary> GL_SRC_ALPHA_SATURATE </summary>
    public static readonly int RlSrcAlphaSaturate = 776;
    /// <summary> GL_CONSTANT_COLOR </summary>
    public static readonly int RlConstantColor = 32769;
    /// <summary> GL_ONE_MINUS_CONSTANT_COLOR </summary>
    public static readonly int RlOneMinusConstantColor = 32770;
    /// <summary> GL_CONSTANT_ALPHA </summary>
    public static readonly int RlConstantAlpha = 32771;
    /// <summary> GL_ONE_MINUS_CONSTANT_ALPHA </summary>
    public static readonly int RlOneMinusConstantAlpha = 32772;
    /// <summary> GL_FUNC_ADD </summary>
    public static readonly int RlFuncAdd = 32774;
    /// <summary> GL_MIN </summary>
    public static readonly int RlMin = 32775;
    /// <summary> GL_MAX </summary>
    public static readonly int RlMax = 32776;
    /// <summary> GL_FUNC_SUBTRACT </summary>
    public static readonly int RlFuncSubtract = 32778;
    /// <summary> GL_FUNC_REVERSE_SUBTRACT </summary>
    public static readonly int RlFuncReverseSubtract = 32779;
    /// <summary> GL_BLEND_EQUATION </summary>
    public static readonly int RlBlendEquation = 32777;
    /// <summary> GL_BLEND_EQUATION_RGB   // (Same as BLEND_EQUATION) </summary>
    public static readonly int RlBlendEquationRgb = 32777;
    /// <summary> GL_BLEND_EQUATION_ALPHA </summary>
    public static readonly int RlBlendEquationAlpha = 34877;
    /// <summary> GL_BLEND_DST_RGB </summary>
    public static readonly int RlBlendDstRgb = 32968;
    /// <summary> GL_BLEND_SRC_RGB </summary>
    public static readonly int RlBlendSrcRgb = 32969;
    /// <summary> GL_BLEND_DST_ALPHA </summary>
    public static readonly int RlBlendDstAlpha = 32970;
    /// <summary> GL_BLEND_SRC_ALPHA </summary>
    public static readonly int RlBlendSrcAlpha = 32971;
    /// <summary> GL_BLEND_COLOR </summary>
    public static readonly int RlBlendColor = 32773;
}

