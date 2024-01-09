using System.Numerics;

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

    public static float GuiSliderBar(Rectangle rect, string textLeft, string? textRight, float? value, float minAngle, float maxAngle)
    {
        return 0;
    }

    public static bool GuiCheckBox(Rectangle rect, string text, bool check)
    {
        return false;
    }

    public static Vector3 Vector3Barycenter(Vector3 p, Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 result = Vector3.Zero;

        Vector3 v0 = new(b.X - a.X, b.Y - a.Y, b.Z - a.Z); // Vector3Subtract(b, a)
        Vector3 v1 = new(c.X - a.X, c.Y - a.Y, c.Z - a.Z); // Vector3Subtract(c, a)
        Vector3 v2 = new(p.X - a.X, p.Y - a.Y, p.Z - a.Z); // Vector3Subtract(p, a)
        float d00 = (v0.X * v0.X) + (v0.Y * v0.Y) + (v0.Z * v0.Z); // Vector3DotProduct(v0, v0)
        float d01 = (v0.X * v1.X) + (v0.Y * v1.Y) + (v0.Z * v1.Z); // Vector3DotProduct(v0, v1)
        float d11 = (v1.X * v1.X) + (v1.Y * v1.Y) + (v1.Z * v1.Z); // Vector3DotProduct(v1, v1)
        float d20 = (v2.X * v0.X) + (v2.Y * v0.Y) + (v2.Z * v0.Z); // Vector3DotProduct(v2, v0)
        float d21 = (v2.X * v1.X) + (v2.Y * v1.Y) + (v2.Z * v1.Z); // Vector3DotProduct(v2, v1)

        float denom = (d00 * d11) - (d01 * d01);

        result.Y = ((d11 * d20) - (d01 * d21)) / denom;
        result.Z = ((d00 * d21) - (d01 * d20)) / denom;
        result.X = 1.0f - (result.Z + result.Y);

        return result;
    }
}
