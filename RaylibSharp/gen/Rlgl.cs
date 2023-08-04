namespace RaylibSharp.GL;

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Numerics;
using System.Drawing;

public static unsafe partial class RLGL
{
    /// <summary> Choose the current matrix to be transformed </summary>
    [LibraryImport(LIB, EntryPoint = "rlMatrixMode")]
    public static partial void MatrixMode(int mode);

    /// <summary> Push the current matrix to stack </summary>
    [LibraryImport(LIB, EntryPoint = "rlPushMatrix")]
    public static partial void PushMatrix();

    /// <summary> Pop latest inserted matrix from stack </summary>
    [LibraryImport(LIB, EntryPoint = "rlPopMatrix")]
    public static partial void PopMatrix();

    /// <summary> Reset current matrix to identity matrix </summary>
    [LibraryImport(LIB, EntryPoint = "rlLoadIdentity")]
    public static partial void LoadIdentity();

    /// <summary> Multiply the current matrix by a translation matrix </summary>
    [LibraryImport(LIB, EntryPoint = "rlTranslatef")]
    public static partial void Translatef(float x, float y, float z);

    /// <summary> Multiply the current matrix by a rotation matrix </summary>
    [LibraryImport(LIB, EntryPoint = "rlRotatef")]
    public static partial void Rotatef(float angle, float x, float y, float z);

    /// <summary> Multiply the current matrix by a scaling matrix </summary>
    [LibraryImport(LIB, EntryPoint = "rlScalef")]
    public static partial void Scalef(float x, float y, float z);

    /// <summary> Multiply the current matrix by another matrix </summary>
    [LibraryImport(LIB, EntryPoint = "rlMultMatrixf")]
    public static partial void MultMatrixf(IntPtr matf);

    /// <summary>  </summary>
    [LibraryImport(LIB, EntryPoint = "rlFrustum")]
    public static partial void Frustum(double left, double right, double bottom, double top, double znear, double zfar);

    /// <summary>  </summary>
    [LibraryImport(LIB, EntryPoint = "rlOrtho")]
    public static partial void Ortho(double left, double right, double bottom, double top, double znear, double zfar);

    /// <summary> Set the viewport area </summary>
    [LibraryImport(LIB, EntryPoint = "rlViewport")]
    public static partial void Viewport(int x, int y, int width, int height);

    /// <summary> Initialize drawing mode (how to organize vertex) </summary>
    [LibraryImport(LIB, EntryPoint = "rlBegin")]
    public static partial void Begin(int mode);

    /// <summary> Finish vertex providing </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnd")]
    public static partial void End();

    /// <summary> Define one vertex (position) - 2 int </summary>
    [LibraryImport(LIB, EntryPoint = "rlVertex2i")]
    public static partial void Vertex2i(int x, int y);

    /// <summary> Define one vertex (position) - 2 float </summary>
    [LibraryImport(LIB, EntryPoint = "rlVertex2f")]
    public static partial void Vertex2f(float x, float y);

    /// <summary> Define one vertex (position) - 3 float </summary>
    [LibraryImport(LIB, EntryPoint = "rlVertex3f")]
    public static partial void Vertex3f(float x, float y, float z);

    /// <summary> Define one vertex (texture coordinate) - 2 float </summary>
    [LibraryImport(LIB, EntryPoint = "rlTexCoord2f")]
    public static partial void TexCoord2f(float x, float y);

    /// <summary> Define one vertex (normal) - 3 float </summary>
    [LibraryImport(LIB, EntryPoint = "rlNormal3f")]
    public static partial void Normal3f(float x, float y, float z);

    /// <summary> Define one vertex (color) - 4 byte </summary>
    [LibraryImport(LIB, EntryPoint = "rlColor4ub")]
    public static partial void Color4ub(byte r, byte g, byte b, byte a);

    /// <summary> Define one vertex (color) - 3 float </summary>
    [LibraryImport(LIB, EntryPoint = "rlColor3f")]
    public static partial void Color3f(float x, float y, float z);

    /// <summary> Define one vertex (color) - 4 float </summary>
    [LibraryImport(LIB, EntryPoint = "rlColor4f")]
    public static partial void Color4f(float x, float y, float z, float w);

