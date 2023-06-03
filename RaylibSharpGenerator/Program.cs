using System.Globalization;
using System.Text;
using System.Text.Json;

using Raylib.Generator;

string[] skipStructs = {
    "Vector4",
    "Vector3",
    "Vector2",
    "Quaternion",
    "Color",
};

string jsonString = File.ReadAllText("api.json");

RaylibApi? api = JsonSerializer.Deserialize<RaylibApi>(jsonString);


if (api is null)
{
    Console.Error.WriteLine("Failed to deserialize api.json");
    return;
}

StringBuilder sb = new();

EmitEnums(api, sb);
EmitStructs(api, sb);

string ToPascalCase(string name)
{
    string[] words = name.Split("_");
    words = words.Select(w => w[..1].ToUpper(CultureInfo.CurrentCulture) + w[1..].ToLower(CultureInfo.CurrentCulture)).ToArray();
    return string.Join("", words);
}

(string type, string name) ToCSharpType(string type, string name)
{
    if (type.StartsWith("unsigned ", true, CultureInfo.CurrentCulture))
    {
        type = "u" + type[9..];
    }
    else if (type.EndsWith("*"))
    {
        type = "IntPtr";
    }
    else if (type.StartsWith("char["))
    {
        string array = type[4..];
        type = "fixed sbyte";
        name += array;
    }

    return (type, name);
}

void EmitEnums(RaylibApi api, StringBuilder sb)
{
    foreach (Raylib.Generator.Enum e in api.Enums)
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
        sb.AppendLine();
        File.WriteAllText("../RaylibSharp/Enums/" + e.Name + ".cs", sb.ToString());
    }
}


void EmitStructs(RaylibApi api, StringBuilder sb)
{
    foreach (Struct s in api.Structs)
    {
        if (skipStructs.Contains(s.Name))
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
        sb.AppendLine();
        sb.AppendLine($"/// <summary> {s.Description} </summary>");
        sb.AppendLine($"[StructLayout(LayoutKind.Sequential)]");
        sb.AppendLine($"public struct {s.Name}");
        sb.AppendLine("{");

        foreach (Alias field in s.Fields)
        {
            string pascalName = ToPascalCase(field.Name);

            if (pascalName.StartsWith(s.Name, true, CultureInfo.CurrentCulture))
            {
                pascalName = pascalName[s.Name.Length..];
            }

            (string type, string name) = ToCSharpType(field.Type, pascalName);

            sb.AppendLine($"    /// <summary> {field.Description} </summary>");
            sb.AppendLine($"    public {type} /*{field.Type}*/ {name};");
        }

        sb.AppendLine("}");
        sb.AppendLine("#pragma warning restore CA1711,IDE0005");
        sb.AppendLine();

        File.WriteAllText("../RaylibSharp/Structs/" + s.Name + ".cs", sb.ToString());
    }
}
