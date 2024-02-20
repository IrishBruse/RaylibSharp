namespace RaylibSharp.Generator;

using System.Text.Json;

public struct FunctionConfig
{
    public string[] Excluded { get; set; }
    public bool DebugOutput { get; set; }
    public Dictionary<string, Dictionary<string, string>> FunctionTypeConversion { get; set; }
    static readonly JsonSerializerOptions Options = new() { ReadCommentHandling = JsonCommentHandling.Skip };
    public static FunctionConfig Deserialize(string path)
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<FunctionConfig>(json, Options)!;
    }

    static FunctionConfig()
    {
        Dictionary<string, Dictionary<string, string>> data = new()
        {
            ["BeginBlendMode"] = { { "mode", "BlendMode mode" } },
            ["ClearWindowState"] = { { "flags", "WindowFlag flags" } },
            ["DrawMeshInstanced"] = { { "transforms", "Matrix4x4[] transforms" } },
            ["DrawTriangleFan"] = { { "points", "Vector2[] points" } },
            ["GenTextureMipmaps"] = { { "texture", "ref Texture texture" } },
            ["GetGamepadAxisMovement"] = { { "axis", "GamepadAxis axis" } },
            ["GetGamepadButtonPressed"] = { { "_", "GamepadButton" } },
            ["GetGestureDetected"] = { { "_", "Gesture" } },
            ["IsGamepadButtonDown"] = { { "button", "GamepadButton button" } },
            ["IsGamepadButtonPressed"] = { { "button", "GamepadButton button" } },
            ["IsGamepadButtonReleased"] = { { "button", "GamepadButton button" } },
            ["IsGamepadButtonUp"] = { { "button", "GamepadButton button" } },
            ["IsKeyDown"] = { { "key", "Key key" } },
            ["IsKeyPressed"] = { { "key", "Key key" } },
            ["IsKeyReleased"] = { { "key", "Key key" } },
            ["IsKeyUp"] = { { "key", "Key key" } },
            ["IsMouseButtonDown"] = { { "button", "MouseButton button" } },
            ["IsMouseButtonPressed"] = { { "button", "MouseButton button" } },
            ["IsMouseButtonReleased"] = { { "button", "MouseButton button" } },
            ["IsMouseButtonUp"] = { { "button", "MouseButton button" } },
            ["IsWindowState"] = { { "flag", "WindowFlag flag" } },
            ["LoadFileText"] = { { "_", "string" } },
            ["LoadFontFromMemory"] = { { "fileData", "[MarshalAs(UnmanagedType.LPArray)] byte[] fileData" } },
            ["LoadImageFromMemory"] = { { "fileData", "[MarshalAs(UnmanagedType.LPArray)] byte[] fileData" } },
            ["LoadImageAnim"] = { { "frames", "ref int frames" } },
            ["LoadTextureCubemap"] = { { "layout", "CubemapLayout layout" } },
            ["rlBindImageTexture"] = { { "readonly", "[MarshalAs(UnmanagedType.I1)] bool @readonly" } },
            ["rlLoadTexture"] = { { "format", "PixelFormat format" } },
            ["rlLoadTextureCubemap"] = { { "format", "PixelFormat format" } },
            ["rlLoadVertexBuffer"] = { { "buffer", "float* buffer" } },
            ["rlSetBlendMode"] = { { "mode", "BlendMode mode" } },
            ["rlSetCullFace"] = { { "mode", "CullMode mode" } },
            ["SetConfigFlags"] = { { "flags", "WindowFlag flags" } },
            ["SetExitKey"] = { { "key", "Key key" } },
            ["SetMouseCursor"] = { { "cursor", "MouseCursor cursor" } },
            ["SetShaderValue"] = { { "uniformType", "ShaderUniformDataType uniformType" } },
            ["SetTextureFilter"] = { { "filter", "TextureFilter filter" } },
            ["SetTraceLogLevel"] = { { "logLevel", "TraceLogLevel logLevel" } },
            ["SetWindowState"] = { { "flags", "WindowFlag flags" } },
            ["UpdateCamera"] = { { "mode", "CameraMode mode" } },
            ["UploadMesh"] = { { "mesh", "ref Mesh mesh" } },
            ["ImageDrawPixel"] = { { "dst", "ref Image dst" } },
            ["ImageDraw"] = { { "dst", "ref Image dst" } },
            ["ImageResize"] = { { "image", "ref Image image" } },
            ["ImageCrop"] = { { "image", "ref Image image" } },
            ["ImageDrawCircleLines"] = { { "dst", "ref Image dst" } },
            ["ImageDrawRectangle"] = { { "dst", "ref Image dst" } },
            ["ImageDrawText"] = { { "dst", "ref Image dst" } },
            ["ImageDrawTextEx"] = { { "dst", "ref Image dst" } },
            ["ImageColorGrayscale"] = { { "image", "ref Image image" } },
            ["ImageColorTint"] = { { "image", "ref Image image" } },
            ["ImageColorInvert"] = { { "image", "ref Image image" } },
            ["ImageColorContrast"] = { { "image", "ref Image image" } },
            ["ImageColorBrightness"] = { { "image", "ref Image image" } },
            ["ImageBlurGaussian"] = { { "image", "ref Image image" } },
            ["ImageFlipVertical"] = { { "image", "ref Image image" } },
            ["ImageFlipHorizontal"] = { { "image", "ref Image image" } },
            ["ImageRotate"] = { { "image", "ref Image image" } },
            ["LoadImageRaw"] = { { "format", "PixelFormat format" } },

            ["ImageFormat"] = new(){
                {"image", "ref Image image"},
                {"newFormat", "PixelFormat newFormat"}
            },
            ["SetMaterialTexture"] = new(){
                {"mapType", "MaterialMapIndex mapType"},
                {"material", "Material material"}
            },
            ["rlFramebufferAttach"] = new(){
                {"attachType", "FramebufferAttachType attachType"},
                {"texType", "FramebufferAttachTextureType texType"}
            },
            ["LoadModelAnimations"] = new(){
                {"_", "ModelAnimation[]"},
                {"@", "[return, MarshalUsing(CountElementName = \"animCount\")]"},
                {"animCount", "ref uint animCount"}
            },
            ["LoadShader"] = new(){
                {"fsFileName", "[MarshalAs(UnmanagedType.LPStr)] string? fragmentShaderPath"},
                {"vsFileName", "[MarshalAs(UnmanagedType.LPStr)] string? vertexShaderPath"}
            },
            ["LoadShaderFromMemory"] = new(){
                {"vsCode", "[MarshalAs(UnmanagedType.LPStr)] string? fragmentShaderCode"},
                {"fsCode", "[MarshalAs(UnmanagedType.LPStr)] string? vertexShaderCode"}
            }
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
