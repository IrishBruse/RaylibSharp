namespace RaylibSharp.Generator;

using System.Globalization;

public class Utility
{
    public static readonly string StringMarshal = "MarshalAs(UnmanagedType.LPStr)";
    public static readonly string BoolMarshal = "MarshalAs(UnmanagedType.I1)";
    public static readonly string ColorMarshal = "MarshalUsing(typeof(ColorMarshaller))";
    public static readonly string MeshMarshal = "MarshalUsing(typeof(MeshMarshaller))";
    public static readonly string ModelAnimationMarshal = "MarshalUsing(typeof(ModelAnimationMarshaller))";

    public static readonly string[] Colors = {
        "RayWhite",
        "LightGray",
        "DarkGray",
        "Gray",
        "Yellow",
        "Gold",
        "Orange",
        "Pink",
        "Red",
        "Maroon",
        "DarkGreen",
        "Green",
        "Lime",
        "SkyBlue",
        "DarkBlue",
        "Blue",
        "DarkPurple",
        "Purple",
        "Violet",
        "Beige",
        "DarkBrown",
        "Brown",
        "White",
        "Black",
        "Blank",
        "Magenta",
    };

    public static readonly string[] Keys = {
        "Null","Apostrophe",
        "Comma","Minus",
        "Period","Slash",
        "Zero","One",
        "Two","Three",
        "Four","Five",
        "Six","Seven",
        "Eight","Nine",
        "Semicolon","Key:",
        "Equal",
        "A","B","C",
        "D","E","F",
        "G","H","I",
        "J","K","L",
        "M","N","O",
        "P","Q","R",
        "S","T","U",
        "V","W","X",
        "Y","Z",
        "LeftBracket","Backslash",
        "RightBracket","Grave",
        "Space","Escape",
        "Enter","Tab",
        "Backspace","Insert",
        "Delete","Right",
        "Left","Down",
        "Up","PageUp",
        "PageDown","Home",
        "End","CapsLock",
        "ScrollLock","NumLock",
        "PrintScreen","Pause",
        "F1","F2",
        "F3","F4",
        "F5","F6",
        "F7","F8",
        "F9","F10",
        "F11","F12",
        "LeftShift","LeftControl",
        "LeftAlt","LeftSuper",
        "RightShift","RightControl",
        "RightAlt","RightSuper",
        "KbMenu","Kp0",
        "Kp1","Kp2",
        "Kp3","Kp4",
        "Kp5","Kp6",
        "Kp7","Kp8",
        "Kp9","KpDecimal",
        "KpDivide","KpMultiply",
        "KpSubtract","KpAdd",
        "KpEnter","KpEqual",
        "Back","Menu",
        "VolumeUp","VolumeDown"
    };

    public static string ConvertTypeFunction(string t)
    {
        t = t.Replace(" *", "*");

        t = t switch
        {
            "..." => "params object[]",
            "va_list" => "params object[]",
            "float[2]" => "Vector2",
            "float[4]" => "Vector4",

            "Matrix[2]" => "Matrix",// Handled manually
            "char[32]" => "string",

            "const unsigned char*" => "byte[]",

            "unsigned char" => "byte",
            "unsigned char*" => "byte*",
            "unsigned int" => "uint",
            "unsigned int*" => "uint*",

            "Camera*" => "ref Camera3D",

            "const char*" => "string",

            _ => HandleFunctionTypeConversions(t),
        };


        return ConvertTypeRemoveAlias(t);
    }

    private static string HandleFunctionTypeConversions(string type)
    {
        if (type.EndsWith("*"))
        {
            return "IntPtr";
        }

        return type;
    }

    public static string ConvertTypeRemoveAlias(string t)
    {
        return t switch
        {
            "TextureCubemap" => "Texture",
            "Camera" => "Camera3D",
            "Texture2D" => "Texture",
            "RenderTexture2D" => "RenderTexture",
            "Matrix" => "Matrix4x4",
            "Rectangle" => "RectangleF",
            _ => t,
        };
    }

    public static string ToPascalCase(string name)
    {
        string[] words = name.Split("_");
        words = words.Select(w => w[..1].ToUpper(CultureInfo.CurrentCulture) + w[1..].ToLower(CultureInfo.CurrentCulture)).ToArray();
        return string.Join("", words);
    }
}
