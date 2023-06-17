namespace RaylibSharp.Generator;

using System.Globalization;
using System.Text;
using System.Text.Json;

internal class Program
{
    private static void Main()
    {
        string jsonString = File.ReadAllText("api.json");

        RaylibApi api = JsonSerializer.Deserialize<RaylibApi>(jsonString)!;

        // EmitCallbacks(api);
        // EnumProcessor.Emit(api);
        // StructProcessor.Emit(api);
        FunctionProcessor.Emit(api);

        ExampleGenerator.Emit();
    }

    private static void EmitEnums(RaylibApi api)
    {
        StringBuilder sb = new();

        foreach (EnumDef e in api.Enums)
        {
            sb.Clear();
            sb.AppendLine($"namespace RaylibSharp;");
            sb.AppendLine();
            sb.AppendLine("#pragma warning disable CA1711");
            sb.AppendLine();
            sb.AppendLine($"/// <summary> {e.Description} </summary>");
            sb.AppendLine($"public enum {e.Name}");
            sb.AppendLine("{");

            foreach (ValueElement value in e.Values)
            {
                string name = Utility.ToPascalCase(value.Name);

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

    private static void EmitCallbacks(RaylibApi api)
    {
        StringBuilder sb = new();

        sb.AppendLine($"namespace RaylibSharp;");

        foreach (Function c in api.Callbacks)
        {
            string pascalName = Utility.ToPascalCase(c.Name);

            sb.AppendLine($"/// <summary> {c.Description} </summary>");
            string parameters = "";
            if (c.Params is not null)
            {
                parameters = string.Join(", ", c.Params.Select(p =>
                {
                    string type = Utility.ConvertTypeFunction(p.Type);
                    return $"{type} /* {p.Type.Replace(" *", "*")} */ {p.Name}";
                }));
            }

            string type = Utility.ConvertTypeFunction(c.ReturnType);
            sb.AppendLine($"public delegate {type} {c.Name}({parameters});");
            sb.AppendLine("");
        }

        File.WriteAllText("../RaylibSharp/Callbacks.g.cs", sb.ToString());
    }
}
