namespace RaylibSharp.Generator;

using System.Text;

public static class DefineProcessor
{
    public static void Emit(RaylibApi api)
    {
        Log("Emitting Defines", ConsoleColor.Blue);

        StringBuilder sb = new();

        _ = sb.AppendLine($"namespace {api.Namespace};");
        _ = sb.AppendLine();
        _ = sb.AppendLine($"public static unsafe partial class {api.ClassName}");
        _ = sb.AppendLine("{");
        foreach (Define e in api.Defines)
        {
            if (e.Type is "GUARD" or "MACRO" or "UNKNOWN" or "COLOR")
            {
                continue;
            }

            string pascalName = Utility.ToPascalCase(e.Name);

            if (pascalName is "Pi" or "Deg2rad" or "Rad2deg")
            {
                continue;
            }

            string type = e.Type.ToLowerInvariant();

            if (type.Contains("string"))
            {
                type = "string";
            }
            else if (type.Contains("float"))
            {
                type = "float";
            }

            string value = e.Value.ToString()!;

            if (type == "string")
            {
                value = '"' + value + '"';
            }

            _ = string.IsNullOrEmpty(e.Description)
                ? sb.AppendLine($"    /// <summary> {pascalName} </summary>")
                : sb.AppendLine($"    /// <summary> {e.Description} </summary>");

            Log(pascalName);

            _ = sb.AppendLine($"    public static readonly {type} {pascalName} = {value};");
        }
        _ = sb.AppendLine("}");
        _ = sb.AppendLine();

        File.WriteAllText(Path.Join("../RaylibSharp/gen/Defs/", api.Directory, "Defines.cs"), sb.ToString());

        Console.WriteLine();
    }
}
