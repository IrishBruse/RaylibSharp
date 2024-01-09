namespace RaylibSharp;

using System.Runtime.InteropServices;

public static unsafe partial class Raylib
{
    /// <summary> Load color data from image as a Color array </summary>
    public static Color[] LoadImageColors(Image image)
    {
        Color* colors = _LoadImageColors(image);
        ReadOnlySpan<Color> pixels = new(colors, image.Width * image.Height);
        _UnloadImageColors(colors);

        return pixels.ToArray();
    }

    /// <summary> Load color data from image as a Color array (RGBA - 32bit) </summary>
    [LibraryImport(LIB, EntryPoint = "LoadImageColors")]
    private static partial Color* _LoadImageColors(Image image);

    /// <summary> Unload color data loaded with LoadImageColors() </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadImageColors")]
    private static partial void _UnloadImageColors(Color* colors);
}
