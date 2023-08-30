namespace RaylibSharp;

using System.Drawing;
using System.Runtime.InteropServices;

public static unsafe partial class Raylib
{
    /// <summary> Load color data from image as a Color array </summary>
    public static Color[] LoadImageColors(Image image)
    {
        UnmanagedColor* pixels = _LoadImageColors(image);
        Color[] color = new Color[image.Width * image.Height];

        for (int i = 0; i < color.Length; i++)
        {
            color[i] = Color.FromArgb(pixels[i].R, pixels[i].G, pixels[i].B, pixels[i].A);
        }

        _UnloadImageColors(pixels);

        return color;
    }

    /// <summary> Load color data from image as a Color array (RGBA - 32bit) </summary>
    [LibraryImport(LIB, EntryPoint = "LoadImageColors")]
    private static partial UnmanagedColor* _LoadImageColors(Image image);

    /// <summary> Unload color data loaded with LoadImageColors() </summary>
    [LibraryImport(LIB, EntryPoint = "UnloadImageColors")]
    private static partial void _UnloadImageColors(UnmanagedColor* colors);
}
