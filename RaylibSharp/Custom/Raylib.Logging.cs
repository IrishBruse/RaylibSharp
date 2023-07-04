namespace RaylibSharp;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

public static unsafe partial class Raylib
{
    private static TraceLogCallback traceLogCallback = ConsoleLog;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static unsafe void NativeLog(int msgType, sbyte* text, sbyte* args)
    {
        IntPtr textPtr = new(text);
        IntPtr argsPtr = new(args);

        string nativeSprintf = Snprintf(textPtr, argsPtr);
        string mySprintf = SprintF(text, argsPtr);

        if (string.Equals(nativeSprintf, mySprintf, StringComparison.Ordinal))
        {
            Console.WriteLine(nativeSprintf);
            Console.WriteLine(mySprintf);
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine(mySprintf);
        }

        // traceLogCallback?.Invoke((TraceLogLevel)msgType, log);
    }

    private static string Snprintf(IntPtr textPtr, IntPtr argsPtr)
    {
        int byteLength = vsnprintf(IntPtr.Zero, 0, textPtr, argsPtr) + 1;
        Span<byte> span = new byte[byteLength];
        fixed (byte* variable = span)
        {
            _ = vsnprintf((nint)variable, byteLength, textPtr, argsPtr);

            // Convert byte span to string
            string sprintfText = Encoding.ASCII.GetString(span.ToArray());
            return sprintfText;
        }
    }

    // Custom logging function
    private static void ConsoleLog(TraceLogLevel msgType, string text)
    {
        switch (msgType)
        {
            case TraceLogLevel.LogInfo:
            LogMessage(" [INFO]: ", text, ConsoleColor.White);
            break;

            case TraceLogLevel.LogError:
            LogMessage(" [ERROR]: ", text, ConsoleColor.Red);
            break;

            case TraceLogLevel.LogWarning:
            LogMessage(" [WARNING]: ", text, ConsoleColor.Yellow);
            break;

            case TraceLogLevel.LogDebug:
            LogMessage(" [DEBUG]: ", text, ConsoleColor.Blue);
            break;

            case TraceLogLevel.LogFatal:
            LogMessage(" [Fatal]: ", text, ConsoleColor.DarkRed);
            break;

            case TraceLogLevel.LogTrace:
            LogMessage(" [Trace]: ", text, ConsoleColor.White);
            break;
        }
    }

    private static void LogMessage(string prefix, string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + prefix + text);
        Console.ResetColor();
    }

    [LibraryImport("msvcrt", EntryPoint = "vsnprintf")]
    private static partial int vsnprintf(IntPtr buffer, int size, IntPtr format, IntPtr args);

}
