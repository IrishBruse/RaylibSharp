namespace RaylibSharp.Generator;

public class StructConfig
{
    public string[] AdditionalProperties { get; set; } = [];
    public bool GenUnmanaged { get; set; } = true;
    public bool GenManaged { get; set; } = true;
    public bool UnmanagedAttribute { get; set; }
    public Dictionary<string, string> FunctionTypeConversion { get; set; } = new Dictionary<string, string>();
    public string[] Remove { get; set; } = [];
    public string[] UnmanagedRemove { get; set; } = [];
}
