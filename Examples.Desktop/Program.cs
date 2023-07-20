namespace Examples.Desktop;
using System.IO;

public static class Program
{
    public static void Main()
    {
        Directory.SetCurrentDirectory(System.AppContext.BaseDirectory);
        ExamplePicker.Run();
    }
}
