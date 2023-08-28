namespace RaylibSharp.Generator;

using System.Text;

public static class DefineProcessor
{
    public static void Emit(RaylibApi api)
    {
        StringBuilder sb = new();

        sb.AppendLine($"namespace {api.Namespace};");
        sb.AppendLine();
        sb.AppendLine($"public static unsafe partial class {api.ClassName}");
        sb.AppendLine("{");
        foreach (Define e in api.Defines)
        {
            if (e.Type == "GUARD" || e.Type == "MACRO" || e.Type == "UNKNOWN" || e.Type == "COLOR")
            {
                continue;
            }

            string pascalName = Utility.ToPascalCase(e.Name);

            if (pascalName == "Pi" || pascalName == "Deg2rad" || pascalName == "Rad2deg")
            {
                continue;
            }

            string type = e.Type.ToLower();

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

            if (string.IsNullOrEmpty(e.Description))
            {
                sb.AppendLine($"    /// <summary> {pascalName} </summary>");
            }
            else
            {
                sb.AppendLine($"    /// <summary> {e.Description} </summary>");
            }

            sb.AppendLine($"    public static readonly {type} {pascalName} = {value};");
        }
        sb.AppendLine("}");
        sb.AppendLine();


        File.WriteAllText(Path.Join("../RaylibSharp/gen/Defs/", api.Directory, "Defines.cs"), sb.ToString());
    }
}
