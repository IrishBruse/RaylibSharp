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

    /// <summary> LightGray from Raylibs color pallet  </summary>
    public static readonly Color LightGray = Color.LightGray;
    /// <summary> Gray from Raylibs color pallet  </summary>
    public static readonly Color Gray = Color.Gray;
    /// <summary> DarkGray from Raylibs color pallet  </summary>
    public static readonly Color DarkGray = Color.DarkGray;
    /// <summary> Yellow from Raylibs color pallet  </summary>
    public static readonly Color Yellow = Color.Yellow;
    /// <summary> Gold from Raylibs color pallet  </summary>
    public static readonly Color Gold = Color.Gold;
    /// <summary> Orange from Raylibs color pallet  </summary>
    public static readonly Color Orange = Color.Orange;
    /// <summary> Pink from Raylibs color pallet  </summary>
    public static readonly Color Pink = Color.Pink;
    /// <summary> Red from Raylibs color pallet  </summary>
    public static readonly Color Red = Color.Red;
    /// <summary> Maroon from Raylibs color pallet  </summary>
    public static readonly Color Maroon = Color.Maroon;
    /// <summary> Green from Raylibs color pallet  </summary>
    public static readonly Color Green = Color.Green;
    /// <summary> Lime from Raylibs color pallet  </summary>
    public static readonly Color Lime = Color.Lime;
    /// <summary> DarkGreen from Raylibs color pallet  </summary>
    public static readonly Color DarkGreen = Color.DarkGreen;
    /// <summary> SkyBlue from Raylibs color pallet  </summary>
    public static readonly Color SkyBlue = Color.SkyBlue;
    /// <summary> Blue from Raylibs color pallet  </summary>
    public static readonly Color Blue = Color.Blue;
    /// <summary> DarkBlue from Raylibs color pallet  </summary>
    public static readonly Color DarkBlue = Color.DarkBlue;
    /// <summary> Purple from Raylibs color pallet  </summary>
    public static readonly Color Purple = Color.Purple;
    /// <summary> Violet from Raylibs color pallet  </summary>
    public static readonly Color Violet = Color.Violet;
    /// <summary> DarkPurple from Raylibs color pallet  </summary>
    public static readonly Color DarkPurple = Color.DarkPurple;
    /// <summary> Beige from Raylibs color pallet  </summary>
    public static readonly Color Beige = Color.Beige;
    /// <summary> Brown from Raylibs color pallet  </summary>
    public static readonly Color Brown = Color.Brown;
    /// <summary> DarkBrown from Raylibs color pallet  </summary>
    public static readonly Color DarkBrown = Color.DarkBrown;
    /// <summary> White from Raylibs color pallet  </summary>
    public static readonly Color White = Color.White;
    /// <summary> Black from Raylibs color pallet  </summary>
    public static readonly Color Black = Color.Black;
    /// <summary> Blank from Raylibs color pallet  </summary>
    public static readonly Color Blank = Color.Blank;
    /// <summary> Magenta from Raylibs color pallet  </summary>
    public static readonly Color Magenta = Color.Magenta;
    /// <summary> RayWhite from Raylibs color pallet  </summary>
    public static readonly Color RayWhite = Color.RayWhite;
}
