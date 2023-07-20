namespace RaylibSharp;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

public static unsafe partial class Raylib
{
    /// <summary> Show trace log messages (LOG_DEBUG, LOG_INFO, LOG_WARNING, LOG_ERROR...) </summary>
    public static void TraceLog(TraceLogLevel level, string value)
    {
        traceLogCallback.Invoke(level, value);
    }

    // Todo Fix this as this locks the binding to a single instance
    private static TraceLogCallback traceLogCallback = ConsoleLog;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static unsafe void NativeLog(int msgType, sbyte* text, sbyte* args)
    {
        IntPtr textPtr = new(text);
        IntPtr argsPtr = new(args);

        string mySprintf = SprintF(text, argsPtr);

        // string nativeSprintf = Snprintf(textPtr, argsPtr);
        // if (string.Equals(nativeSprintf, mySprintf, StringComparison.Ordinal))
        // {
        //     Console.WriteLine(nativeSprintf);
        //     Console.WriteLine(mySprintf);
        //     Console.WriteLine();
        // }
        // else
        // {
        //     Console.WriteLine(mySprintf);
        // }

        traceLogCallback?.Invoke((TraceLogLevel)msgType, mySprintf);
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
            case TraceLogLevel.Info:
            LogMessage(" [INFO]: ", text, ConsoleColor.White);
            break;

            case TraceLogLevel.Error:
            LogMessage(" [ERROR]: ", text, ConsoleColor.Red);
            break;

            case TraceLogLevel.Warning:
            LogMessage(" [WARNING]: ", text, ConsoleColor.Yellow);
            break;

            case TraceLogLevel.Debug:
            LogMessage(" [DEBUG]: ", text, ConsoleColor.Blue);
            break;

            case TraceLogLevel.Fatal:
            LogMessage(" [Fatal]: ", text, ConsoleColor.DarkRed);
            break;

            case TraceLogLevel.Trace:
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
