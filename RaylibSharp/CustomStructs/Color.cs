namespace RaylibSharp;

using System.Runtime.InteropServices;

/// <summary> Color type, RGBA (32bit) </summary>
[StructLayout(LayoutKind.Explicit)]
public struct Color
{
    /// <summary> Red </summary>
    [FieldOffset(0)] public byte R;
    /// <summary> Green </summary>
    [FieldOffset(1)] public byte G;
    /// <summary> Blue </summary>
    [FieldOffset(2)] public byte B;
    /// <summary> Alpha (0-255) </summary>
    [FieldOffset(3)] public byte A;

    /// <summary> Color constructor (RGBA) </summary>
    public Color(byte r, byte g, byte b, byte a = 255)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    /// <summary> Color constructor (RGBA) </summary>
    public Color(byte gray, byte a = 255)
    {
        R = gray;
        G = gray;
        B = gray;
        A = a;
    }

    /// <summary> Color constructor (RGBA) ints </summary>
    public Color(int r, int g, int b, int a = 255)
    {
        R = Convert.ToByte(r);
        G = Convert.ToByte(g);
        B = Convert.ToByte(b);
        A = Convert.ToByte(a);
    }

    /// <summary> Color constructor (RGBA) </summary>
    public Color(uint rgba)
    {
        A = (byte)(rgba & 0xFF);
        B = (byte)((rgba >> 8) & 0xFF);
        G = (byte)((rgba >> 16) & 0xFF);
        R = (byte)((rgba >> 24) & 0xFF);
    }

    /// <summary> Returns hexadecimal value for a Color </summary>
    public override readonly string ToString()
    {
        return $"0x{R:X2}{G:X2}{B:X2}{A:X2}";
    }

    /// <summary> LightGray from Raylibs color pallet  </summary>
    public static readonly Color LightGray = new(200, 200, 200, 255);
    /// <summary> Gray from Raylibs color pallet  </summary>
    public static readonly Color Gray = new(130, 130, 130, 255);
    /// <summary> DarkGray from Raylibs color pallet  </summary>
    public static readonly Color DarkGray = new(80, 80, 80, 255);
    /// <summary> Yellow from Raylibs color pallet  </summary>
    public static readonly Color Yellow = new(253, 249, 0, 255);
    /// <summary> Gold from Raylibs color pallet  </summary>
    public static readonly Color Gold = new(255, 203, 0, 255);
    /// <summary> Orange from Raylibs color pallet  </summary>
    public static readonly Color Orange = new(255, 161, 0, 255);
    /// <summary> Pink from Raylibs color pallet  </summary>
    public static readonly Color Pink = new(255, 109, 194, 255);
    /// <summary> Red from Raylibs color pallet  </summary>
    public static readonly Color Red = new(230, 41, 55, 255);
    /// <summary> Maroon from Raylibs color pallet  </summary>
    public static readonly Color Maroon = new(190, 33, 55, 255);
    /// <summary> Green from Raylibs color pallet  </summary>
    public static readonly Color Green = new(0, 228, 48, 255);
    /// <summary> Lime from Raylibs color pallet  </summary>
    public static readonly Color Lime = new(0, 158, 47, 255);
    /// <summary> DarkGreen from Raylibs color pallet  </summary>
    public static readonly Color DarkGreen = new(0, 117, 44, 255);
    /// <summary> SkyBlue from Raylibs color pallet  </summary>
    public static readonly Color SkyBlue = new(102, 191, 255, 255);
    /// <summary> Blue from Raylibs color pallet  </summary>
    public static readonly Color Blue = new(0, 121, 241, 255);
    /// <summary> DarkBlue from Raylibs color pallet  </summary>
    public static readonly Color DarkBlue = new(0, 82, 172, 255);
    /// <summary> Purple from Raylibs color pallet  </summary>
    public static readonly Color Purple = new(200, 122, 255, 255);
    /// <summary> Violet from Raylibs color pallet  </summary>
    public static readonly Color Violet = new(135, 60, 190, 255);
    /// <summary> DarkPurple from Raylibs color pallet  </summary>
    public static readonly Color DarkPurple = new(112, 31, 126, 255);
    /// <summary> Beige from Raylibs color pallet  </summary>
    public static readonly Color Beige = new(211, 176, 131, 255);
    /// <summary> Brown from Raylibs color pallet  </summary>
    public static readonly Color Brown = new(127, 106, 79, 255);
    /// <summary> DarkBrown from Raylibs color pallet  </summary>
    public static readonly Color DarkBrown = new(76, 63, 47, 255);
    /// <summary> White from Raylibs color pallet  </summary>
    public static readonly Color White = new(255, 255, 255, 255);
    /// <summary> Black from Raylibs color pallet  </summary>
    public static readonly Color Black = new(0, 0, 0, 255);
    /// <summary> Blank from Raylibs color pallet  </summary>
    public static readonly Color Blank = new(0, 0, 0, 0);
    /// <summary> Magenta from Raylibs color pallet  </summary>
    public static readonly Color Magenta = new(255, 0, 255, 255);
    /// <summary> RayWhite from Raylibs color pallet  </summary>
    public static readonly Color RayWhite = new(245, 245, 245, 255);
}
