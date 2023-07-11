namespace Example;

using System;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Reflection;

using RaylibSharp;

public static class Program
{
    public static void Main()
    {
        Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        Examples.ExamplePicker.Run();
    }
}
