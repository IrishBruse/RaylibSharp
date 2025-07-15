namespace RaylibSharp.Generator;

using System.Text;
using System.Text.RegularExpressions;

public partial class ExampleProcessor
{
    public static void Emit()
    {
        Log("Emitting Examples", ConsoleColor.Blue);

        IEnumerable<string> files = Directory.GetFiles("../raylib/examples/", "*.c", SearchOption.AllDirectories).ToList();

        foreach (string cFile in files)
        {
            string path = cFile.Replace("../raylib/examples/", "");
            string exampleName = Utility.ToPascalCase(Path.GetFileNameWithoutExtension(path));

            if (exampleName == "ShapesTopDownLights" || exampleName == "ExamplesTemplate" || exampleName.StartsWith("temp/"))
            {
                continue;
            }

            string[] lines = File.ReadAllLines(cFile);
            Lines exampleLines = new(lines);

            if (exampleName == "CoreBasicWindowWeb") continue;
            if (exampleName == "CoreLoadingThread") continue;
            if (exampleName == "Core2dCameraPlatformer") continue;
            if (exampleName == "Core2dCameraSplitScreen") continue;

            if (exampleName.StartsWith("Core"))
            {
                GenerateExample(exampleLines, exampleName, $"../Examples/Core/{exampleName}.cs");
            }
        }
    }

    static void GenerateExample(Lines lines, string exampleName, string outputFile)
    {
        Log($"Example {outputFile}", ConsoleColor.Green);

        _ = Directory.CreateDirectory(Path.GetDirectoryName(outputFile)!);

        List<string> output = new();

        string tab = "    ";

        while (lines.HasNext())
        {
            string? line = lines.NextLine();
            if (line == null)
            {
                break;
            }
            line = line.TrimEnd();

            output.Add(line);

            if (line.EndsWith("***/"))
            {
                lines.NextLine();
                output.Add("");
                break;
            }
        }

        while (lines.HasNext())
        {
            string? line = lines.NextLine();
            if (line == null)
            {
                break;
            }

            if (line.StartsWith("#include"))
            {
                if (line.Contains("raylib.h"))
                {
                    output.Add("using static RaylibSharp.Raylib;");
                    output.Add("using RaylibSharp;");
                }
                if (line.Contains("rlgl.h"))
                {
                    output.Add("using RaylibSharp.GL;");
                }
            }
            else if (!line.StartsWith("#include") && line != "")
            {
                lines.Undo();
                break;
            }
        }

        output.Add("");
        output.Add($"public partial class {exampleName} : ExampleHelper");
        output.Add("{");

        while (lines.HasNext())
        {
            string? source = lines.NextLine();
            if (source == null)
            {
                Console.WriteLine("Line is null, skipping...");
                break;
            }
            else if (source.TrimStart().StartsWith("#include"))
            {
                lines.SkipEmpty();
                continue;
            }
            // else if (source.TrimStart().StartsWith("typedef struct"))
            // {
            //     string[] test = source.Split([" ", "{"], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            //     string structName = test[2];

            //     if (exampleName == "CoreAutomationEvents")
            //     {
            //         output.Add($"{tab}struct {structName}() {{");
            //     }
            //     else
            //     {
            //         output.Add($"{tab}struct {structName} {{");
            //     }

            //     while (lines.Until("} " + structName + ";"))
            //     {
            //         string? structLine = lines.NextLine().Trim();
            //         if (structLine == null || structLine.Trim() == "}")
            //         {
            //             output.Add($"{tab}}} {structName};");
            //             break;
            //         }

            //         string[] parts = structLine.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            //         string type = parts[0];
            //         string name = parts[1];

            //         output.Add($"{tab}{tab}public {type} {char.ToUpper(name[0]) + name[1..]}");
            //     }
            //     lines.NextLine();
            //     output.Add(tab + "}");

            //     continue;
            // }
            else if (source.TrimStart().StartsWith("#define"))
            {
                string[] parts = source.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                string name = parts[1];
                string value = parts[2];

                string type = value.Contains('.') ? "float" : "int";

                if (source.Contains("MAX(a") || source.Contains("MIN(a"))
                {
                    continue;
                }

                output.Add(tab + $"const {type} {name} = {value};");
                continue;
            }
            else if (source.Contains("int main("))
            {
                if (exampleName == "CoreInputGamepadInfo")
                {
                    output.Add(tab + $"public static void Example()");
                }
                else
                {
                    output.Add(tab + $"public static int Example()");
                }
                continue;
            }
            else
            {
                string converted = tab + ProcessLine(source, exampleName);
                output.Add(converted.TrimEnd());
            }
        }

        output.Add("}");
        output.Add("");

        File.WriteAllLines(outputFile, output);
    }

