namespace RaylibSharp.Generator;

using System;
using System.Globalization;
using System.Text;
using System.Text.Json;

public static class StructProcessor
{
    private static Dictionary<string, StructConfig> structConfig = new();

    private static readonly string[] Ignore = {
        "Vector4",
        "Vector3",
        "Vector2",
        "Quaternion",
        "Color",
        "Matrix",
        "Rectangle",
    };

    private static HashSet<string> generated = new();

    public static void Emit(RaylibApi api)
    {
        structConfig = JsonSerializer.Deserialize<Dictionary<string, StructConfig>>(File.ReadAllText("./StructConfig.jsonc"), new JsonSerializerOptions { ReadCommentHandling = JsonCommentHandling.Skip })!;

        StringBuilder sb = new();
        foreach (Struct s in api.Structs)
        {
            if (!generated.Add(s.Name))
            {
                continue;
            }

            StructConfig config = GetConfig(s.Name);

            if (Ignore.Contains(s.Name))
            {
                continue;
            }

            sb.Clear();
            sb.AppendLine($"namespace {api.Namespace};");
            sb.AppendLine();
            sb.AppendLine("#pragma warning disable CA1711,IDE0005,CA1051");
            sb.AppendLine();
            sb.AppendLine("using System.Runtime.InteropServices;");
            sb.AppendLine("using System.Numerics;");
            sb.AppendLine("using System.Drawing;");
            sb.AppendLine("using System.Runtime.InteropServices.Marshalling;");
            sb.AppendLine();

            if (s.Name.StartsWith("rl"))
            {
                s.Name = s.Name[2..];
            }

            if (config.GenManaged)
            {
                ManagedStruct(sb, s, config);
            }

            if (config.GenUnmanaged)
            {
                UnmanagedStruct(sb, s);
            }

            sb.AppendLine("#pragma warning restore CA1711,IDE0005");

            File.WriteAllText(Path.Join("../RaylibSharp/gen/Structs/", api.Directory, s.Name + ".cs"), sb.ToString());
        }
    }

    private static void UnmanagedStruct(StringBuilder sb, Struct s)
    {
        sb.AppendLine($"/// <summary> {s.Description} </summary>");
        sb.AppendLine($"[StructLayout(LayoutKind.Sequential)]");
        sb.AppendLine($"internal unsafe struct Unmanaged{s.Name}");
        sb.AppendLine("{");

        foreach (Fields field in s.Fields)
        {
            string titleCaseName = char.ToUpper(field.Name[0]) + field.Name[1..];

            if (titleCaseName.StartsWith(s.Name, true, CultureInfo.CurrentCulture))
            {
                titleCaseName = titleCaseName[s.Name.Length..];
            }

            string type = ConvertUnmanagedTypeStruct(field.Type);

            string[] parts = field.Type.Split('[');
            string fixedArray = "";

            if (parts.Length > 1)
            {
                fixedArray = "[" + parts[1];
            }

            if (field.Type == "unsigned int[4]")
            {
                for (int i = 0; i < 4; i++)
                {
                    sb.AppendLine($"    /// <summary> {field.Description} </summary>");
                    sb.AppendLine($"    public uint {titleCaseName}{i};");
                }
            }
            else if (field.Type == "Matrix[2]")
            {
                sb.AppendLine($"    /// <summary> {field.Description} </summary>");
                sb.AppendLine($"    public fixed float {titleCaseName}L[16];");
                sb.AppendLine($"    /// <summary> {field.Description} </summary>");
                sb.AppendLine($"    public fixed float {titleCaseName}R[16];");
            }
            else
            {
                sb.AppendLine($"    /// <summary> {field.Description} </summary>");
                if (
                    type.Contains("int") ||
                    type.Contains("byte") ||
                    type.Contains("short") ||
                    type.Contains("float") ||
                    type.Contains("Rectangle") ||
                    type.Contains("Matrix") ||
                    type.Contains("Color") ||
                    type.Contains("Vector") ||
                    type.Contains("char") ||
                    type.Contains("IntPtr") ||
                    type.Contains("bool") ||
                    type.Contains("GlyphInfo") ||
                    type.Contains("DrawCall") ||
                    type.Contains("Quaternion") ||
                    type.Contains("Texture") ||
                    type.Contains("Image") ||
                    type.Contains("void")
                )
                {
                    sb.AppendLine($"    public {type} {titleCaseName}{fixedArray};");
                }
                else
                {
                    sb.AppendLine($"    public Unmanaged{type} {titleCaseName};");
                }
            }
        }

        sb.AppendLine("}");
        sb.AppendLine();
    }

