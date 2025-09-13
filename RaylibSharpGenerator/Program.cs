namespace RaylibSharp.Generator;

public class Program
{
    private static bool raylib = true;
    private static bool rlgl = true;
    private static bool examples = true;

    private static void Main()
    {
        RaylibApi api;

        if (raylib)
        {
            api = RaylibApi.Deserialize("api/raylib.json");
            api.ClassName = "Raylib";
            api.Namespace = "RaylibSharp";
            Generate(api);
        }

        Console.WriteLine();

        if (rlgl)
        {
            api = RaylibApi.Deserialize("api/rlgl.json");
            api.ClassName = "RLGL";
            api.Namespace = "RaylibSharp.GL";
            api.Directory = "GL";
            Generate(api);
        }

        if (examples)
        {
            ExampleProcessor.Emit();
        }
    }

    private static void Generate(RaylibApi api)
    {
        Log($"Class {api.ClassName}", ConsoleColor.Green);

        EnumProcessor.Emit(api);
        DefineProcessor.Emit(api);
        StructProcessor.Emit(api);
        FunctionProcessor.Emit(api);
    }
}
