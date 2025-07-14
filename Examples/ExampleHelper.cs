using System;
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
        _ = rect;
        _ = textLeft;
        _ = textRight;
        _ = value;
        _ = minAngle;
        _ = maxAngle;
        return 0;
    }

    public static bool GuiCheckBox(Rectangle rect, string text, bool check)
    {
        _ = rect;
        _ = text;
        _ = check;
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

    public static Vector2 Vector2Scale(Vector2 v, float scale)
    {
        return v * scale;
    }

    public static Vector2 Vector2Add(Vector2 v1, Vector2 v2)
    {
        return v1 + v2;
    }

    public static Vector2 Vector2Subtract(Vector2 v1, Vector2 v2)
    {
        return v1 - v2;
    }

    public static float Vector2Length(Vector2 v)
    {
        return MathF.Sqrt((v.X * v.X) + (v.Y * v.Y));
    }

    public static float fabsf(float x)
    {
        return MathF.Abs(x);
    }

    public static float fminf(float x, float y)
    {
        return MathF.Min(x, y);
    }
    public static float fmaxf(float x, float y)
    {
        return MathF.Max(x, y);
    }

    public static float sinf(float x)
    {
        return MathF.Sin(x);
    }

    public static float cosf(float x)
    {
        return MathF.Cos(x);
    }

    public static float truncf(float x)
    {
        return MathF.Truncate(x);
    }

    public static float MIN(float x, float y)
    {
        return MathF.Min(x, y);
    }

    public static float MAX(float x, float y)
    {
        return MathF.Max(x, y);
    }

    public static float Clamp(float value, float min, float max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }

    public static Vector2 Vector2Clamp(Vector2 value, Vector2 min, Vector2 max)
    {
        return new Vector2(
            Math.Clamp(value.X, ((Vector2)min).X, ((Vector2)max).X),
            Math.Clamp(value.Y, ((Vector2)min).Y, ((Vector2)max).Y)
        );
    }

    public static void DrawRectangleRec(Rectangle rec, Color color)
    {
        Raylib.DrawRectangle(rec, color);
    }

    public static void DrawCircleV(Vector2 center, float radius, Color color)
    {
        Raylib.DrawCircle(center, radius, color);
    }

    public static void DrawTextEx(Font font, string text, Vector2 position, float fontSize, float spacing, Color tint)
    {
        Raylib.DrawText(font, text, position, fontSize, spacing, tint);
    }

    public static void DrawRectangleLinesEx(Rectangle scissorArea, int lineThick, Color black)
    {
        Raylib.DrawRectangleLines(scissorArea, lineThick, black);
    }

    public static bool IsKeyPressed(char c)
    {
        return Raylib.IsKeyPressed((Key)c);
    }

    public static bool IsKeyPressed(Key c)
    {
        return Raylib.IsKeyPressed(c);
    }
}
