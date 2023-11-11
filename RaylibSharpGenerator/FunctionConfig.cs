namespace RaylibSharp.Generator;

using System.Text.Json;

public struct FunctionConfig
{
    public string[] Excluded { get; set; }
    public bool DebugOutput { get; set; }
    public Dictionary<string, Dictionary<string, string>> FunctionTypeConversion { get; set; }
    static readonly JsonSerializerOptions Options = new() { ReadCommentHandling = JsonCommentHandling.Skip };
    public static FunctionConfig Deserialize(string path)
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<FunctionConfig>(json, Options)!;
    }
}