    /// <summary> Enable vertex array (VAO, if supported) </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnableVertexArray")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool EnableVertexArray(uint vaoId);

    /// <summary> Disable vertex array (VAO, if supported) </summary>
    [LibraryImport(LIB, EntryPoint = "rlDisableVertexArray")]
    public static partial void DisableVertexArray();

    /// <summary> Enable vertex buffer (VBO) </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnableVertexBuffer")]
    public static partial void EnableVertexBuffer(uint id);

    /// <summary> Disable vertex buffer (VBO) </summary>
    [LibraryImport(LIB, EntryPoint = "rlDisableVertexBuffer")]
    public static partial void DisableVertexBuffer();

    /// <summary> Enable vertex buffer element (VBO element) </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnableVertexBufferElement")]
    public static partial void EnableVertexBufferElement(uint id);

    /// <summary> Disable vertex buffer element (VBO element) </summary>
    [LibraryImport(LIB, EntryPoint = "rlDisableVertexBufferElement")]
    public static partial void DisableVertexBufferElement();

    /// <summary> Enable vertex attribute index </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnableVertexAttribute")]
    public static partial void EnableVertexAttribute(uint index);

    /// <summary> Disable vertex attribute index </summary>
    [LibraryImport(LIB, EntryPoint = "rlDisableVertexAttribute")]
    public static partial void DisableVertexAttribute(uint index);

    /// <summary> Enable attribute state pointer </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnableStatePointer")]
    public static partial void EnableStatePointer(int vertexAttribType, IntPtr buffer);

    /// <summary> Disable attribute state pointer </summary>
    [LibraryImport(LIB, EntryPoint = "rlDisableStatePointer")]
    public static partial void DisableStatePointer(int vertexAttribType);

    /// <summary> Select and active a texture slot </summary>
    [LibraryImport(LIB, EntryPoint = "rlActiveTextureSlot")]
    public static partial void ActiveTextureSlot(int slot);

    /// <summary> Enable texture </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnableTexture")]
    public static partial void EnableTexture(uint id);

    /// <summary> Disable texture </summary>
    [LibraryImport(LIB, EntryPoint = "rlDisableTexture")]
    public static partial void DisableTexture();

    /// <summary> Enable texture cubemap </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnableTextureCubemap")]
    public static partial void EnableTextureCubemap(uint id);

    /// <summary> Disable texture cubemap </summary>
    [LibraryImport(LIB, EntryPoint = "rlDisableTextureCubemap")]
    public static partial void DisableTextureCubemap();

    /// <summary> Set texture parameters (filter, wrap) </summary>
    [LibraryImport(LIB, EntryPoint = "rlTextureParameters")]
    public static partial void TextureParameters(uint id, int param, int value);

    /// <summary> Set cubemap parameters (filter, wrap) </summary>
    [LibraryImport(LIB, EntryPoint = "rlCubemapParameters")]
    public static partial void CubemapParameters(uint id, int param, int value);

    /// <summary> Enable shader program </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnableShader")]
    public static partial void EnableShader(uint id);

    /// <summary> Disable shader program </summary>
    [LibraryImport(LIB, EntryPoint = "rlDisableShader")]
    public static partial void DisableShader();

    /// <summary> Enable render texture (fbo) </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnableFramebuffer")]
    public static partial void EnableFramebuffer(uint id);

    /// <summary> Disable render texture (fbo), return to default framebuffer </summary>
    [LibraryImport(LIB, EntryPoint = "rlDisableFramebuffer")]
    public static partial void DisableFramebuffer();

    /// <summary> Activate multiple draw color buffers </summary>
    [LibraryImport(LIB, EntryPoint = "rlActiveDrawBuffers")]
    public static partial void ActiveDrawBuffers(int count);

    /// <summary> Enable color blending </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnableColorBlend")]
    public static partial void EnableColorBlend();

    /// <summary> Disable color blending </summary>
    [LibraryImport(LIB, EntryPoint = "rlDisableColorBlend")]
    public static partial void DisableColorBlend();

    /// <summary> Enable depth test </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnableDepthTest")]
    public static partial void EnableDepthTest();

