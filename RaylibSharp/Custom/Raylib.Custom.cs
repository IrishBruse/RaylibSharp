namespace RaylibSharp;

using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

public static unsafe partial class Raylib
{
    private const string LIB = "raylib";

    /// <summary> Initialize window and OpenGL context </summary>
    [LibraryImport(LIB, EntryPoint = "InitWindow")]
    internal static partial void _InitWindow(int width, int height, [MarshalAs(UnmanagedType.LPStr)] string title);

    /// <summary> Initialize window and logging and load an embedded Icon.png file falling back on default </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void InitWindow(int width, int height, string title)
    {
        _InitWindow(width, height, title);
        if (!OperatingSystem.IsBrowser())
        {
            _SetTraceLogCallback(&NativeLog);
            LoadIcon();
        }
    }

    private static void LoadIcon()
    {
        Assembly assembly = Assembly.GetEntryAssembly()!;

        if (assembly == null)
        {
            return;
        }

        List<string> list = assembly.GetManifestResourceNames().ToList();
        string? iconResourcePath = list.FirstOrDefault(x => x!.EndsWith("Icon.png"), null);

        Stream logoStream;

        if (iconResourcePath != null)
        {
            TraceLog(TraceLogLevel.Info, $"ICON: Embedded Path '{iconResourcePath}'");

            logoStream = assembly.GetManifestResourceStream(iconResourcePath)!;
        }
        else
        {
            TraceLog(TraceLogLevel.Info, "ICON: Embedded Path 'RaylibSharp.Icon.png'");
            Assembly asm = Assembly.GetExecutingAssembly();
            logoStream = asm.GetManifestResourceStream("RaylibSharp.Icon.png")!;
        }

        using BinaryReader iconStream = new(logoStream);
        byte[] data = iconStream.ReadBytes((int)iconStream.BaseStream.Length);
        SetWindowIcon(LoadImageFromMemory(".png", data, data.Length));
    }

    /// <summary> Check if a key is being pressed </summary>
    public static bool IsKeyDown(char key)
    {
        return IsKeyDown((Key)(int)key);
    }

    /// <summary> Just calls C# Random function used mostly for examples <br/> Random.Shared.Next(min, max + 1) </summary>
    public static int GetRandomValue(int min, int max)
    {
        return Random.Shared.Next(min, max + 1);
    }

    /// <summary> Just calls C# Random function used mostly for examples <br/> Random.Shared.NextSingle() * (max - min) + min </summary>
    public static float GetRandomValue(float min, float max)
    {
        return (Random.Shared.NextSingle() * (max - min)) + min;
    }

    /// <summary> Converts degrees to radians </summary>
    public static float DEG2RAD => MathF.PI / 180.0f;

    /// <summary> Set custom trace log </summary>
    [UnsupportedOSPlatform("browser")]
    public static void SetTraceLogCallback(TraceLogCallback callback)
    {
        traceLogCallback = callback;
    }

    /// <summary> Set custom trace log </summary>
    [UnsupportedOSPlatform("browser")]
    [LibraryImport(LIB, EntryPoint = "SetTraceLogCallback")]
    private static partial void _SetTraceLogCallback(delegate* unmanaged[Cdecl]<int, sbyte*, sbyte*, void> callback);

    /// <summary> Set shader uniform value </summary>
    [LibraryImport(LIB, EntryPoint = "SetShaderValue")]
    public static partial void SetShaderValue(Shader shader, int locIndex, ref Vector2 value, ShaderUniformDataType uniformType);

    /// <summary> Set shader uniform value </summary>
    [LibraryImport(LIB, EntryPoint = "SetShaderValue")]
    public static partial void SetShaderValue(Shader shader, int locIndex, ref Vector4 value, ShaderUniformDataType uniformType);
}