    static void MoveLineRangeBy(List<string> lines, int start, int end, int count, bool dedent = false)
    {
        start--;
        end--;
        if (start < 0 || end >= lines.Count || start >= end)
        {
            return; // Invalid range
        }

        List<string> range = lines.GetRange(start, end - start + 1);
        if (dedent)
        {
            for (int i = 0; i <= range.Count - 1; i++)
            {
                if (range[i].Length > 4)
                {
                    range[i] = range[i][4..]; // Remove 4 spaces
                }
            }
        }
        lines.RemoveRange(start, end - start + 1);
        lines.InsertRange(start + count, range);
    }


    static string? ProcessLine(string l, string exampleName)
    {
        StringBuilder line = new(l);

        Globals(line);

        switch (exampleName)
        {
            case "Core2dCamera":
            break;
            case "Core2dCameraMouseZoom":
            break;
            case "Core2dCameraPlatformer":
            {
                if (line.Contains("void") && l.EndsWith(';'))
                {
                    return "// " + line;
                }

                line.ReplaceAll("char *", "string ");

                line.ReplaceAll(" *", " ");

                line.Replace("int eveningOut", "bool eveningOut");
                line.Replace("eveningOut = 0;", "eveningOut = false;");
                line.Replace("eveningOut = 1;", "eveningOut = true;");

                line.Replace(".speed", ".Speed");
                line.Replace(".rect", ".Rect");
                line.Replace(".canJump", ".CanJump");
                line.Replace(".blocking", ".Blocking");
            }
            break;
            case "Core2dCameraSplitScreen":
            break;
            case "Core3dCameraFirstPerson":
            line.Replace("&camera", "ref camera");
            line.Replace("int cameraMode", "CameraMode cameraMode");
            break;
            case "Core3dCameraFree":
            line.Replace("&camera", "ref camera");
            line.Replace("int cameraMode", "CameraMode cameraMode");
            break;
            case "Core3dCameraMode":
            break;
            case "Core3dCameraSplitScreen":
            break;
            case "Core3dPicking":
            line.Replace("&camera", "ref camera");
            line.Replace("new({", "new(new(");
            line.Replace(")});", ")));");
            break;
            case "CoreAutomationEvents":
            line.Replace("), 0", "), false");
            line.Replace("), 1", "), true");
            line.Replace("} Player;", "}");
            line.Replace("} EnvElement;", "}");

            line.Replace("Vector2 position;", "public Vector2 Position = position;");
            line.Replace("float speed;", "public float Speed = speed;");
            line.Replace("bool canJump;", "public bool CanJump = canJump;");
            line.Replace("Rectangle rect;", "public Rectangle Rect = rect;");
            line.Replace("int blocking;", "public int Blocking = blocking;");
            line.Replace("Color color;", "public Color Color = color;");

            line.Replace("struct Player", "struct Player(Vector2 position, float speed, bool canJump)");
            line.Replace("struct EnvElement", "struct EnvElement(Rectangle rect, int blocking, Color color)");
            break;
            case "CoreBasicScreenManager":
            {
                string convertedEnum = """
                const int LOGO = 0;
                    const int TITLE = 1;
                    const int GAMEPLAY = 2;
                    const int ENDING = 3;
                """;
                line.Replace("typedef enum GameScreen new(LOGO = 0, TITLE, GAMEPLAY, ENDING) GameScreen;", convertedEnum);
                line.Replace("GameScreen", "int");
            }
            break;
            case "CoreBasicWindow":
            break;
            case "CoreCustomFrameControl":
            break;
            case "CoreCustomLogging":
            break;
            case "CoreDropFiles":
            break;
            case "CoreInputGamepad":
            break;
            case "CoreInputGamepadInfo":
            line.Replace("GetGamepadAxisMovement(i, ", "GetGamepadAxisMovement(i, (GamepadAxis)");
            line.Replace("IsGamepadButtonDown(i, ", "IsGamepadButtonDown(i, (GamepadButton)");
            break;
            case "CoreInputGestures":
            break;
            case "CoreInputGesturesWeb":
            break;
            case "CoreInputKeys":
            break;
            case "CoreInputMouse":
            break;
            case "CoreInputMouseWheel":
            break;
            case "CoreInputMultitouch":
            break;
            case "CoreInputVirtualControls":
            break;
            case "CoreLoadingThread":
            {
                string convertedEnum = """
                const int STATE_WAITING = 0;
                        const int STATE_LOADING = 1;
                        const int STATE_FINISHED = 2;
                """;
                line.Replace("enum { STATE_WAITING, STATE_LOADING, STATE_FINISHED } state = STATE_WAITING;", convertedEnum);
            }
            break;
            case "CoreRandomSequence":
            break;
            case "CoreRandomValues":
            break;
            case "CoreScissorTest":
            break;
            case "CoreSmoothPixelperfect":
            break;
            case "CoreSplitScreen":
            break;
            case "CoreStorageValues":
            break;
            case "CoreVrSimulator":
            line.Replace("&camera", "ref camera");

            line.Replace("config.leftLensCenter", "config.LeftLensCenter");
            line.Replace("config.rightLensCenter", "config.RightLensCenter");
            line.Replace("config.leftScreenCenter", "config.LeftScreenCenter");
            line.Replace("config.rightScreenCenter", "config.RightScreenCenter");
            line.Replace("config.scale", "config.Scale");
            line.Replace("config.scaleIn", "config.ScaleIn");

            line.Replace(".hResolution", ".HResolution");
            line.Replace(".vResolution", ".VResolution");
            line.Replace(".hScreenSize", ".HScreenSize");
            line.Replace(".vScreenSize", ".VScreenSize");
            line.Replace(".eyeToScreenDistance", ".EyeToScreenDistance");
            line.Replace(".lensSeparationDistance", ".LensSeparationDistance");
            line.Replace(".interpupillaryDistance", ".InterpupillaryDistance");
            line.Replace(".lensDistortionValues", ".LensDistortionValues");
            line.Replace(".lensDistortionValues", ".LensDistortionValues");
            line.Replace(".lensDistortionValues", ".LensDistortionValues");
            line.Replace(".lensDistortionValues", ".LensDistortionValues");
            line.Replace(".chromaAbCorrection", ".ChromaAbCorrection");
            line.Replace(".chromaAbCorrection", ".ChromaAbCorrection");
            line.Replace(".chromaAbCorrection", ".ChromaAbCorrection");
            line.Replace(".chromaAbCorrection", ".ChromaAbCorrection");

            line.Replace(" .", " ");
            line.Replace("VrDeviceInfo device = {", "VrDeviceInfo device = new () {");

            line.Replace("};", "");
            line.Replace("IPD (distance between pupils) in meters", "IPD (distance between pupils) in meters\n        };");

            line.Replace("SHADER_UNIFORM_VEC2", "ShaderUniformDataType.ShaderUniformVec2");
            line.Replace("SHADER_UNIFORM_VEC4", "ShaderUniformDataType.ShaderUniformVec4");

            line.Replace("     LensDistortionValues", " device.LensDistortionValues");
            line.Replace("     ChromaAbCorrection", " device.ChromaAbCorrection");
            line.Replace("defined(PLATFORM_DESKTOP)", "PLATFORM_DESKTOP");

            if (line.Contains("parameter"))
            {
                line.Replace(",", ";");
            }

            line.Replace("LoadShader(0", "LoadShader(null");
            break;
            case "CoreWindowFlags":
            break;
            case "CoreWindowLetterbox":
            if (line.Contains("const int MAX(a, = b);") || line.Contains("const int MIN(a, = b);"))
            {
                line.Length = 0;
            }
            break;
            case "CoreWindowShouldClose":
            break;
            case "CoreWorldScreen":
            line.Replace("&camera", "ref camera");
            break;
            default:
            break;
        }

        // Pascal Case
        UpperCaseVariables(line);

        return line.ToString();
    }