    /// <summary> Disable depth test </summary>
    [LibraryImport(LIB, EntryPoint = "rlDisableDepthTest")]
    public static partial void DisableDepthTest();

    /// <summary> Enable depth write </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnableDepthMask")]
    public static partial void EnableDepthMask();

    /// <summary> Disable depth write </summary>
    [LibraryImport(LIB, EntryPoint = "rlDisableDepthMask")]
    public static partial void DisableDepthMask();

    /// <summary> Enable backface culling </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnableBackfaceCulling")]
    public static partial void EnableBackfaceCulling();

    /// <summary> Disable backface culling </summary>
    [LibraryImport(LIB, EntryPoint = "rlDisableBackfaceCulling")]
    public static partial void DisableBackfaceCulling();

    /// <summary> Set face culling mode </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetCullFace")]
    public static partial void SetCullFace(CullMode mode);

    /// <summary> Enable scissor test </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnableScissorTest")]
    public static partial void EnableScissorTest();

    /// <summary> Disable scissor test </summary>
    [LibraryImport(LIB, EntryPoint = "rlDisableScissorTest")]
    public static partial void DisableScissorTest();

    /// <summary> Scissor test </summary>
    [LibraryImport(LIB, EntryPoint = "rlScissor")]
    public static partial void Scissor(int x, int y, int width, int height);

    /// <summary> Enable wire mode </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnableWireMode")]
    public static partial void EnableWireMode();

    /// <summary> Disable wire mode </summary>
    [LibraryImport(LIB, EntryPoint = "rlDisableWireMode")]
    public static partial void DisableWireMode();

    /// <summary> Set the line drawing width </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetLineWidth")]
    public static partial void SetLineWidth(float width);

    /// <summary> Get the line drawing width </summary>
    [LibraryImport(LIB, EntryPoint = "rlGetLineWidth")]
    public static partial float GetLineWidth();

    /// <summary> Enable line aliasing </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnableSmoothLines")]
    public static partial void EnableSmoothLines();

    /// <summary> Disable line aliasing </summary>
    [LibraryImport(LIB, EntryPoint = "rlDisableSmoothLines")]
    public static partial void DisableSmoothLines();

    /// <summary> Enable stereo rendering </summary>
    [LibraryImport(LIB, EntryPoint = "rlEnableStereoRender")]
    public static partial void EnableStereoRender();

    /// <summary> Disable stereo rendering </summary>
    [LibraryImport(LIB, EntryPoint = "rlDisableStereoRender")]
    public static partial void DisableStereoRender();

