namespace RaylibSharp.Generator;

using System.Globalization;
using System.Text;

public static class EnumProcessor
{
    private static HashSet<string> generated = new();

    public static void Emit(RaylibApi api)
    {
        Log("Emitting Enums", ConsoleColor.Blue);

        StringBuilder sb = new();

        foreach (EnumDef e in api.Enums)
        {
            if (e.Name.StartsWith("rl"))
            {
                e.Name = e.Name[2..];
            }

            if (!generated.Add(e.Name))
            {
                Log($"{e.Name} (Skipped)", ConsoleColor.Gray);
                continue;
            }

            if (e.Name == "KeyboardKey")
            {
                e.Name = "Key";
            }
            else if (e.Name == "ConfigFlags")
            {
                e.Name = "WindowFlag";
            }

            Console.WriteLine(e.Name);

            _ = sb.Clear();
            _ = sb.AppendLine($"namespace {api.Namespace};");
            _ = sb.AppendLine();
            _ = sb.AppendLine("#pragma warning disable CA1711");
            _ = sb.AppendLine();
            _ = sb.AppendLine($"/// <summary> {e.Description} </summary>");
            _ = sb.AppendLine($"public enum {e.Name}");
            _ = sb.AppendLine("{");

            foreach (ValueElement value in e.Values)
            {
                string valueName = Utility.ToPascalCase(value.Name);

                if (valueName.StartsWith("rl", true, CultureInfo.CurrentCulture))
                {
                    valueName = valueName[2..];
                }

                if (valueName.StartsWith(e.Name, true, CultureInfo.CurrentCulture))
                {
                    valueName = valueName[e.Name.Length..];
                }
                else if (valueName.StartsWith("Camera"))
                {
                    valueName = valueName[6..];
                }
                else if (e.Name == "BlendMode")
                {
                    valueName = valueName[5..];
                }
                else if (e.Name == "CullMode")
                {
                    valueName = valueName[8..];
                }
                else if (e.Name == "FramebufferAttachType")
                {
                    valueName = valueName[10..];
                }
                else if (e.Name == "FramebufferAttachTextureType")
                {
                    valueName = valueName[10..];
                }
                else if (e.Name == "WindowFlag")
                {
                    valueName = valueName[4..];
                    if (valueName.StartsWith("Window"))
                    {
                        valueName = valueName[6..];
                    }
                }
                else if (e.Name == "TraceLogLevel")
                {
                    valueName = valueName[3..];
                }
                else if (e.Name == "MaterialMapIndex")
                {
                    valueName = valueName[11..];
                }
                else if (e.Name == "Npatch")
                {
                    valueName = valueName[6..];
                }

                _ = sb.AppendLine($"    /// <summary> {value.Description} </summary>");
                _ = sb.AppendLine($"    {valueName} = {value.Value},");
            }

            _ = sb.AppendLine("}");
            _ = sb.AppendLine();
            _ = sb.AppendLine("#pragma warning restore CA1711");

            File.WriteAllText(Path.Join("../RaylibSharp/gen/Enums/", api.Directory, e.Name + ".cs"), sb.ToString());
        }

        Console.WriteLine();
    }
}