    static void Globals(StringBuilder line)
    {
        line.Replace("\"raylib [", "\"RaylibSharp [");

        foreach (string gesture in Utility.Gestures)
        {
            line.Replace("GESTURE_" + gesture.ToUpperInvariant(), "Gesture." + gesture);
        }

        foreach (string key in Utility.Keys)
        {
            line.Replace("KEY_" + key.ToUpperInvariant(), "Key." + key);
        }

        foreach (string val in Utility.MaterialMapIndex)
        {
            line.ReplaceAll("MATERIAL_MAP_" + val.ToUpperInvariant(), "MaterialMapIndex." + val);
        }

        foreach (string val in Utility.MaterialMapIndex)
        {
            line.ReplaceAll("MATERIAL_MAP_" + val.ToUpperInvariant(), "MaterialMapIndex." + val);
        }

        foreach (string val in Utility.TextureFilter)
        {
            line.ReplaceAll(val, string.Concat("TextureFilter.", Utility.ToPascalCase(val.Substring(15))));
        }

        foreach (string val in Utility.Flags)
        {
            line.ReplaceAll(val, "WindowFlag." + Utility.ToPascalCase(val.Replace("FLAG_", "").Replace("WINDOW_", "")));
        }

        // CameraProjection
        line.Replace("CAMERA_PERSPECTIVE", "CameraProjection.Perspective");
        line.Replace("CAMERA_ORTHOGRAPHIC", "CameraProjection.Orthographic");

        // CameraMode
        line.Replace("CAMERA_CUSTOM", "CameraMode.Custom");
        line.Replace("CAMERA_FREE", "CameraMode.Free");
        line.Replace("CAMERA_ORBITAL", "CameraMode.Orbital");
        line.Replace("CAMERA_FIRST_PERSON", "CameraMode.FirstPerson");
        line.Replace("CAMERA_THIRD_PERSON", "CameraMode.ThirdPerson");

        line.Replace("LOG_INFO", "TraceLogLevel.Info");

        line.Replace(RLGLReplace(), "RLGL.$1");

        line.Replace(IsMouseConstEnumReplace(), m => $"{m.Groups[1]}(MouseButton.{Utility.ToPascalCase(m.Groups[2].Value)})");

        line.Replace("typedef struct", "struct");

        line.ReplaceAll("->", ".");

        line.ReplaceAll("unsigned int ", "uint ");

        line.Replace("(Color)", "");
        line.Replace("(Vector2)", "");
        line.Replace("(Vector3)", "");
        line.Replace("(Rectangle)", "");
        line.Replace("(BoundingBox)", "");

        line.Replace(ArrayReplace(), "$1[] $2 = new $1$3");

        line.Replace("{ 0 }", "new()");

        for (int i = 0; i < 3; i++)
        {
            line.Replace(Test(), "new($1)");
        }
        // line.Replace(Object4Params(), "new($1, $2, $3, $4)");
        // line.Replace(Object3Params(), "new($1, $2, $3)");
        // line.Replace(Object2Params(), "new($1, $2)");


    }

