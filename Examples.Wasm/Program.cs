namespace Examples.Wasm;

using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

[SupportedOSPlatform("browser")]
public static partial class Program
{
    public static void Main()
    {
        ExamplePicker.Run();
    }

    [JSExport]
    public static bool IsExample()
    {
        return ExamplePicker.IsExample;
    }
}
