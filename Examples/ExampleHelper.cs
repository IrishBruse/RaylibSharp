using System.Drawing;

using RaylibSharp;

/// <summary>
/// Helper class for examples
/// Used to override InitWindow and CloseWindow
/// so it only uses 1 window for all the examples
/// </summary>
public class ExampleHelper
{
    public static void InitWindow(int width, int height, string title)
    {
        Raylib.SetWindowTitle(title);
        Raylib.SetWindowSize(width, height);
        Raylib.SetMouseCursor(MouseCursor.Default);
        Raylib.SetConfigFlags(WindowFlag.Msaa4xHint | WindowFlag.VsyncHint);
    }

    public static void CloseWindow() { }

    public static bool WindowShouldClose()
    {
        return Raylib.WindowShouldClose() || Raylib.IsKeyPressed(Key.Escape);
    }

    public static bool IsFileExtension(string fileName, string extension)
    {
        return fileName.EndsWith(extension);
    }

    // Temp gui stubs TODO: replace when raygui is ported

    public static float GuiSliderBar(RectangleF rect, string textLeft, string textRight, float value, float minAngle, float maxAngle)
    {
        return 0;
    }

    public static bool GuiCheckBox(RectangleF rect, string text, bool check)
    {
        return false;
    }

    public static float EaseElasticOut(float t, float b, float c, float d)
    {
        return 0.5f;
    }

    public static float EaseElasticIn(float t, float b, float c, float d)
    {
        return 0.5f;
    }

    public static float EaseCubicOut(float t, float b, float c, float d)
    {
        return 0.5f;
    }

    public static float EaseBounceOut(float t, float b, float c, float d)
    {
        return 0.5f;
    }


    public static float EaseQuadOut(float t, float b, float c, float d)
    {
        return 0.5f;
    }

    public static float EaseCircOut(float t, float b, float c, float d)
    {
        return 0.5f;
    }

    public static float EaseSineOut(float t, float b, float c, float d)
    {
        return 0.5f;
    }

    public static float EaseLinearIn(float t, float b, float c, float d)
    {
        return 0.5f;
    }
}
