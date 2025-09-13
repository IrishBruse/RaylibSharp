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

    private static void GenerateExample(Lines lines, string exampleName, string outputFile)
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
                _ = lines.NextLine();
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
            else if (source.TrimStart().StartsWith("#define"))
            {
                string[] parts = source.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                string name = parts[1];
                string value = parts[2];

                string type;
                if (value.Contains('.'))
                {
                    type = "float";
                }
                else
                {
                    type = value.Contains('"') ? "string" : "int";
                }

                if (source.Contains("MAX(a") || source.Contains("MIN(a"))
                {
                    continue;
                }

                output.Add(tab + $"const {type} {name} = {value};");
                continue;
            }
            else if (source.Contains("int main("))
            {
                if (exampleName is "CoreInputGamepadInfo" or
                    "CoreInputGestures")
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

    private static void MoveLineRangeBy(List<string> lines, int start, int end, int count, bool dedent = false)
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


    private static string? ProcessLine(string l, string exampleName)
    {
        StringBuilder line = new(l);

        Globals(line);

        // Pascal Case
        UpperCaseVariables(line);

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

                    _ = line.Replace("int eveningOut", "bool eveningOut");
                    _ = line.Replace("eveningOut = 0;", "eveningOut = false;");
                    _ = line.Replace("eveningOut = 1;", "eveningOut = true;");

                    _ = line.Replace(".speed", ".Speed");
                    _ = line.Replace(".rect", ".Rect");
                    _ = line.Replace(".canJump", ".CanJump");
                    _ = line.Replace(".blocking", ".Blocking");
                }
                break;
            case "Core2dCameraSplitScreen":
                break;
            case "Core3dCameraFirstPerson":
                _ = line.Replace("&camera", "ref camera");
                _ = line.Replace("int cameraMode", "CameraMode cameraMode");
                break;
            case "Core3dCameraFree":
                _ = line.Replace("&camera", "ref camera");
                _ = line.Replace("int cameraMode", "CameraMode cameraMode");
                break;
            case "Core3dCameraMode":
                break;
            case "Core3dCameraSplitScreen":
                break;
            case "Core3dPicking":
                _ = line.Replace("&camera", "ref camera");
                _ = line.Replace("new({", "new(new(");
                _ = line.Replace(")});", ")));");
                break;
            case "CoreAutomationEvents":
                _ = line.Replace("), 0", "), false");
                _ = line.Replace("), 1", "), true");

                _ = line.Replace("struct Player", "struct Player(Vector2 position, float speed, bool canJump)");
                _ = line.Replace("Vector2 position;", "public Vector2 Position = position;");
                _ = line.Replace("float speed;", "public float Speed = speed;");
                _ = line.Replace("bool canJump;", "public bool CanJump = canJump;");
                _ = line.Replace("} Player;", "}");

                _ = line.Replace("struct EnvElement", "struct EnvElement(Rectangle rect, bool blocking, Color color)");
                _ = line.Replace("Rectangle rect;", "public Rectangle Rect = rect;");
                _ = line.Replace("int blocking;", "public bool Blocking = blocking;");
                _ = line.Replace("Color color;", "public Color Color = color;");
                _ = line.Replace("} EnvElement;", "}");

                _ = line.Replace("EnvElement *element = &envElements[i];", "EnvElement element = envElements[i];");
                _ = line.Replace("Vector2 *p = &(player.Position);", "ref Vector2 p = ref player.Position;");
                _ = line.Replace("int hitObstacle = 0;", "bool hitObstacle = false;");
                _ = line.Replace("hitObstacle = 1;", "hitObstacle = true;");

                break;
            case "CoreBasicScreenManager":
                {
                    string convertedEnum = """
                const int LOGO = 0;
                    const int TITLE = 1;
                    const int GAMEPLAY = 2;
                    const int ENDING = 3;
                """;
                    _ = line.Replace("typedef enum GameScreen new(LOGO = 0, TITLE, GAMEPLAY, ENDING) GameScreen;", convertedEnum);
                    _ = line.Replace("GameScreen", "int");
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
                _ = line.Replace("GetGamepadAxisMovement(0, i)", "GetGamepadAxisMovement(0, (GamepadAxis)i)");
                break;
            case "CoreInputGamepadInfo":
                _ = line.Replace("GetGamepadAxisMovement(i, ", "GetGamepadAxisMovement(i, (GamepadAxis)");
                _ = line.Replace("IsGamepadButtonDown(i, ", "IsGamepadButtonDown(i, (GamepadButton)");
                break;
            case "CoreInputGestures":
                _ = line.Replace("public static int Example()", "public static void Example()");
                _ = line.Replace("int currentGesture", "Gesture currentGesture");
                _ = line.Replace("int lastGesture", "Gesture lastGesture");
                _ = line.Replace("char gestureStrings[MAX_GESTURE_STRINGS][32];", "string[] gestureStrings = new string[MAX_GESTURE_STRINGS];");
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
                    _ = line.Replace("enum { STATE_WAITING, STATE_LOADING, STATE_FINISHED } state = STATE_WAITING;", convertedEnum);
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
                _ = line.Replace("&camera", "ref camera");

                _ = line.Replace("config.leftLensCenter", "config.LeftLensCenter");
                _ = line.Replace("config.rightLensCenter", "config.RightLensCenter");
                _ = line.Replace("config.leftScreenCenter", "config.LeftScreenCenter");
                _ = line.Replace("config.rightScreenCenter", "config.RightScreenCenter");
                _ = line.Replace("config.scale", "config.Scale");
                _ = line.Replace("config.scaleIn", "config.ScaleIn");

                _ = line.Replace(".hResolution", ".HResolution");
                _ = line.Replace(".vResolution", ".VResolution");
                _ = line.Replace(".hScreenSize", ".HScreenSize");
                _ = line.Replace(".vScreenSize", ".VScreenSize");
                _ = line.Replace(".eyeToScreenDistance", ".EyeToScreenDistance");
                _ = line.Replace(".lensSeparationDistance", ".LensSeparationDistance");
                _ = line.Replace(".interpupillaryDistance", ".InterpupillaryDistance");
                _ = line.Replace(".lensDistortionValues", ".LensDistortionValues");
                _ = line.Replace(".lensDistortionValues", ".LensDistortionValues");
                _ = line.Replace(".lensDistortionValues", ".LensDistortionValues");
                _ = line.Replace(".lensDistortionValues", ".LensDistortionValues");
                _ = line.Replace(".chromaAbCorrection", ".ChromaAbCorrection");
                _ = line.Replace(".chromaAbCorrection", ".ChromaAbCorrection");
                _ = line.Replace(".chromaAbCorrection", ".ChromaAbCorrection");
                _ = line.Replace(".chromaAbCorrection", ".ChromaAbCorrection");

                _ = line.Replace(" .", " ");
                _ = line.Replace("VrDeviceInfo device = {", "VrDeviceInfo device = new () {");

                _ = line.Replace("};", "");
                _ = line.Replace("IPD (distance between pupils) in meters", "IPD (distance between pupils) in meters\n        };");

                _ = line.Replace("SHADER_UNIFORM_VEC2", "ShaderUniformDataType.ShaderUniformVec2");
                _ = line.Replace("SHADER_UNIFORM_VEC4", "ShaderUniformDataType.ShaderUniformVec4");

                _ = line.Replace("     LensDistortionValues", " device.LensDistortionValues");
                _ = line.Replace("     ChromaAbCorrection", " device.ChromaAbCorrection");
                _ = line.Replace("defined(PLATFORM_DESKTOP)", "PLATFORM_DESKTOP");
                if (line.Contains("parameter"))
                {
                    _ = line.Replace(",", ";");
                }

                _ = line.Replace("LoadShader(0", "LoadShader(null");
                break;
            case "CoreWindowFlags":
                break;
            case "CoreWindowLetterbox":
                if (line.Contains("const int MAX(a, = b);") || line.Contains("const int MIN(a, = b);"))
                {
                    line.Length = 0;
                }
                _ = line.Replace("{ (GetScreenWidth(", "new((GetScreenWidth(");
                _ = line.Replace("scale }, ", "scale), ");

                break;
            case "CoreWindowShouldClose":
                break;
            case "CoreWorldScreen":
                _ = line.Replace("&camera", "ref camera");
                break;
            default:
                break;
        }

        return line.ToString();
    }

    private static void Globals(StringBuilder line)
    {
        _ = line.Replace("\"raylib [", "\"RaylibSharp [");

        foreach (string gesture in Utility.Gestures)
        {
            _ = line.Replace("GESTURE_" + gesture.ToUpperInvariant(), "Gesture." + gesture);
        }

        foreach (string key in Utility.Keys)
        {
            _ = line.Replace("KEY_" + key.ToUpperInvariant(), "Key." + key);
        }

        foreach (string val in Utility.MaterialMapIndex)
        {
            _ = line.Replace("MATERIAL_MAP_" + val.ToUpperInvariant(), "MaterialMapIndex." + val);
        }

        foreach (string val in Utility.MaterialMapIndex)
        {
            _ = line.Replace("MATERIAL_MAP_" + val.ToUpperInvariant(), "MaterialMapIndex." + val);
        }

        foreach (string val in Utility.TextureFilter)
        {
            _ = line.Replace(val, string.Concat("TextureFilter.", Utility.ToPascalCase(val[15..])));
        }

        foreach (string val in Utility.Flags)
        {
            _ = line.Replace(val, "WindowFlag." + Utility.ToPascalCase(val.Replace("FLAG_", "").Replace("WINDOW_", "")));
        }

        foreach (string val in Utility.GamepadAxis)
        {
            _ = line.Replace(val, "GamepadAxis." + Utility.ToPascalCase(val.Replace("GAMEPAD_AXIS_", "")));
        }

        foreach (string val in Utility.GamepadButtons)
        {
            _ = line.Replace(val, "GamepadButton." + Utility.ToPascalCase(val.Replace("GAMEPAD_BUTTON_", "")));
        }

        // CameraProjection
        _ = line.Replace("CAMERA_PERSPECTIVE", "CameraProjection.Perspective");
        _ = line.Replace("CAMERA_ORTHOGRAPHIC", "CameraProjection.Orthographic");

        // CameraMode
        _ = line.Replace("CAMERA_CUSTOM", "CameraMode.Custom");
        _ = line.Replace("CAMERA_FREE", "CameraMode.Free");
        _ = line.Replace("CAMERA_ORBITAL", "CameraMode.Orbital");
        _ = line.Replace("CAMERA_FIRST_PERSON", "CameraMode.FirstPerson");
        _ = line.Replace("CAMERA_THIRD_PERSON", "CameraMode.ThirdPerson");

        _ = line.Replace("LOG_INFO", "TraceLogLevel.Info");

        line.Replace(RLGLReplace(), "RLGL.$1");

        line.Replace(IsMouseConstEnumReplace(), m => $"{m.Groups[1]}(MouseButton.{Utility.ToPascalCase(m.Groups[2].Value)})");

        _ = line.Replace("typedef struct", "struct");

        line.ReplaceAll("->", ".");

        line.ReplaceAll("unsigned int ", "uint ");

        _ = line.Replace("(Color)", "");
        _ = line.Replace("(Vector2)", "");
        _ = line.Replace("(Vector3)", "");
        _ = line.Replace("(Rectangle)", "");
        _ = line.Replace("(BoundingBox)", "");

        line.Replace(ArrayReplace(), "$1[] $2 = new $1$3");

        _ = line.Replace("{ 0 }", "new()");

        for (int i = 0; i < 3; i++)
        {
            line.Replace(Test(), "new($1)");
        }
    }

    private static void UpperCaseVariables(StringBuilder line)
    {
        _ = line.Replace(".x", ".X");
        _ = line.Replace(".y", ".Y");
        _ = line.Replace(".z", ".Z");

        _ = line.Replace(".height", ".Height");
        _ = line.Replace(".width", ".Width");

        _ = line.Replace(".materials", ".Materials");
        _ = line.Replace(".maps", ".Maps");
        _ = line.Replace(".meshes", ".Meshes");
        _ = line.Replace(".count", ".Count");
        _ = line.Replace(".paths", ".Paths");
        _ = line.Replace(".name", ".Name");
        _ = line.Replace(".parent", ".Parent");

        _ = line.Replace(".id", ".Id");

        _ = line.Replace(".target", ".Target");
        _ = line.Replace(".offset", ".Offset");
        _ = line.Replace(".rotation", ".Rotation");
        _ = line.Replace(".zoom", ".Zoom");
        _ = line.Replace(".zoom", ".Zoom");
        _ = line.Replace(".position", ".Position");
        _ = line.Replace(".up", ".Up");
        _ = line.Replace(".fovy", ".Fovy");
        _ = line.Replace(".projection", ".Projection");

        _ = line.Replace(".texture", ".Texture");
        _ = line.Replace(".hit", ".Hit");
        _ = line.Replace(".speed", ".Speed");
        _ = line.Replace(".canJump", ".CanJump");
        _ = line.Replace(".rect", ".Rect");
        _ = line.Replace(".blocking", ".Blocking");
        _ = line.Replace(".events", ".Events");
        _ = line.Replace(".frame", ".Frame");
        _ = line.Replace(".color", ".Color");
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

internal static class Extensions
{
    public static void ReplaceAll(this StringBuilder sb, string find, string replace)
    {
        string l = "";
        while (!sb.Equals(l))
        {
            _ = sb.Replace(find, replace);
            l = sb.ToString();
        }
    }

    public static void Replace(this StringBuilder sb, Regex regex, MatchEvaluator matchEvaluator)
    {
        string output = regex.Replace(sb.ToString(), matchEvaluator);
        _ = sb.Clear();
        _ = sb.Insert(0, output);
    }

    public static void Replace(this StringBuilder sb, Regex regex, string replacement)
    {
        string output = regex.Replace(sb.ToString(), replacement);
        _ = sb.Clear();
        _ = sb.Insert(0, output);
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
