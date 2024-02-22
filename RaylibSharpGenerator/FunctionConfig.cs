namespace RaylibSharp.Generator;

public struct FunctionConfig
{
    public string[] Excluded { get; set; }
    public bool DebugOutput { get; set; }
    public Dictionary<string, Dictionary<string, string>> FunctionTypeConversion { get; set; }

    static FunctionConfig()
    {
        Dictionary<string, Dictionary<string, string>> data = new()
        {
            {"BeginBlendMode", new() { { "mode", "BlendMode mode" } }},
            {"ClearWindowState", new() { { "flags", "WindowFlag flags" } }},
            {"DrawMeshInstanced", new() { { "transforms", "Matrix4x4[] transforms" } }},
            {"DrawTriangleFan", new() { { "points", "Vector2[] points" } }},
            {"GenTextureMipmaps", new() { { "texture", "ref Texture texture" } }},
            {"GetGamepadAxisMovement", new() { { "axis", "GamepadAxis axis" } }},
            {"GetGamepadButtonPressed", new() { { "_", "GamepadButton" } }},
            {"GetGestureDetected", new() { { "_", "Gesture" } }},
            {"IsGamepadButtonDown", new() { { "button", "GamepadButton button" } }},
            {"IsGamepadButtonPressed", new() { { "button", "GamepadButton button" } }},
            {"IsGamepadButtonReleased", new() { { "button", "GamepadButton button" } }},
            {"IsGamepadButtonUp", new() { { "button", "GamepadButton button" } }},
            {"IsKeyDown", new() { { "key", "Key key" } }},
            {"IsKeyPressed", new() { { "key", "Key key" } }},
            {"IsKeyReleased", new() { { "key", "Key key" } }},
            {"IsKeyUp", new() { { "key", "Key key" } }},
            {"IsMouseButtonDown", new() { { "button", "MouseButton button" } }},
            {"IsMouseButtonPressed", new() { { "button", "MouseButton button" } }},
            {"IsMouseButtonReleased", new() { { "button", "MouseButton button" } }},
            {"IsMouseButtonUp", new() { { "button", "MouseButton button" } }},
            {"IsWindowState", new() { { "flag", "WindowFlag flag" } }},
            {"LoadFileText", new() { { "_", "string" } }},
            {"LoadFontFromMemory", new() { { "fileData", "[MarshalAs(UnmanagedType.LPArray)] byte[] fileData" } }},
            {"LoadImageFromMemory", new() { { "fileData", "[MarshalAs(UnmanagedType.LPArray)] byte[] fileData" } }},
            {"LoadImageAnim", new() { { "frames", "ref int frames" } }},
            {"LoadTextureCubemap", new() { { "layout", "CubemapLayout layout" } }},
            {"rlBindImageTexture", new() { { "readonly", "[MarshalAs(UnmanagedType.I1)] bool @readonly" } }},
            {"rlLoadTexture", new() { { "format", "PixelFormat format" } }},
            {"rlLoadTextureCubemap", new() { { "format", "PixelFormat format" } }},
            {"rlLoadVertexBuffer", new() { { "buffer", "float* buffer" } }},
            {"rlSetBlendMode", new() { { "mode", "BlendMode mode" } }},
            {"rlSetCullFace", new() { { "mode", "CullMode mode" } }},
            {"SetConfigFlags", new() { { "flags", "WindowFlag flags" } }},
            {"SetExitKey", new() { { "key", "Key key" } }},
            {"SetMouseCursor", new() { { "cursor", "MouseCursor cursor" } }},
            {"SetShaderValue", new() { { "uniformType", "ShaderUniformDataType uniformType" } }},
            {"SetTextureFilter", new() { { "filter", "TextureFilter filter" } }},
            {"SetTraceLogLevel", new() { { "logLevel", "TraceLogLevel logLevel" } }},
            {"SetWindowState", new() { { "flags", "WindowFlag flags" } }},
            {"UpdateCamera", new() { { "mode", "CameraMode mode" } }},
            {"UploadMesh", new() { { "mesh", "ref Mesh mesh" } }},
            {"ImageDrawPixel", new() { { "dst", "ref Image dst" } }},
            {"ImageDraw", new() { { "dst", "ref Image dst" } }},
            {"ImageResize", new() { { "image", "ref Image image" } }},
            {"ImageCrop", new() { { "image", "ref Image image" } }},
            {"ImageDrawCircleLines", new() { { "dst", "ref Image dst" } }},
            {"ImageDrawRectangle", new() { { "dst", "ref Image dst" } }},
            {"ImageDrawText", new() { { "dst", "ref Image dst" } }},
            {"ImageDrawTextEx", new() { { "dst", "ref Image dst" } }},
            {"ImageColorGrayscale", new() { { "image", "ref Image image" } }},
            {"ImageColorTint", new() { { "image", "ref Image image" } }},
            {"ImageColorInvert", new() { { "image", "ref Image image" } }},
            {"ImageColorContrast", new() { { "image", "ref Image image" } }},
            {"ImageColorBrightness", new() { { "image", "ref Image image" } }},
            {"ImageBlurGaussian", new() { { "image", "ref Image image" } }},
            {"ImageFlipVertical", new() { { "image", "ref Image image" } }},
            {"ImageFlipHorizontal", new() { { "image", "ref Image image" } }},
            {"ImageRotate", new() { { "image", "ref Image image" } }},
            {"LoadImageRaw", new() { { "format", "PixelFormat format" } }},
            {"ImageFormat", new() {
                {"image", "ref Image image"},
                {"newFormat", "PixelFormat newFormat"}
            }},
            {"SetMaterialTexture", new() {
                {"mapType", "MaterialMapIndex mapType"},
                {"material", "Material material"}
            }},
            {"rlFramebufferAttach", new() {
                {"attachType", "FramebufferAttachType attachType"},
                {"texType", "FramebufferAttachTextureType texType"}
            }},
            {"LoadModelAnimations", new() {
                {"_", "ModelAnimation[]"},
                {"@", "[return: MarshalUsing(CountElementName = \"animCount\")]"},
                {"animCount", "ref uint animCount"}
            }},
            {"LoadShader", new() {
                {"fsFileName", "[MarshalAs(UnmanagedType.LPStr)] string? fragmentShaderPath"},
                {"vsFileName", "[MarshalAs(UnmanagedType.LPStr)] string? vertexShaderPath"}
            }},
            {"LoadShaderFromMemory", new() {
                {"vsCode", "[MarshalAs(UnmanagedType.LPStr)] string? fragmentShaderCode"},
                {"fsCode", "[MarshalAs(UnmanagedType.LPStr)] string? vertexShaderCode"}
            }}
        };

        Data.FunctionTypeConversion = data;
    }

