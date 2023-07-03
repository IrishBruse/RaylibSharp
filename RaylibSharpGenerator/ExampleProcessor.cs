namespace RaylibSharp.Generator;

using System.Text.RegularExpressions;

public partial class ExampleProcessor
{
    private static readonly string[] Exclude ={
        "Core2dCamera",
        "Core2dCameraMouseZoom",
        "Core3dCameraFirstPerson",
        "Core3dCameraFree",
        "Core3dCameraMode",
        "Core3dPicking",
        "CoreBasicWindow",
        "CoreCustomFrameControl",
        "CoreInputKeys",
        "CoreInputMouse",
        "CoreInputMultitouch",
        "CoreRandomValues",
        "CoreScissorTest",
        "CoreWindowShouldClose",
        "CoreWorldScreen",
        "Core2dCameraPlatformer",
        "CoreInputMouseWheel",
        "CoreBasicScreenManager",
        "CoreBasicWindowWeb",
        "CoreDropFiles",
        "CoreWindowFlags",
        "CoreInputGamepad",
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

            if (name == "examples_template")
            {
                continue;
            }

            string pascalName = Utility.ToPascalCase(name);
            string csFile = $"../Example/temp/{pascalName}.c";

            if (Exclude.Contains(pascalName))
            {
                continue;
            }

            GenerateExample(cFile, csFile);
        }

        // Process.Start("dotnet", "format ../Example/Example.csproj").WaitForExit();
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
            "using Camera = RaylibSharp.Camera3D;",
            "",
        };

        List<string> output = new()
        {
            string.Join("\n", fileHeader),
            "public static partial class "+exampleName+"\n{",
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

                string[] parts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[1..];

                if (parts.Length <= 2)
                {
                    string type = parts[1][^1] == 'f' ? "float" : "int";
                    line = new($"private static readonly {type} {parts[0]} = {parts[1]};");
                }
                else
                {

                    if (line.IndexOf('"') != -1)
                    {
                        line = $"{parts[0]} = {line.Split('"')[1]};";
                    }
                    else
                    {
                        line = "// " + line;
                    }
                }
            }

            lastLineEmpty = currentLineEmpty;

            if (line.ToString().Contains("int main("))
            {
                line = new($"public static int Example()");
            }

            output.Add(ProcessLine(line));
        }

        output.Add("}");

        File.WriteAllLines(outputFile, output);
    }

    private static bool replaceCloseingBrace;

    private static string ProcessLine(string line)
    {
        if (line.StartsWith("rl"))
        {
            return "";
        }

        line = line.Replace("->", ".");

        line = IsKeyConstEnumReplace().Replace(line, m => $"{m.Groups[1]}(Key.{Utility.ToPascalCase(m.Groups[2].Value)})");
        line = IsMouseConstEnumReplace().Replace(line, m => $"{m.Groups[1]}(MouseButton.{Utility.ToPascalCase(m.Groups[2].Value)})");
        line = ArrayReplace().Replace(line, "$1[] $2 = new $1$3;");

        line = line.Replace("{ 0 }", "new()");

        line = Vector2Replace().Replace(line, "new($1)");
        line = Vector3Replace().Replace(line, "new($1)");
        line = StructAssignment().Replace(line, "= new($1)");
        line = ColorReplace().Replace(line, "Color.FromArgb($2, $1)");

        line = Vector2AddReplace().Replace(line, "($1 + $2)");
        line = Vector2ScaleReplace().Replace(line, "($1 * $2)");

        if (replaceCloseingBrace)
        {
            line = line.Replace("}", ")");
            replaceCloseingBrace = false;
        }

        foreach (string color in Utility.Colors)
        {
            line = line.Replace(color.ToUpper(), color);
        }

        line = line.Replace("&camera", "ref camera");

        line = line.Replace("Rectangle ", "RectangleF ");
        line = line.Replace("Rectangle[", "RectangleF[");


        line = line.Replace("camera.target", "camera.Target");
        line = line.Replace("camera.offset", "camera.Offset");
        line = line.Replace("camera.rotation", "camera.Rotation");
        line = line.Replace("camera.zoom", "camera.Zoom");
        line = line.Replace("camera.zoom", "camera.Zoom");
        line = line.Replace("camera.position", "camera.Position");
        line = line.Replace("camera.up", "camera.Up");
        line = line.Replace("camera.fovy", "camera.Fovy");
        line = line.Replace("camera.projection", "camera.Projection");


        line = line.Replace(".x", ".X");
        line = line.Replace(".y", ".Y");
        line = line.Replace(".z", ".Z");
        line = line.Replace(".height", ".Height");
        line = line.Replace(".width", ".Width");

        if (line.Contains("(BoundingBox)"))
        {
            line = line.Replace("(BoundingBox)", "new");
            line = line.Replace("{", "(");
            replaceCloseingBrace = true;
        }

        // CameraProjection
        line = line.Replace("CAMERA_PERSPECTIVE", "CameraProjection.Perspective");
        line = line.Replace("CAMERA_ORTHOGRAPHIC", "CameraProjection.Orthographic");

        // CameraMode
        line = line.Replace("CAMERA_CUSTOM", "CameraMode.Custom");
        line = line.Replace("CAMERA_FREE", "CameraMode.Free");
        line = line.Replace("CAMERA_ORBITAL", "CameraMode.Orbital");
        line = line.Replace("CAMERA_FIRST_PERSON", "CameraMode.FirstPerson");
        line = line.Replace("CAMERA_THIRD_PERSON", "CameraMode.ThirdPerson");

        line = line.Replace("typedef struct", "struct");
        line = line.Replace("collision.hit", "collision.Hit");
        line = line.Replace("KEY_NULL", "Key.Null");

        line = line.Replace("unsigned ", "");
        line = line.Replace("BeginDrawing();", "BeginDrawing();{");
        line = line.Replace("EndDrawing();", "}EndDrawing();");

        line = line.Replace("BeginMode2D(camera);", "BeginMode2D(camera);{");
        line = line.Replace("EndMode2D();", "}EndMode2D();");

        // Speficic examples
        line = line.Replace("int cameraMode = CameraMode", "CameraMode cameraMode = CameraMode");

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

    [GeneratedRegex(@"Vector2Add\((.*?), (.*?)\)")] // Vector2Add(delta, -1.0f / camera.Zoom);
    private static partial Regex Vector2AddReplace();

    [GeneratedRegex(@"Vector2Scale\((.*?), (.*?)\)")] // Vector2Scale(delta, -1.0f / camera.Zoom);
    private static partial Regex Vector2ScaleReplace();
}
