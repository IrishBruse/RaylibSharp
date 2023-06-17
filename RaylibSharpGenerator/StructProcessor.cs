namespace RaylibSharp.Generator;

using System.Globalization;
using System.Text;

public static class StructProcessor
{
    private static readonly string[] Ignore = {
        "Vector4",
        "Vector3",
        "Vector2",
        "Quaternion",
        "Color",
        "Matrix",
        "Rectangle",
    };

    public static void Emit(RaylibApi api)
    {
        StringBuilder sb = new();
        foreach (Struct s in api.Structs)
        {
            if (Ignore.Contains(s.Name))
            {
                continue;
            }

            sb.Clear();
            sb.AppendLine($"namespace RaylibSharp;");
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

            foreach (Fields field in s.Fields)
            {
                string pascalName = Utility.ToPascalCase(field.Name);

                if (pascalName.StartsWith(s.Name, true, CultureInfo.CurrentCulture))
                {
                    pascalName = pascalName[s.Name.Length..];
                }

                if (field.Type == "Matrix[2]")
                {
                    sb.AppendLine($"    /// <summary> {field.Description} </summary>");
                    sb.AppendLine($"    public Matrix4x4 {pascalName}1;");
                    sb.AppendLine($"    /// <summary> {field.Description} </summary>");
                    sb.AppendLine($"    public Matrix4x4 {pascalName}2;");
                }
                else
                {

                    sb.AppendLine($"    /// <summary> {field.Description} </summary>");

                    if (field.Type == "char[32]")
                    {
                        sb.AppendLine("    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]");
                    }

                    string type = Utility.ConvertTypeStruct(field.Type);

                    sb.AppendLine($"    public {type} {pascalName};");
                }
            }

            sb.AppendLine("}");
            sb.AppendLine();
            sb.AppendLine("#pragma warning restore CA1711,IDE0005");

            File.WriteAllText("../RaylibSharp/Structs/" + s.Name + ".cs", sb.ToString());
        }
    }
}
