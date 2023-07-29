namespace RaylibSharp.Generator;

using System.Text.Json;

public class Program
{
    private static void Main()
    {
        string jsonString = File.ReadAllText("api/raylib.json");

        RaylibApi api = JsonSerializer.Deserialize<RaylibApi>(jsonString)!;

        EnumProcessor.Emit(api);
        StructProcessor.Emit(api);
        FunctionProcessor.Emit(api, "Raylib");

        ExampleProcessor.Emit();
    }
}
