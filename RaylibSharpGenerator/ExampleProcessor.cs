namespace RaylibSharp.Generator;

using System.Text;
using System.Text.RegularExpressions;

public partial class ExampleProcessor
{
    public static void Emit()
    {
        IEnumerable<string> files = Directory.GetFiles("./examples/", "*.c", SearchOption.AllDirectories).ToList();

        foreach (string f in files)
        {
            string cFile = f;
            string name = Path.GetFileNameWithoutExtension(cFile);

            if (!name.StartsWith("core"))
            {
                continue;
            }

            if (name == "examples_template")
            {
                continue;
            }

            string csFile = $"../Example/temp/{Utility.ToPascalCase(name)}.cs";

            GenerateExample(cFile, csFile);
        }
    }

    private static void GenerateExample(string inputFile, string outputFile)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(outputFile)!);

        string exampleName = Path.GetFileNameWithoutExtension(outputFile);

        string[] lines = File.ReadAllLines(inputFile);

        string[] fileHeader = {
            "using System.Numerics;",
            "using System.Drawing;",
            "",
            "using RaylibSharp;",
            "",
            "using static RaylibSharp.Raylib;",
            "",
        };

        List<string> output = new()
        {
            string.Join("\n", fileHeader),
            "public static partial class Example\n{",
        };

        bool headerRemoved = false;
        bool lastLineEmpty = false;

        foreach (string item in lines)
        {
            string line = item.Trim();

            if (!headerRemoved)
            {
                headerRemoved = line.EndsWith("***/");
                continue;
            }

            if (line.StartsWith("//---") || line.StartsWith("#include"))
            {
                continue;
            }

            bool currentLineEmpty = string.IsNullOrEmpty(line);

            if (lastLineEmpty && currentLineEmpty)
            {
                continue;
            }

            if (line.StartsWith("#define"))
            {
                string[] parts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine(parts.Length + " : " + string.Join(" ", parts));

                string type = parts[2][0] == '"' ? "string" : "int";
                line = new($"private static readonly {type} {parts[1]} = {parts[2]};");
            }

            lastLineEmpty = currentLineEmpty;

            if (line.ToString().Contains("int main("))
            {
                line = new($"public static int {exampleName}()");
            }

            output.Add(ProcessLine(line));
        }

        output.Add("}");

        File.WriteAllLines(outputFile, output);
    }

    private static string ProcessLine(string line)
    {
        line = IsKeyConstEnumReplace().Replace(line, m => $"{m.Groups[1]}(Key.{Utility.ToPascalCase(m.Groups[2].Value)})");
        line = IsMouseConstEnumReplace().Replace(line, m => $"{m.Groups[1]}(MouseButton.{Utility.ToPascalCase(m.Groups[2].Value)})");
        line = ArrayReplace().Replace(line.ToString(), "$1[] $2 = new $1$3;");

        line = line.Replace("{ 0 }", "new()");

        line = StructAssignment().Replace(line.ToString(), "= new($1)");
        line = Vector2Replace().Replace(line.ToString(), "new($1)");
        line = Vector3Replace().Replace(line.ToString(), "new($1)");
        line = ColorReplace().Replace(line.ToString(), "Color.FromArgb($2, $1)");

        foreach (string color in Utility.Colors)
        {
            line = line.Replace(color.ToUpper(), color);
        }

        line = line.Replace("Rectangle ", "RectangleF ");
        line = line.Replace("Rectangle[", "RectangleF[");

        line = line.Replace("camera.target", "camera.Target");
        line = line.Replace("camera.offset", "camera.Offset");
        line = line.Replace("camera.rotation", "camera.Rotation");
        line = line.Replace("camera.zoom", "camera.Zoom");

        line = line.Replace(".x", ".X");
        line = line.Replace(".y", ".Y");
        line = line.Replace(".height", ".Height");
        line = line.Replace(".width", ".Width");

        line = line.Replace("unsigned ", "");
        line = line.Replace("BeginDrawing();", "BeginDrawing();{");
        line = line.Replace("EndDrawing();", "}EndDrawing();");

        line = line.Replace("BeginMode2D(camera);", "BeginMode2D(camera);{");
        line = line.Replace("EndMode2D();", "}EndMode2D();");

        return line;
    }

    [GeneratedRegex(@"= {(.*)}")] // = { -12.0, 1.0 }
    private static partial Regex StructAssignment();

    [GeneratedRegex(@"(IsKey\w+)\(KEY_(.*)\)")] // IsKeyDown(KEY_RIGHT)
    private static partial Regex IsKeyConstEnumReplace();

    [GeneratedRegex(@"(IsMouse\w+)\(MOUSE_BUTTON_(.*)\)")] // IsMouseButtonDown(MOUSE_BUTTON_RIGHT)
    private static partial Regex IsMouseConstEnumReplace();

    [GeneratedRegex(@"\(Vector3\)\{((.*?),(.*?),(.*?))\}")] // (Vector3){ , , }
    private static partial Regex Vector3Replace();

    [GeneratedRegex(@"\(Color\)\{ (.*), (255) \}")] // (Color){ , , , }
    private static partial Regex ColorReplace();

    [GeneratedRegex(@"\(Vector2\)\{((.*?),(.*?))\}")] // (Vector2){ , }
    private static partial Regex Vector2Replace();

    [GeneratedRegex(@"(\w+) (\w+)(\[\w+\]).*")] // int x[10];
    private static partial Regex ArrayReplace();
}

// int opens = trimmed.Count(c => c == '{');
// int closes = trimmed.Count(c => c == '}');

// if (opens > 0 && opens == closes)
// {
//     line.Replace("{ 0 }", "new()");

//     Match structMatch = StructAssignment().Match(trimmed);

//     if (structMatch.Groups.Count > 1)
//     {
//         line.Replace(structMatch.Value, $"= new({structMatch.Groups[1].Value})");
//     }
// }
