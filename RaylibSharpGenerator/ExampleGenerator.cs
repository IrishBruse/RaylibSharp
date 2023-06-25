namespace RaylibSharp.Generator;

using System.Text;
using System.Text.RegularExpressions;

public partial class ExampleGenerator
{
    private static readonly string[] Skip = {
        "examples_template",
        "core_basic_window",
        "core_input_keys",
        "core_random_values",
        "core_drop_files",
        "core_2d_camera",
    };

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

            if (Skip.Contains(name))
            {
                continue;
            }

            string csFile = $"../Example/temp/{Utility.ToPascalCase(name)}.c";

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
            "using RaylibSharp;",
            "using static RaylibSharp.Raylib;",
            "",
        };

        List<string> output = new()
        {
            "// " + inputFile,
            "// " + outputFile,
            "",
            string.Join("\n", fileHeader),
            "public static partial class Example\n{",
        };

        bool headerRemoved = false;
        bool lastLineEmpty = false;

        foreach (string item in lines)
        {
            string trimmed = item.Trim();
            StringBuilder line = new(trimmed);

            if (!headerRemoved)
            {
                headerRemoved = trimmed.EndsWith("***/");
                continue;
            }

            if (trimmed.StartsWith("//---") || trimmed.StartsWith("#include"))
            {
                continue;
            }

            bool currentLineEmpty = string.IsNullOrEmpty(trimmed);

            if (lastLineEmpty && currentLineEmpty)
            {
                continue;
            }

            lastLineEmpty = currentLineEmpty;

            if (trimmed.StartsWith("#define"))
            {
                string[] parts = trimmed.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                string type = parts[2][0] == '"' ? "string" : "int";
                line = new($"private static readonly {type} {parts[1]} = {parts[2]};");
            }

            line.Replace(".x ", ".X ");
            line.Replace(".y ", ".Y ");
            line.Replace("unsigned ", "");

            int opens = trimmed.Count(c => c == '{');
            int closes = trimmed.Count(c => c == '}');

            if (opens > 0 && opens == closes)
            {
                line.Replace("{ 0 }", "new()");

                Match structMatch = StructAssignment().Match(trimmed);

                if (structMatch.Groups.Count > 1)
                {
                    line.Replace(structMatch.Value, $"= new({structMatch.Groups[1].Value})");
                }
            }

            Match isKeyMatch = IsKeyConstEnumReplace().Match(trimmed);

            if (isKeyMatch.Groups.Count > 1)
            {
                line.Replace(isKeyMatch.Value, $"{isKeyMatch.Groups[1].Value}(Key.{Utility.ToPascalCase(isKeyMatch.Groups[2].Value)})");
            }

            Match isVector2Match = Vector2Replace().Match(trimmed);

            if (isVector2Match.Groups.Count > 1)
            {
                line.Replace(isVector2Match.Value, $"new({isVector2Match.Groups[1].Value})");
            }

            Match isVector3Match = Vector3Replace().Match(trimmed);

            if (isVector3Match.Groups.Count > 1)
            {
                line.Replace(isVector3Match.Value, $"new({isVector3Match.Groups[1].Value})");
            }

            Match isColorMatch = ColorReplace().Match(trimmed);

            if (isColorMatch.Groups.Count > 1)
            {
                line.Replace(isColorMatch.Value, $"Color.FromArgb({isColorMatch.Groups[1].Value})");
            }

            foreach (string color in Utility.Colors)
            {
                if (trimmed.Contains(color.ToUpper()))
                {
                    line.Replace(color.ToUpper(), color);
                }
            }

            if (trimmed.StartsWith("int main(void)"))
            {
                line = new($"public static int {exampleName}()");
            }

            output.Add(line.ToString());
        }

        output.Add("}");

        File.WriteAllLines(outputFile, output);
    }

    [GeneratedRegex(@"= {(.*)}")] // = { -12.0, 1.0 }
    private static partial Regex StructAssignment();

    [GeneratedRegex(@"(IsKey\w+)\(KEY_(.*)\)")] // IsKeyDown(KEY_RIGHT)
    private static partial Regex IsKeyConstEnumReplace();


    [GeneratedRegex(@"\(Vector3\)\{(.*,.*,.*)\}")] // (Vector3){ , , }
    private static partial Regex Vector3Replace();

    [GeneratedRegex(@"\(Color\)\{(.*,.*,.*,.*)\}")] // (Color){ , , , }
    private static partial Regex ColorReplace();


    [GeneratedRegex(@"\(Vector2\)\{(.*,.*)\}")] // (Vector2){ , }
    private static partial Regex Vector2Replace();
}
