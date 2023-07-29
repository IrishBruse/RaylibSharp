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

            if (name == "examples_template")
            {
                continue;
            }

            string pascalName = Utility.ToPascalCase(name);

            string csFile = "../Examples/Gen";

            if (pascalName == "ShapesTopDownLights")
            {
                continue;
            }

            if (pascalName.StartsWith("Core"))
            {
                continue;
            }
            else if (pascalName.StartsWith("Audio"))
            {
                continue;
            }
            else if (pascalName.StartsWith("Shapes"))
            {
                csFile = $"../Examples/Shapes/{pascalName}.cs";
            }
            else if (pascalName.StartsWith("Models"))
            {
                csFile = $"{csFile}/Models/{pascalName}.cs";
            }
            else if (pascalName.StartsWith("Shader"))
            {
                csFile = $"{csFile}/Shader/{pascalName}.cs";
            }
            else if (pascalName.StartsWith("Text"))
            {
                csFile = $"{csFile}/Text/{pascalName}.cs";
            }
            else if (pascalName.StartsWith("Texture"))
            {
                csFile = $"{csFile}/Texture/{pascalName}.cs";
            }
            else
            {
                csFile = $"{csFile}/temp/{pascalName}.cs";
            }


            string[] lines = File.ReadAllLines(cFile);
            GenerateExample(lines, csFile);
        }

        // Process.Start("dotnet", "format ../Example/Example.csproj").WaitForExit();
    }

    private static void GenerateExample(string[] input, string outputFile)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(outputFile)!);

        string exampleName = Path.GetFileNameWithoutExtension(outputFile);

        string[] fileHeader = {
            "using System.Numerics;",
            "using System.Drawing;",
            "using System;",
            "",
            "using RaylibSharp;",
            "",
            "using static RaylibSharp.Raylib;",
            "",
        };

        List<string> output = new()
        {
            string.Join("\n", fileHeader),
            "public partial class " + exampleName + " : ExampleHelper \n{",
        };

        bool headerRemoved = false;
        bool lastLineEmpty = false;

        foreach (string item in input)
        {
            string line = "    " + item;
            line = line.TrimEnd();

            if (!headerRemoved)
            {
                headerRemoved = line.EndsWith("***/");
                continue;
            }

            if (line.TrimStart().StartsWith("//---") || line.TrimStart().StartsWith("#include"))
            {
                continue;
            }

            bool currentLineEmpty = string.IsNullOrEmpty(line);

            if (lastLineEmpty && currentLineEmpty)
            {
                continue;
            }

            lastLineEmpty = currentLineEmpty;

            if (line.TrimStart().StartsWith("#define"))
            {
                string lineWithoutComment = line.Split("//", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[0];
                string[] parts = lineWithoutComment.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                Console.WriteLine(string.Join(" ", parts));

                if (parts.Length == 2)
                {
                    Console.WriteLine("Skipping: " + line);
                    line = "";
                }
                else
                {
                    line = new($"private const int {parts[1]} = {parts[2]};");
                }
            }

            if (line.ToString().Contains("int main("))
            {
                line = new($"    public static int Example()");
            }

            string newLine = ProcessLine(line);
            if (newLine != null)
            {
                output.Add(newLine);
            }
        }

        output.Add("}");

        File.WriteAllLines(outputFile, output);
    }

    private static bool replaceCloseingBrace;

    private static string ProcessLine(string l)
    {
        StringBuilder line = new(l);

        line.Replace(RectangleReplace(), "new($1)");
        line.Replace(ColorReplace(), "Color.FromArgb($2, $1)");

        line.Replace(StructAssignment(), "= new($1)");

        line.ReplaceAll("Rectangle ", "RectangleF ");
        line.ReplaceAll("Rectangle[", "RectangleF[");

        // Change Alias
        line.ReplaceAll("MATERIAL_MAP_DIFFUSE", "MATERIAL_MAP_ALBEDO");
        line.ReplaceAll("Camera3D", "Camera");
        line.ReplaceAll("Texture2D", "Texture");
        line.ReplaceAll("RenderTexture2D", "RenderTexture");

        line.ReplaceAll("atan2f(", "MathF.Atan2(");
        line.ReplaceAll("cosf(", "MathF.Cos(");
        line.ReplaceAll("sinf(", "MathF.Sin(");
        line.ReplaceAll("ceilf(", "MathF.Ceiling(");

        line.ReplaceAll("->", ".");

        line.Replace(IsMouseConstEnumReplace(), m => $"{m.Groups[1]}(MouseButton.{Utility.ToPascalCase(m.Groups[2].Value)})");
        line.Replace(ArrayReplace(), "$1[] $2 = new $1$3");
        line.Replace(VoidFunctionMatch(), "static $0");
        line.Replace(Vector2Replace(), "new($1)");
        line.Replace(Vector2AssignReplace(), "new($1,$2)");
        line.Replace(Vector3Replace(), "new($1)");
        line.Replace(Vector3AssignReplace(), "new($1,$2,$3)");
        line.Replace(FalseBooleanAssignment(), "$1 false;");
        line.Replace(ExampleName(), "RaylibSharp - $1 - ");

        foreach (string color in Utility.Colors)
        {
            line.ReplaceAll(color.ToUpper(), color);
        }

        foreach (string key in Utility.Keys)
        {
            line.ReplaceAll("KEY_" + key.ToUpper(), "Key." + key);
        }

        foreach (string val in Utility.MaterialMapIndex)
        {
            line.ReplaceAll("MATERIAL_MAP_" + val.ToUpper(), "MaterialMapIndex." + val);
        }

        line.ReplaceAll("camera.target", "camera.Target");
        line.ReplaceAll("camera.offset", "camera.Offset");
        line.ReplaceAll("camera.rotation", "camera.Rotation");
        line.ReplaceAll("camera.zoom", "camera.Zoom");
        line.ReplaceAll("camera.zoom", "camera.Zoom");
        line.ReplaceAll("camera.position", "camera.Position");
        line.ReplaceAll("camera.up", "camera.Up");
        line.ReplaceAll("camera.fovy", "camera.Fovy");
        line.ReplaceAll("camera.projection", "camera.Projection");

        line.ReplaceAll(".hit", ".Hit");

        line.ReplaceAll(".x", ".X");
        line.ReplaceAll(".y", ".Y");
        line.ReplaceAll(".z", ".Z");
        line.ReplaceAll(".height", ".Height");
        line.ReplaceAll(".width", ".Width");

        line.Replace("V(", "(");
        line.Replace("Ex(", "(");
        line.Replace("Pro(", "(");
        line.Replace("Rec(", "(");

        line.Replace("%2", "%2 == 0");

        line.Replace("{ 0 }", "new()");

        // Types
        line.ReplaceAll("void *", "System.IntPtr ");
        line.ReplaceAll("unsigned int ", "uint ");
        line.ReplaceAll("const char *", "string ");

        // CameraProjection
        line.Replace("CAMERA_PERSPECTIVE", "CameraProjection.Perspective");
        line.Replace("CAMERA_ORTHOGRAPHIC", "CameraProjection.Orthographic");

        // CameraMode
        line.Replace("CAMERA_CUSTOM", "CameraMode.Custom");
        line.Replace("CAMERA_FREE", "CameraMode.Free");
        line.Replace("CAMERA_ORBITAL", "CameraMode.Orbital");
        line.Replace("CAMERA_FIRST_PERSON", "CameraMode.FirstPerson");
        line.Replace("CAMERA_THIRD_PERSON", "CameraMode.ThirdPerson");

        line.Replace("FLAG_MSAA_4X_HINT", "WindowFlag.Msaa4xHint");

        line.Replace("NULL", "null");

        line.Replace(".materials", ".Materials");
        line.Replace(".maps", ".Maps");
        line.Replace(".meshes", ".Meshes");
        line.Replace(".count", ".Count");
        line.Replace(".paths", ".Paths");
        line.Replace("&camera", "ref camera");

        line.Replace("BeginDrawing();", "BeginDrawing();{");
        line.Replace("EndDrawing();", "}EndDrawing();");

        line.Replace("BeginMode2D(camera);", "BeginMode2D(camera);{");
        line.Replace("EndMode2D();", "}EndMode2D();");

        line.Replace("BeginMode3D(camera);", "BeginMode3D(camera);{");
        line.Replace("EndMode3D();", "}EndMode3D();");

        // Hardcoded edits
        line.Replace("int [] colorState = new int [MAX_COLORS_COUNT];           // Color state: 0-DEFAULT, 1-MOUSE_HOVER", "bool [] colorState = new bool [MAX_COLORS_COUNT];           // Color state: 0-DEFAULT, 1-MOUSE_HOVER");
        line.Replace("colorState[i] = 1;", "colorState[i] = true;");
        line.Replace("colorState[i] = 0;", "colorState[i] = false;");
        line.Replace("if (framesCounter/12)", "if (framesCounter/12==0)");

        return line.ToString();
    }

    [GeneratedRegex(@"= {(.*?,.*?)}")] private static partial Regex StructAssignment(); // = { -12.0, 1.0 }
    [GeneratedRegex(@"(IsMouse\w+)\(MOUSE_BUTTON_(.*?)\)")] private static partial Regex IsMouseConstEnumReplace(); // IsMouseButtonDown(MOUSE_BUTTON_RIGHT)
    [GeneratedRegex(@"\(Vector3\)\{((.*?),(.*?),(.*?))\}")] private static partial Regex Vector3Replace(); // (Vector3){ , , }
    [GeneratedRegex(@"\{ (.*?f), (.*?f), (.*?f) \}")] private static partial Regex Vector3AssignReplace(); // { 0.0f, 0.0f, 0.0f }
    [GeneratedRegex(@"\(Vector2\)\{((.*?),(.*?))\}")] private static partial Regex Vector2Replace(); // (Vector2){ , }
    [GeneratedRegex(@"\{ (.*?f), (.*?f) \}")] private static partial Regex Vector2AssignReplace(); // { , }
    [GeneratedRegex(@"(int |float |const char \*|Color |RectangleF )(\w+)(\[.*\]) = (\{ 0 \})?")] private static partial Regex ArrayReplace(); // int x[10];
    [GeneratedRegex(@"void \w+\(")] private static partial Regex VoidFunctionMatch();
    [GeneratedRegex(@"(bool \w+ =) 0")] private static partial Regex FalseBooleanAssignment(); // bool varname = 0
    [GeneratedRegex(@"raylib \[(\w+)\] example - ")] private static partial Regex ExampleName(); // raylib [core] example => RaylibSharp - core -

    [GeneratedRegex(@"\(Rectangle\)\{(.*?,.*?,.*?,.*?)\}")] private static partial Regex RectangleReplace(); // (Rectangle){ , , , }
    [GeneratedRegex(@"\(Color\)\{ (.*), (255) \}")] private static partial Regex ColorReplace(); // (Color){ , , , }
    [GeneratedRegex(@"Vector2Add\((.*?), (.*?)\)")] private static partial Regex Vector2AddReplace(); // Vector2Add(delta, -1.0f / camera.Zoom);
    [GeneratedRegex(@"Vector2Scale\((.*?), (.*?)\)")] private static partial Regex Vector2ScaleReplace(); // Vector2Scale(delta, -1.0f / camera.Zoom);
    [GeneratedRegex(@"(IsKey\w+)\(KEY_(.*)\)")] private static partial Regex IsKeyConstEnumReplace(); // IsKeyDown(KEY_RIGHT)
}

