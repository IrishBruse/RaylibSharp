namespace RaylibSharp.Generator;

using System.Text;
using System.Text.Json;

public class FunctionProcessor
{
    private static FunctionConfig config;
    private const bool DebugOutput = false;

    public static void Emit(RaylibApi api)
    {
        config = JsonSerializer.Deserialize<FunctionConfig>(File.ReadAllText("./FunctionConfig.jsonc"), new JsonSerializerOptions { ReadCommentHandling = JsonCommentHandling.Skip })!;
        StringBuilder sb = new();

        sb.AppendLine($"namespace RaylibSharp;");
        sb.AppendLine();
        sb.AppendLine("using System.Runtime.InteropServices;");
        sb.AppendLine("using System.Runtime.InteropServices.Marshalling;");
        sb.AppendLine("using System.Numerics;");
        sb.AppendLine("using System.Drawing;");
        sb.AppendLine();
        sb.AppendLine($"public static unsafe partial class Raylib");
        sb.AppendLine("{");

        foreach (Function f in api.Functions)
        {
            if (config.Excluded.Contains(f.Name))
            {
                continue;
            }

            string pascalName = Utility.ToPascalCase(f.Name);

            sb.AppendLine($"    /// <summary> {f.Description} </summary>");
            string parameters = "";
            if (f.Params is not null)
            {
                parameters = string.Join(", ", f.Params.Select(p => EmitParameter(p, f)));
            }

            string type = Utility.ConvertTypeFunction(f.ReturnType);

            if (config.FunctionTypeConversion.TryGetValue(f.Name, out Dictionary<string, string>? conversion))
            {
                if (conversion.TryGetValue("_", out string? newReturnType))
                {
                    type = newReturnType;
                }
            }

            string debug = string.Join(" ", f.Params?.Select(p => p.Type + " " + p.Name) ?? Array.Empty<string>());
            if (debug.Length > 0 && DebugOutput)
            {
                sb.AppendLine($"    /// {debug}");
            }
            sb.AppendLine($"    [LibraryImport(\"raylib\")]");

            if (type == "bool")
            {
                sb.AppendLine($"    [return: {Utility.BoolMarshal}]");
            }
            else if (type == "string")
            {
                sb.AppendLine($"    [return: {Utility.StringMarshal}]");
            }
            else if (type == "Color")
            {
                sb.AppendLine($"    [return: {Utility.ColorMarshal}]");
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
        sb.AppendLine();

        File.WriteAllText("../RaylibSharp/Raylib.cs", sb.ToString());
    }

    private static string EmitParameter(Param p, Function f)
    {
        if (config.FunctionTypeConversion.TryGetValue(f.Name, out Dictionary<string, string>? conversion))
        {
            if (conversion.TryGetValue(p.Name, out string? newParam))
            {
                return newParam;
            }
        }

        string type = Utility.ConvertTypeFunction(p.Type);

        if (type == "bool")
        {
            return $"[{Utility.BoolMarshal}] {type} {p.Name}";
        }
        else if (type == "string")
        {
            return $"[{Utility.StringMarshal}] {type} {p.Name}";
        }
        else if (type == "Color")
        {
            return $"[{Utility.ColorMarshal}] {type} {p.Name}";
        }
        else if (type == "Camera3D")
        {
            return $"[{Utility.Camera3DMarshal}] {type} {p.Name}";
        }
        else if (type == "Camera2D")
        {
            return $"[{Utility.Camera2DMarshal}] {type} {p.Name}";
        }


        return $"{type} {p.Name}";
    }
}