    static void UpperCaseVariables(StringBuilder line)
    {
        line.Replace(".x", ".X");
        line.Replace(".y", ".Y");
        line.Replace(".z", ".Z");

        line.Replace(".height", ".Height");
        line.Replace(".width", ".Width");

        line.Replace(".materials", ".Materials");
        line.Replace(".maps", ".Maps");
        line.Replace(".meshes", ".Meshes");
        line.Replace(".count", ".Count");
        line.Replace(".paths", ".Paths");
        line.Replace(".name", ".Name");
        line.Replace(".parent", ".Parent");

        line.Replace(".id", ".Id");
        // line.Replace(".r", ".R");
        // line.Replace(".g", ".G");
        // line.Replace(".b", ".B");
        // line.Replace(".a", ".A");

        line.Replace(".target", ".Target");
        line.Replace(".offset", ".Offset");
        line.Replace(".rotation", ".Rotation");
        line.Replace(".zoom", ".Zoom");
        line.Replace(".zoom", ".Zoom");
        line.Replace(".position", ".Position");
        line.Replace(".up", ".Up");
        line.Replace(".fovy", ".Fovy");
        line.Replace(".projection", ".Projection");

        line.Replace(".texture", ".Texture");
        line.Replace(".hit", ".Hit");
        line.Replace(".speed", ".Speed");
        line.Replace(".canJump", ".CanJump");
        line.Replace(".rect", ".Rect");
        line.Replace(".color", ".Color");
    }

    [GeneratedRegex(@"(IsMouse\w+)\(MOUSE_BUTTON_(.*?)\)")] private static partial Regex IsMouseConstEnumReplace(); // IsMouseButtonDown(MOUSE_BUTTON_RIGHT)

    // { $1, $2 }
    [GeneratedRegex(@"\{\s*(.+?)\s*,\s*(.+?)\s*\}")]
    private static partial Regex Object2Params();

    // { $1, $2 }
    [GeneratedRegex(@"\{\s*(.+?)\s*\}")]
    private static partial Regex Test();

    // { $1, $2, $3 }
    [GeneratedRegex(@"\{\s*(.+?)\s*,\s*(.+?)\s*,\s*(.+?)\s*\}")]
    private static partial Regex Object3Params();

    // { $1, $2, $3, $4 }
    [GeneratedRegex(@"\{\s*([^\s,{}\]]+)\s*,\s*([^\s,{}\]]+)\s*,\s*([^\s,{}\]]+)\s*,\s*([^\s,{}\]]+)\s*\}")]
    private static partial Regex Object4Params();

    [GeneratedRegex(@"(\w+) (\w+)(\[.*\]) = (\{ 0 \})?")] private static partial Regex ArrayReplace(); // int x[10];

    [GeneratedRegex(@"rl([A-Z])")] private static partial Regex RLGLReplace(); // rlBegin
}

static class Extensions
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

    public static void Replace(this StringBuilder sb, Regex regex, string replacement)
    {
        string output = regex.Replace(sb.ToString(), replacement);
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
