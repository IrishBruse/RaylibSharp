namespace RaylibSharp.Generator;

using System.Text.Json;

public class StructConfig
{
    public string[] AdditionalProperties { get; set; } = Array.Empty<string>();
    public bool GenUnmanaged { get; set; } = true;
    public bool GenManaged { get; set; } = true;
    public bool UnmanagedAttribute { get; set; }

    public static StructConfig Deserialize(string path)
    {
        string json = File.ReadAllText(path);
        JsonSerializerOptions options = new() { ReadCommentHandling = JsonCommentHandling.Skip };
        return JsonSerializer.Deserialize<StructConfig>(json, options)!;
    }
}
