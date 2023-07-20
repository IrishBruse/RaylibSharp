namespace RaylibSharp.Generator;

using System.Globalization;
using System.Text;

public static class EnumProcessor
{
    public static void Emit(RaylibApi api)
    {
        StringBuilder sb = new();

        foreach (EnumDef e in api.Enums)
        {
            if (e.Name == "KeyboardKey")
            {
                e.Name = "Key";
            }
            else if (e.Name == "ConfigFlags")
            {
                e.Name = "WindowFlag";
            }

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
                else if (name.StartsWith("Camera", true, CultureInfo.CurrentCulture))
                {
                    name = name[6..];
                }
                else if (e.Name == "WindowFlag")
                {
                    name = name[4..];
                    if (name.StartsWith("Window"))
                    {
                        name = name[6..];
                    }
                }
                else if (e.Name == "TraceLogLevel")
                {
                    name = name[3..];
                }

                sb.AppendLine($"    /// <summary> {value.Description} </summary>");
                sb.AppendLine($"    {name} = {value.Value},");
            }

            sb.AppendLine("}");
            sb.AppendLine();
            sb.AppendLine("#pragma warning restore CA1711");

            File.WriteAllText("../RaylibSharp/Enums/" + e.Name + ".cs", sb.ToString());
        }
    }
}
