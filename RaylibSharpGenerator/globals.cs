#pragma warning disable IDE0005,CS8019
global using static RaylibSharp.Generator.Globals;
#pragma warning restore IDE0005, CS8019

namespace RaylibSharp.Generator;

public static class Globals
{
    public static void Log(string message, ConsoleColor? color = null)
    {
        if (color != null)
        {
            Console.ForegroundColor = color.Value;
        }

        Console.WriteLine(message);
        Console.ResetColor();
    }
}
