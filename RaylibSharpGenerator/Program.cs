using System.Globalization;
using System.Text;
using System.Text.Json;

using Raylib.Generator;

internal class Program
{
    private static readonly string[] SkipStructs = {
        "Vector4",
        "Vector3",
        "Vector2",
        "Quaternion",
        "Color",
    };

    private static readonly string[] DebugInclude = {
        "InitWindow",
        "SetTargetFPS",
        "WindowShouldClose",
        "BeginDrawing",
        "ClearBackground",
        "DrawText",
        "EndDrawing",
        "CloseWindow",
    };

    private static string ToCSharpType(string type)
    {
        type = type.Replace(" *", "*");

        type = type switch
        {
            "..." => "params object[]",
            "va_list" => "params object[]",
            "byte *" => "byte[]",
            "float[2]" => "Vector2",
            "float[4]" => "Vector4",

            "Matrix[2]" => "Matrix",// Handled manually
            "char[32]" => "string",

            "unsigned char" => "byte",
            "unsigned char*" => "byte*",
            "unsigned int" => "uint",
            "unsigned int*" => "uint*",

            "const char*" => StringMarshal + " string",
            "Color" => "[MarshalUsing(typeof(ColorMarshaller))] Color",

            // Remove aliases
            "TextureCubemap" => "Texture",
            "Camera" => "Camera3D",
            "Texture2D" => "Texture",
            "RenderTexture2D" => "RenderTexture",
            _ => UnhandledTypeConversions(type),
        };

        return type;
    }

    private static readonly string StringMarshal = "[MarshalAs(UnmanagedType.LPStr)]";
    private static readonly string BoolMarshal = "MarshalAs(UnmanagedType.I1)";

    private static readonly bool DebugOutput;

    private static void Main()
    {
        string jsonString = File.ReadAllText("api.json");

        RaylibApi? api = JsonSerializer.Deserialize<RaylibApi>(jsonString);


        if (api is null)
        {
            Console.Error.WriteLine("Failed to deserialize api.json");
            return;
        }


        // EmitEnums(api);
        // EmitStructs(api);
        // EmitCallbacks(api);
        EmitFunctions(api);


    }

    private static string ToPascalCase(string name)
    {
        string[] words = name.Split("_");
        words = words.Select(w => w[..1].ToUpper(CultureInfo.CurrentCulture) + w[1..].ToLower(CultureInfo.CurrentCulture)).ToArray();
        return string.Join("", words);
    }



    private static string UnhandledTypeConversions(string type)
    {
        if (type.EndsWith("*"))
        {
            return "IntPtr";
        }

        return type;
    }

    private static void EmitEnums(RaylibApi api)
    {
        StringBuilder sb = new();

        foreach (EnumDef e in api.Enums)
        {
            sb.Clear();
            sb.AppendLine($"namespace Raylib;");
            sb.AppendLine();
            sb.AppendLine("#pragma warning disable CA1711");
            sb.AppendLine();
            sb.AppendLine($"/// <summary> {e.Description} </summary>");
            sb.AppendLine($"public enum {e.Name}");
            sb.AppendLine("{");

            foreach (ValueElement value in e.Values)
            {
                string name = ToPascalCase(value.Name);

                if (name.StartsWith(e.Name, true, CultureInfo.CurrentCulture))
                {
                    name = name[e.Name.Length..];
                }
                sb.AppendLine($"    /// <summary> {value.Description} </summary>");
                sb.AppendLine($"    {name} = {value.Value},");
            }

            sb.AppendLine("}");
            sb.AppendLine("#pragma warning restore CA1711");

            File.WriteAllText("../RaylibSharp/Enums/" + e.Name + ".cs", sb.ToString());
        }
    }