internal static class Extensions
{
    public static void ReplaceAll(this StringBuilder sb, string find, string replace)
    {
        string l = "";
        while (!sb.Equals(l))
        {
            sb.Replace(find, replace);
            l = sb.ToString();
        }
    }

    public static void Replace(this StringBuilder sb, Regex regex, MatchEvaluator matchEvaluator)
    {
        string output = regex.Replace(sb.ToString(), matchEvaluator);
        sb.Clear();
        sb.Insert(0, output);
    }

    public static void Replace(this StringBuilder sb, Regex regex, string matchEvaluator)
    {
        string output = regex.Replace(sb.ToString(), matchEvaluator);
        sb.Clear();
        sb.Insert(0, output);
    }

    public static bool Contains(this StringBuilder sb, string value)
    {
        return sb.IndexOf(value) != -1;
    }

    public static int IndexOf(this StringBuilder sb, string value)
    {
        if (sb == null)
        {
            return -1;
        }

        if (string.IsNullOrEmpty(value))
        {
            return -1;
        }

        int count = sb.Length;
        int len = value.Length;

        if (count < len)
        {
            return -1;
        }

        int loopEnd = count - len + 1;

        for (int loop = 0; loop < loopEnd; loop++)
        {
            bool found = true;

            for (int innerLoop = 0; innerLoop < len; innerLoop++)
            {
                if (sb[loop + innerLoop] != value[innerLoop])
                {
                    found = false;
                    break;
                }
            }

            if (found)
            {
                return loop;
            }
        }

        return -1;
    }
}
