namespace Raylib;

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Numerics;
using System.Drawing;

public static unsafe partial class RL
{
    /// <summary> Initialize window and OpenGL context </summary>
    [LibraryImport("raylib")]
    public static partial void InitWindow(int /* int */ width, int /* int */ height, [MarshalAs(UnmanagedType.LPStr)] string /* const char * */ title);

    /// <summary> Check if KEY_ESCAPE pressed or Close icon pressed </summary>
    [LibraryImport("raylib")]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool WindowShouldClose();

    /// <summary> Close window and unload OpenGL context </summary>
    [LibraryImport("raylib")]
    public static partial void CloseWindow();

    /// <summary> Set background color (framebuffer clear color) </summary>
    [LibraryImport("raylib")]
    public static partial void ClearBackground([MarshalUsing(typeof(ColorMarshaller))] Color /* Color */ color);

    /// <summary> Setup canvas (framebuffer) to start drawing </summary>
    [LibraryImport("raylib")]
    public static partial void BeginDrawing();

    /// <summary> End canvas drawing and swap buffers (double buffering) </summary>
    [LibraryImport("raylib")]
    public static partial void EndDrawing();

    /// <summary> Set target FPS (maximum) </summary>
    [LibraryImport("raylib")]
    public static partial void SetTargetFPS(int /* int */ fps);

    /// <summary> Draw text (using default font) </summary>
    [LibraryImport("raylib")]
    public static partial void DrawText([MarshalAs(UnmanagedType.LPStr)] string /* const char * */ text, int /* int */ posX, int /* int */ posY, int /* int */ fontSize, [MarshalUsing(typeof(ColorMarshaller))] Color /* Color */ color);

}