    private static void EmitStructs(RaylibApi api)
    {
        StringBuilder sb = new();
        foreach (Struct s in api.Structs)
        {
            if (SkipStructs.Contains(s.Name))
            {
                continue;
            }

            sb.Clear();
            sb.AppendLine($"namespace Raylib;");
            sb.AppendLine();
            sb.AppendLine("#pragma warning disable CA1711,IDE0005");
            sb.AppendLine();
            sb.AppendLine("using System.Runtime.InteropServices;");
            sb.AppendLine("using System.Numerics;");
            sb.AppendLine("using System.Drawing;");
            sb.AppendLine();
            sb.AppendLine($"/// <summary> {s.Description} </summary>");
            sb.AppendLine($"[StructLayout(LayoutKind.Sequential)]");
            sb.AppendLine($"public unsafe struct {s.Name}");
            sb.AppendLine("{");

            foreach (Alias field in s.Fields)
            {
                string pascalName = ToPascalCase(field.Name);

                if (pascalName.StartsWith(s.Name, true, CultureInfo.CurrentCulture))
                {
                    pascalName = pascalName[s.Name.Length..];
                }

                if (field.Type == "Matrix[2]")
                {
                    sb.AppendLine($"    /// <summary> {field.Description} </summary>");
                    sb.AppendLine($"    public Matrix /* {field.Type} */ {pascalName}1;");
                }

                sb.AppendLine($"    /// <summary> {field.Description} </summary>");

                if (field.Type == "char[32]")
                {
                    sb.AppendLine("    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]");
                }

                string type = ToCSharpType(field.Type);

                sb.AppendLine($"    public {type} /* {field.Type} */ {pascalName};");
            }

            sb.AppendLine("}");
            sb.AppendLine();
            sb.AppendLine("#pragma warning restore CA1711,IDE0005");

            File.WriteAllText("../RaylibSharp/Structs/" + s.Name + ".cs", sb.ToString());
        }
    }

    private static void EmitCallbacks(RaylibApi api)
    {
        StringBuilder sb = new();

        sb.AppendLine($"namespace Raylib;");

        foreach (Function c in api.Callbacks)
        {
            string pascalName = ToPascalCase(c.Name);

            sb.AppendLine($"/// <summary> {c.Description} </summary>");
            string parameters = "";
            if (c.Params is not null)
            {
                parameters = string.Join(", ", c.Params.Select(p =>
                {
                    string type = ToCSharpType(p.Type);
                    return $"{type} /* {p.Type} */ {p.Name}";
                }));
            }

            string type = ToCSharpType(c.ReturnType);
            sb.AppendLine($"public delegate {type} {c.Name}({parameters});");
            sb.AppendLine("");
        }

        File.WriteAllText("../RaylibSharp/Callbacks.g.cs", sb.ToString());
    }

    private static void EmitFunctions(RaylibApi api)
    {
        StringBuilder sb = new();

        sb.AppendLine($"namespace Raylib;");
        sb.AppendLine();
        sb.AppendLine("using System.Runtime.InteropServices;");
        sb.AppendLine("using System.Runtime.InteropServices.Marshalling;");
        sb.AppendLine("using System.Numerics;");
        sb.AppendLine("using System.Drawing;");
        sb.AppendLine();
        sb.AppendLine($"public static unsafe partial class RL");
        sb.AppendLine("{");

        foreach (Function f in api.Functions)
        {
            if (!DebugInclude.Contains(f.Name))
            {
                continue;
            }

            string pascalName = ToPascalCase(f.Name);

            sb.AppendLine($"    /// <summary> {f.Description} </summary>");
            string parameters = "";
            if (f.Params is not null)
            {
                parameters = string.Join(", ", f.Params.Select(p =>
                {
                    string type = ToCSharpType(p.Type);
                    return $"{type} /* {p.Type} */ {p.Name}";
                }));
            }

            string type = ToCSharpType(f.ReturnType);

            string debug = string.Join(" ", f.Params?.Select(p => p.Type + " " + p.Name) ?? Array.Empty<string>());
            if (debug.Length > 0 && DebugOutput)
            {
                sb.AppendLine($"    /// {debug}");
            }
            sb.AppendLine($"    [LibraryImport(\"raylib\")]");

            if (type == "bool")
            {
                sb.AppendLine($"    [return: {BoolMarshal}]");
            }

            if (type == "void")
            {
                sb.AppendLine($"    public static partial void {f.Name}({parameters});");
            }
            else
            {
                sb.AppendLine($"    public static partial {type} {f.Name}({parameters});");
            }
            sb.AppendLine("");
        }

        sb.AppendLine("}");

        File.WriteAllText("../RaylibSharp/Raylib.g.cs", sb.ToString());
    }
}
