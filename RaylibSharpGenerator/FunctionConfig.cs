namespace RaylibSharp.Generator;

public struct FunctionConfig
{
    public string[] Excluded { get; set; }
    public bool DebugOutput { get; set; }
    public Dictionary<string, Dictionary<string, string>> FunctionTypeConversion { get; set; }
}
