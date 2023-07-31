namespace RaylibSharp.Generator;

public class Program
{
    private static void Main()
    {
        RaylibApi api;

        api = RaylibApi.Deserialize("api/raylib.json");
        api.ClassName = "Raylib";
        api.Namespace = "RaylibSharp";

        EnumProcessor.Emit(api);
        StructProcessor.Emit(api);
        FunctionProcessor.Emit(api);

        api = RaylibApi.Deserialize("api/rlgl.json");
        api.ClassName = "RLGL";
        api.Namespace = "RaylibSharp.GL";
        api.Directory = "GL";

        EnumProcessor.Emit(api);
        StructProcessor.Emit(api);
        FunctionProcessor.Emit(api);

        ExampleProcessor.Emit();
    }
}