    private static void ManagedStruct(StringBuilder sb, Struct s, StructConfig config)
    {
        sb.AppendLine($"/// <summary> {s.Description} </summary>");

        if (config.UnmanagedAttribute)
        {
            sb.AppendLine($"[NativeMarshalling(typeof({s.Name}Marshaller))]");
        }

        sb.AppendLine($"public unsafe partial struct {s.Name}");
        sb.AppendLine("{");

        foreach (Fields field in s.Fields)
        {
            string titleCaseName = char.ToUpper(field.Name[0]) + field.Name[1..];

            if (titleCaseName.StartsWith(s.Name, true, CultureInfo.CurrentCulture))
            {
                titleCaseName = titleCaseName[s.Name.Length..];
            }

            if (field.Type == "Matrix[2]")
            {
                sb.AppendLine($"    /// <summary> {field.Description} </summary>");
                sb.AppendLine($"    public Matrix4x4 {titleCaseName}L;");
                sb.AppendLine($"    /// <summary> {field.Description} </summary>");
                sb.AppendLine($"    public Matrix4x4 {titleCaseName}R;");
            }
            else if (field.Type == "unsigned int[4]")
            {
                for (int i = 0; i < 4; i++)
                {
                    sb.AppendLine($"    /// <summary> {field.Description} </summary>");
                    sb.AppendLine($"    public uint {titleCaseName}{i};");
                }
            }
            else
            {
                sb.AppendLine($"    /// <summary> {field.Description} </summary>");

                string type = ConvertManagedTypeStruct(field.Type);
                if (field.Name == "projection" && s.Name == "Camera3D")
                {
                    type = "CameraProjection";
                }
                else if (field.Name == "format" && s.Name == "Texture")
                {
                    type = "PixelFormat";
                }

                sb.AppendLine($"    public {type} {titleCaseName};");
            }
        }

        if (config.AdditionalProperties != null)
        {
            foreach (string item in config.AdditionalProperties)
            {
                sb.AppendLine($"    {item}");
            }
        }

        sb.AppendLine("}");
        sb.AppendLine();
    }


    private static string ConvertManagedTypeStruct(string t)
    {
        t = t.Replace(" *", "*");

        return t switch
        {
            "float[2]" => "Vector2",
            "float[3]" => "Vector3",
            "float[4]" => "Vector4",
            "Matrix[2]" => "fixed Matrix4x4",
            "char[32]" => "string",

            "void*" => "IntPtr",

            "Rectangle*" => "RectangleF[]",
            "GlyphInfo*" => "GlyphInfo[]",
            "MaterialMap*" => "MaterialMap[]",

            "int*" => "int[]",
            "uint*" => "uint[]",
            "float*" => "float[]",

            "rAudioBuffer*" => "IntPtr",
            "rAudioProcessor*" => "IntPtr",

            "rlVertexBuffer*" => "VertexBuffer[]",
            "rlDrawCall*" => "DrawCall[]",

            "Transform**" => "Transform[][]",

            "Mesh*" => "Mesh[]",
            "Material*" => "Material[]",
            "BoneInfo*" => "BoneInfo[]",
            "Transform*" => "Transform[]",

            "unsigned char*" => "byte[]",
            "unsigned short*" => "short[]",
            "unsigned int" => "uint",
            "unsigned int*" => "uint[]",

            "char**" => "string[]",

            _ => Utility.ConvertTypeRemoveAlias(t),
        };
    }

    public static string ConvertUnmanagedTypeStruct(string t)
    {
        t = t.Replace(" *", "*");

        if (t.StartsWith("rl"))
        {
            t = t[2..];
        }

        return t switch
        {
            "float[2]" => "fixed float",
            "float[4]" => "fixed float",
            "Matrix[2]" => "fixed Matrix4x4",
            "char[32]" => "fixed char",
            "Rectangle*" => "RectangleF*",

            "Color" => "uint",

            "rAudioBuffer*" => "IntPtr",
            "rAudioProcessor*" => "IntPtr",

            "rlVertexBuffer*" => "VertexBuffer*",

            "unsigned char*" => "byte*",
            "unsigned short*" => "short*",
            "unsigned int" => "uint",
            "unsigned int*" => "uint*",

            "char**" => "sbyte**",

            _ => Utility.ConvertTypeRemoveAlias(t),
        };
    }

    private static StructConfig GetConfig(string name)
    {
        if (structConfig.TryGetValue(name, out StructConfig? val))
        {
            return val;
        }
        else
        {
            return new();
        }
    }
}