    /// <summary> Check if stereo render is enabled </summary>
    [LibraryImport(LIB, EntryPoint = "rlIsStereoRenderEnabled")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsStereoRenderEnabled();

    /// <summary> Clear color buffer with color </summary>
    [LibraryImport(LIB, EntryPoint = "rlClearColor")]
    public static partial void ClearColor(byte r, byte g, byte b, byte a);

    /// <summary> Clear used screen buffers (color and depth) </summary>
    [LibraryImport(LIB, EntryPoint = "rlClearScreenBuffers")]
    public static partial void ClearScreenBuffers();

    /// <summary> Check and log OpenGL error codes </summary>
    [LibraryImport(LIB, EntryPoint = "rlCheckErrors")]
    public static partial void CheckErrors();

    /// <summary> Set blending mode </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetBlendMode")]
    public static partial void SetBlendMode(BlendMode mode);

    /// <summary> Set blending mode factor and equation (using OpenGL factors) </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetBlendFactors")]
    public static partial void SetBlendFactors(int glSrcFactor, int glDstFactor, int glEquation);

    /// <summary> Set blending mode factors and equations separately (using OpenGL factors) </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetBlendFactorsSeparate")]
    public static partial void SetBlendFactorsSeparate(int glSrcRGB, int glDstRGB, int glSrcAlpha, int glDstAlpha, int glEqRGB, int glEqAlpha);

    /// <summary> Initialize rlgl (buffers, shaders, textures, states) </summary>
    [LibraryImport(LIB, EntryPoint = "rlglInit")]
    public static partial void glInit(int width, int height);

    /// <summary> De-initialize rlgl (buffers, shaders, textures) </summary>
    [LibraryImport(LIB, EntryPoint = "rlglClose")]
    public static partial void glClose();

    /// <summary> Load OpenGL extensions (loader function required) </summary>
    [LibraryImport(LIB, EntryPoint = "rlLoadExtensions")]
    public static partial void LoadExtensions(IntPtr loader);

    /// <summary> Get current OpenGL version </summary>
    [LibraryImport(LIB, EntryPoint = "rlGetVersion")]
    public static partial int GetVersion();

    /// <summary> Set current framebuffer width </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetFramebufferWidth")]
    public static partial void SetFramebufferWidth(int width);

    /// <summary> Get default framebuffer width </summary>
    [LibraryImport(LIB, EntryPoint = "rlGetFramebufferWidth")]
    public static partial int GetFramebufferWidth();

    /// <summary> Set current framebuffer height </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetFramebufferHeight")]
    public static partial void SetFramebufferHeight(int height);

    /// <summary> Get default framebuffer height </summary>
    [LibraryImport(LIB, EntryPoint = "rlGetFramebufferHeight")]
    public static partial int GetFramebufferHeight();

    /// <summary> Get default texture id </summary>
    [LibraryImport(LIB, EntryPoint = "rlGetTextureIdDefault")]
    public static partial uint GetTextureIdDefault();

    /// <summary> Get default shader id </summary>
    [LibraryImport(LIB, EntryPoint = "rlGetShaderIdDefault")]
    public static partial uint GetShaderIdDefault();

    /// <summary> Get default shader locations </summary>
    [LibraryImport(LIB, EntryPoint = "rlGetShaderLocsDefault")]
    public static partial IntPtr GetShaderLocsDefault();

    /// <summary> Load a render batch system </summary>
    [LibraryImport(LIB, EntryPoint = "rlLoadRenderBatch")]
    public static partial RenderBatch LoadRenderBatch(int numBuffers, int bufferElements);

    /// <summary> Unload render batch system </summary>
    [LibraryImport(LIB, EntryPoint = "rlUnloadRenderBatch")]
    public static partial void UnloadRenderBatch(RenderBatch batch);

    /// <summary> Draw render batch data (Update->Draw->Reset) </summary>
    [LibraryImport(LIB, EntryPoint = "rlDrawRenderBatch")]
    public static partial void DrawRenderBatch(IntPtr batch);

    /// <summary> Set the active render batch for rlgl (NULL for default internal) </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetRenderBatchActive")]
    public static partial void SetRenderBatchActive(IntPtr batch);

    /// <summary> Update and draw internal render batch </summary>
    [LibraryImport(LIB, EntryPoint = "rlDrawRenderBatchActive")]
    public static partial void DrawRenderBatchActive();

    /// <summary> Check internal buffer overflow for a given number of vertex </summary>
    [LibraryImport(LIB, EntryPoint = "rlCheckRenderBatchLimit")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CheckRenderBatchLimit(int vCount);

    /// <summary> Set current texture for render batch and check buffers limits </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetTexture")]
    public static partial void SetTexture(uint id);

    /// <summary> Load vertex array (vao) if supported </summary>
    [LibraryImport(LIB, EntryPoint = "rlLoadVertexArray")]
    public static partial uint LoadVertexArray();

    /// <summary> Load a vertex buffer attribute </summary>
    [LibraryImport(LIB, EntryPoint = "rlLoadVertexBuffer")]
    public static partial uint LoadVertexBuffer(IntPtr buffer, int size, [MarshalAs(UnmanagedType.I1)] bool dynamic);

    /// <summary> Load a new attributes element buffer </summary>
    [LibraryImport(LIB, EntryPoint = "rlLoadVertexBufferElement")]
    public static partial uint LoadVertexBufferElement(IntPtr buffer, int size, [MarshalAs(UnmanagedType.I1)] bool dynamic);

    /// <summary> Update GPU buffer with new data </summary>
    [LibraryImport(LIB, EntryPoint = "rlUpdateVertexBuffer")]
    public static partial void UpdateVertexBuffer(uint bufferId, IntPtr data, int dataSize, int offset);

    /// <summary> Update vertex buffer elements with new data </summary>
    [LibraryImport(LIB, EntryPoint = "rlUpdateVertexBufferElements")]
    public static partial void UpdateVertexBufferElements(uint id, IntPtr data, int dataSize, int offset);

    /// <summary>  </summary>
    [LibraryImport(LIB, EntryPoint = "rlUnloadVertexArray")]
    public static partial void UnloadVertexArray(uint vaoId);

    /// <summary>  </summary>
    [LibraryImport(LIB, EntryPoint = "rlUnloadVertexBuffer")]
    public static partial void UnloadVertexBuffer(uint vboId);

    /// <summary>  </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetVertexAttribute")]
    public static partial void SetVertexAttribute(uint index, int compSize, int type, [MarshalAs(UnmanagedType.I1)] bool normalized, int stride, IntPtr pointer);

    /// <summary>  </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetVertexAttributeDivisor")]
    public static partial void SetVertexAttributeDivisor(uint index, int divisor);

    /// <summary> Set vertex attribute default value </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetVertexAttributeDefault")]
    public static partial void SetVertexAttributeDefault(int locIndex, IntPtr value, int attribType, int count);

    /// <summary>  </summary>
    [LibraryImport(LIB, EntryPoint = "rlDrawVertexArray")]
    public static partial void DrawVertexArray(int offset, int count);

    /// <summary>  </summary>
    [LibraryImport(LIB, EntryPoint = "rlDrawVertexArrayElements")]
    public static partial void DrawVertexArrayElements(int offset, int count, IntPtr buffer);

    /// <summary>  </summary>
    [LibraryImport(LIB, EntryPoint = "rlDrawVertexArrayInstanced")]
    public static partial void DrawVertexArrayInstanced(int offset, int count, int instances);

    /// <summary>  </summary>
    [LibraryImport(LIB, EntryPoint = "rlDrawVertexArrayElementsInstanced")]
    public static partial void DrawVertexArrayElementsInstanced(int offset, int count, IntPtr buffer, int instances);

    /// <summary> Load texture in GPU </summary>
    [LibraryImport(LIB, EntryPoint = "rlLoadTexture")]
    public static partial uint LoadTexture(IntPtr data, int width, int height, int format, int mipmapCount);

    /// <summary> Load depth texture/renderbuffer (to be attached to fbo) </summary>
    [LibraryImport(LIB, EntryPoint = "rlLoadTextureDepth")]
    public static partial uint LoadTextureDepth(int width, int height, [MarshalAs(UnmanagedType.I1)] bool useRenderBuffer);

    /// <summary> Load texture cubemap </summary>
    [LibraryImport(LIB, EntryPoint = "rlLoadTextureCubemap")]
    public static partial uint LoadTextureCubemap(IntPtr data, int size, PixelFormat format);

    /// <summary> Update GPU texture with new data </summary>
    [LibraryImport(LIB, EntryPoint = "rlUpdateTexture")]
    public static partial void UpdateTexture(uint id, int offsetX, int offsetY, int width, int height, int format, IntPtr data);

    /// <summary> Get OpenGL internal formats </summary>
    [LibraryImport(LIB, EntryPoint = "rlGetGlTextureFormats")]
    public static partial void GetGlTextureFormats(int format, uint* glInternalFormat, uint* glFormat, uint* glType);

    /// <summary> Get name string for pixel format </summary>
    [LibraryImport(LIB, EntryPoint = "rlGetPixelFormatName")]
    [return: MarshalAs(UnmanagedType.LPStr)]
    public static partial string GetPixelFormatName(uint format);

    /// <summary> Unload texture from GPU memory </summary>
    [LibraryImport(LIB, EntryPoint = "rlUnloadTexture")]
    public static partial void UnloadTexture(uint id);

    /// <summary> Generate mipmap data for selected texture </summary>
    [LibraryImport(LIB, EntryPoint = "rlGenTextureMipmaps")]
    public static partial void GenTextureMipmaps(uint id, int width, int height, int format, IntPtr mipmaps);

    /// <summary> Read texture pixel data </summary>
    [LibraryImport(LIB, EntryPoint = "rlReadTexturePixels")]
    public static partial IntPtr ReadTexturePixels(uint id, int width, int height, int format);

    /// <summary> Read screen pixel data (color buffer) </summary>
    [LibraryImport(LIB, EntryPoint = "rlReadScreenPixels")]
    public static partial byte* ReadScreenPixels(int width, int height);

    /// <summary> Load an empty framebuffer </summary>
    [LibraryImport(LIB, EntryPoint = "rlLoadFramebuffer")]
    public static partial uint LoadFramebuffer(int width, int height);

    /// <summary> Attach texture/renderbuffer to a framebuffer </summary>
    [LibraryImport(LIB, EntryPoint = "rlFramebufferAttach")]
    public static partial void FramebufferAttach(uint fboId, uint texId, FramebufferAttachType attachType, FramebufferAttachTextureType texType, int mipLevel);

    /// <summary> Verify framebuffer is complete </summary>
    [LibraryImport(LIB, EntryPoint = "rlFramebufferComplete")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool FramebufferComplete(uint id);

    /// <summary> Delete framebuffer from GPU </summary>
    [LibraryImport(LIB, EntryPoint = "rlUnloadFramebuffer")]
    public static partial void UnloadFramebuffer(uint id);

    /// <summary> Load shader from code strings </summary>
    [LibraryImport(LIB, EntryPoint = "rlLoadShaderCode")]
    public static partial uint LoadShaderCode([MarshalAs(UnmanagedType.LPStr)] string vsCode, [MarshalAs(UnmanagedType.LPStr)] string fsCode);

    /// <summary> Compile custom shader and return shader id (type: RL_VERTEX_SHADER, RL_FRAGMENT_SHADER, RL_COMPUTE_SHADER) </summary>
    [LibraryImport(LIB, EntryPoint = "rlCompileShader")]
    public static partial uint CompileShader([MarshalAs(UnmanagedType.LPStr)] string shaderCode, int type);

    /// <summary> Load custom shader program </summary>
    [LibraryImport(LIB, EntryPoint = "rlLoadShaderProgram")]
    public static partial uint LoadShaderProgram(uint vShaderId, uint fShaderId);

    /// <summary> Unload shader program </summary>
    [LibraryImport(LIB, EntryPoint = "rlUnloadShaderProgram")]
    public static partial void UnloadShaderProgram(uint id);

    /// <summary> Get shader location uniform </summary>
    [LibraryImport(LIB, EntryPoint = "rlGetLocationUniform")]
    public static partial int GetLocationUniform(uint shaderId, [MarshalAs(UnmanagedType.LPStr)] string uniformName);

    /// <summary> Get shader location attribute </summary>
    [LibraryImport(LIB, EntryPoint = "rlGetLocationAttrib")]
    public static partial int GetLocationAttrib(uint shaderId, [MarshalAs(UnmanagedType.LPStr)] string attribName);

    /// <summary> Set shader value uniform </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetUniform")]
    public static partial void SetUniform(int locIndex, IntPtr value, int uniformType, int count);

    /// <summary> Set shader value matrix </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetUniformMatrix")]
    public static partial void SetUniformMatrix(int locIndex, Matrix4x4 mat);

    /// <summary> Set shader value sampler </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetUniformSampler")]
    public static partial void SetUniformSampler(int locIndex, uint textureId);

    /// <summary> Set shader currently active (id and locations) </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetShader")]
    public static partial void SetShader(uint id, IntPtr locs);

    /// <summary> Load compute shader program </summary>
    [LibraryImport(LIB, EntryPoint = "rlLoadComputeShaderProgram")]
    public static partial uint LoadComputeShaderProgram(uint shaderId);

    /// <summary> Dispatch compute shader (equivalent to *draw* for graphics pipeline) </summary>
    [LibraryImport(LIB, EntryPoint = "rlComputeShaderDispatch")]
    public static partial void ComputeShaderDispatch(uint groupX, uint groupY, uint groupZ);

    /// <summary> Load shader storage buffer object (SSBO) </summary>
    [LibraryImport(LIB, EntryPoint = "rlLoadShaderBuffer")]
    public static partial uint LoadShaderBuffer(uint size, IntPtr data, int usageHint);

    /// <summary> Unload shader storage buffer object (SSBO) </summary>
    [LibraryImport(LIB, EntryPoint = "rlUnloadShaderBuffer")]
    public static partial void UnloadShaderBuffer(uint ssboId);

    /// <summary> Update SSBO buffer data </summary>
    [LibraryImport(LIB, EntryPoint = "rlUpdateShaderBuffer")]
    public static partial void UpdateShaderBuffer(uint id, IntPtr data, uint dataSize, uint offset);

    /// <summary> Bind SSBO buffer </summary>
    [LibraryImport(LIB, EntryPoint = "rlBindShaderBuffer")]
    public static partial void BindShaderBuffer(uint id, uint index);

    /// <summary> Read SSBO buffer data (GPU->CPU) </summary>
    [LibraryImport(LIB, EntryPoint = "rlReadShaderBuffer")]
    public static partial void ReadShaderBuffer(uint id, IntPtr dest, uint count, uint offset);

    /// <summary> Copy SSBO data between buffers </summary>
    [LibraryImport(LIB, EntryPoint = "rlCopyShaderBuffer")]
    public static partial void CopyShaderBuffer(uint destId, uint srcId, uint destOffset, uint srcOffset, uint count);

    /// <summary> Get SSBO buffer size </summary>
    [LibraryImport(LIB, EntryPoint = "rlGetShaderBufferSize")]
    public static partial uint GetShaderBufferSize(uint id);

    /// <summary> Bind image texture </summary>
    [LibraryImport(LIB, EntryPoint = "rlBindImageTexture")]
    public static partial void BindImageTexture(uint id, uint index, int format, [MarshalAs(UnmanagedType.I1)] bool @readonly);

    /// <summary> Get internal modelview matrix </summary>
    [LibraryImport(LIB, EntryPoint = "rlGetMatrixModelview")]
    public static partial Matrix4x4 GetMatrixModelview();

    /// <summary> Get internal projection matrix </summary>
    [LibraryImport(LIB, EntryPoint = "rlGetMatrixProjection")]
    public static partial Matrix4x4 GetMatrixProjection();

    /// <summary> Get internal accumulated transform matrix </summary>
    [LibraryImport(LIB, EntryPoint = "rlGetMatrixTransform")]
    public static partial Matrix4x4 GetMatrixTransform();

    /// <summary> Get internal projection matrix for stereo render (selected eye) </summary>
    [LibraryImport(LIB, EntryPoint = "rlGetMatrixProjectionStereo")]
    public static partial Matrix4x4 GetMatrixProjectionStereo(int eye);

    /// <summary> Get internal view offset matrix for stereo render (selected eye) </summary>
    [LibraryImport(LIB, EntryPoint = "rlGetMatrixViewOffsetStereo")]
    public static partial Matrix4x4 GetMatrixViewOffsetStereo(int eye);

    /// <summary> Set a custom projection matrix (replaces internal projection matrix) </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetMatrixProjection")]
    public static partial void SetMatrixProjection(Matrix4x4 proj);

    /// <summary> Set a custom modelview matrix (replaces internal modelview matrix) </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetMatrixModelview")]
    public static partial void SetMatrixModelview(Matrix4x4 view);

    /// <summary> Set eyes projection matrices for stereo rendering </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetMatrixProjectionStereo")]
    public static partial void SetMatrixProjectionStereo(Matrix4x4 right, Matrix4x4 left);

    /// <summary> Set eyes view offsets matrices for stereo rendering </summary>
    [LibraryImport(LIB, EntryPoint = "rlSetMatrixViewOffsetStereo")]
    public static partial void SetMatrixViewOffsetStereo(Matrix4x4 right, Matrix4x4 left);

    /// <summary> Load and draw a cube </summary>
    [LibraryImport(LIB, EntryPoint = "rlLoadDrawCube")]
    public static partial void LoadDrawCube();

    /// <summary> Load and draw a quad </summary>
    [LibraryImport(LIB, EntryPoint = "rlLoadDrawQuad")]
    public static partial void LoadDrawQuad();

}

