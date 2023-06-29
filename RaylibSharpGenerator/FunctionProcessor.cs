namespace RaylibSharp.Generator;

using System.Text;

public class FunctionProcessor
{
    private const bool DebugOutput = false;

    private static readonly string[] Exclude = {

        // Have C# alternatives
        "DirectoryExists",
        "FileExists",
        "GetApplicationDirectory",
        "GetDirectoryPath",
        "GetFileExtension",
        "GetFileName",
        "GetFileNameWithoutExt",
        "GetPrevDirectoryPath",
        "GetRandomValue",
        "GetWorkingDirectory",
        "IsFileExtension",
        "LoadDirectoryFiles",
        "LoadDirectoryFilesEx",
        "SaveFileText",
        "SetRandomSeed",
        "TextFormat",
        "UnloadDirectoryFiles",
        // Have C# alternatives

        // Unsure
        "MemAlloc",
        "MemRealloc",
        "MemFree",

        "CompressData",
        "DecompressData",
        "EncodeDataBase64",
        "DecodeDataBase64",

        // WIP currently have errors
        "TraceLog",
    };

    public static void Emit(RaylibApi api)
    {
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
            if (Exclude.Contains(f.Name))
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

        File.WriteAllText("../RaylibSharp/Raylib.g.cs", sb.ToString());
    }

    private static string EmitParameter(Param p, Function f)
    {
        if (f.Name == "LoadFontFromMemory" || f.Name == "LoadImageFromMemory")
        {
            if (p.Name == "fileData")
            {
                return $"[MarshalAs(UnmanagedType.LPArray)] byte[] {p.Name}";
            }
        }
        else if (f.Name == "SetConfigFlags")
        {
            if (p.Name == "flags")
            {
                return $"ConfigFlags {p.Name}";
            }
        }
        else if (f.Name == "IsKeyReleased" || f.Name == "IsKeyUp" || f.Name == "IsKeyPressed" || f.Name == "IsKeyDown" || f.Name == "SetExitKey")
        {
            if (p.Name == "key")
            {
                return $"Key {p.Name}";
            }
        }
        else if (f.Name == "IsMouseButtonDown")
        {
            if (p.Name == "button")
            {
                return $"MouseButton {p.Name}";
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


        return $"{type} {p.Name}";
    }
}