    public static readonly FunctionConfig Data = new()
    {
        DebugOutput = false,
        Excluded = [
            // Have C# alternatives
            "DirectoryExists",
            "FileExists",
            "GetApplicationDirectory",
            "GetDirectoryPath",
            "GetFileExtension",
            "GetFileName",
            "GetFileNameWithoutExt",
            "GetPrevDirectoryPath",
            "GetRandomValue",
            "GetWorkingDirectory",
            "IsFileExtension",
            "LoadDirectoryFiles",
            "LoadDirectoryFilesEx",
            "SaveFileText",
            "SetRandomSeed",
            "UnloadDirectoryFiles",
            "LoadFileData",
            "UnloadFileData",
            "GetFileLength",
            "SaveFileData",
            "ExportDataAsCode",
            "LoadFileText",
            "UnloadFileText",
            // Text
            "TextFormat",
            "TextToUpper",
            "TextIsEqual",
            // Unnecessary
            "GetMouseWheelMove",
            "CompressData",
            "DecompressData",
            "EncodeDataBase64",
            "DecodeDataBase64",
            "ExportImageAsCode",
            "TraceLog", // Errors
            // Manually implemented
            "SetTraceLogCallback",
            "InitWindow",
            "LoadImageColors",
            "UnloadImageColors",
            "rlLoadRenderBatch",
            "rlUnloadRenderBatch"
        ],
    };
}
