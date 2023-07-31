namespace RaylibSharp.Generator;

using System.Text.Json;

public struct FunctionConfig
{
    public string[] Excluded { get; set; }
    public bool DebugOutput { get; set; }
    public Dictionary<string, Dictionary<string, string>> FunctionTypeConversion { get; set; }

    public static FunctionConfig Deserialize(string path)
    {
        string json = File.ReadAllText(path);
        JsonSerializerOptions options = new() { ReadCommentHandling = JsonCommentHandling.Skip };
        return JsonSerializer.Deserialize<FunctionConfig>(json, options)!;
    }
}
