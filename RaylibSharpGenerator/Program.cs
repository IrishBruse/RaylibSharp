namespace RaylibSharp.Generator;

public class Program
{
    private static void Main()
    {
        RaylibApi api;

        api = RaylibApi.Deserialize("api/raylib.json");
        api.ClassName = "Raylib";
        api.Namespace = "RaylibSharp";

        Generate(api);

        api = RaylibApi.Deserialize("api/rlgl.json");
        api.ClassName = "RLGL";
        api.Namespace = "RaylibSharp.GL";
        api.Directory = "GL";

        Generate(api);

        // ExampleProcessor.Emit();
    }

    private static void Generate(RaylibApi api)
    {
        EnumProcessor.Emit(api);
        DefineProcessor.Emit(api);
        StructProcessor.Emit(api);
        FunctionProcessor.Emit(api);
    }
}
