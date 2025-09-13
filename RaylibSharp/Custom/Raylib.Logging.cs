namespace RaylibSharp;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public static unsafe partial class Raylib
{
    /// <summary> Show trace log messages (LOG_DEBUG, LOG_INFO, LOG_WARNING, LOG_ERROR...) </summary>
    public static void TraceLog(TraceLogLevel level, string value, params object[] args)
    {
        TraceLogCallback.Invoke(level, value);
    }

    private static TraceLogCallback TraceLogCallback = ConsoleLog;

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvCdecl) })]
    private static unsafe void NativeLog(int msgType, sbyte* text, sbyte* args)
    {
        string message = Logging.GetLogMessage(new IntPtr(text), new IntPtr(args));
        ConsoleLog((TraceLogLevel)msgType, message);
    }

    private static void ConsoleLog(TraceLogLevel msgType, string text)
    {
        switch (msgType)
        {
            case TraceLogLevel.Info:
                LogMessage("INFO:  ", text, ConsoleColor.White);
                break;

            case TraceLogLevel.Error:
                LogMessage("ERROR: ", text, ConsoleColor.Red);
                break;

            case TraceLogLevel.Warning:
                LogMessage("WARN:  ", text, ConsoleColor.Yellow);
                break;

            case TraceLogLevel.Debug:
                LogMessage("DEBUG: ", text, ConsoleColor.Blue);
                break;

            case TraceLogLevel.Fatal:
                LogMessage("FATAL: ", text, ConsoleColor.DarkRed);
                break;

            case TraceLogLevel.Trace:
                LogMessage("TRACE: ", text, ConsoleColor.Gray);
                break;

            case TraceLogLevel.All: break;
            case TraceLogLevel.None: break;
            default: break;
        }
    }

    private static void LogMessage(string prefix, string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(prefix + text);
        Console.ResetColor();
    }

}

internal readonly partial struct Native
{
    internal const string Msvcrt = "msvcrt";
    internal const string Libc = "libc";
    internal const string LibSystem = "libSystem";

    [LibraryImport(LibSystem, EntryPoint = "vasprintf")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial int VasPrintfApple(ref IntPtr buffer, IntPtr format, IntPtr args);

    [LibraryImport(Libc, EntryPoint = "vsprintf")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial int VsPrintfLinux(IntPtr buffer, IntPtr format, IntPtr args);

    [LibraryImport(Msvcrt, EntryPoint = "vsprintf")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial int VsPrintfWindows(IntPtr buffer, IntPtr format, IntPtr args);

    [LibraryImport(Libc, EntryPoint = "vsnprintf")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial int VsnPrintfLinux(IntPtr buffer, UIntPtr size, IntPtr format, IntPtr args);

    [LibraryImport(Msvcrt, EntryPoint = "vsnprintf")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial int VsnPrintfWindows(IntPtr buffer, UIntPtr size, IntPtr format, IntPtr args);
}

[StructLayout(LayoutKind.Sequential, Pack = 4)]
internal struct VaListLinuxX64
{
    private uint _gpOffset;
    private uint _fpOffset;
    private IntPtr _overflowArgArea;
    private IntPtr _regSaveArea;
}

// https://github.com/raylib-cs/raylib-cs/blob/master/Raylib-cs/types/Logging.cs

/// <summary>
/// Logging workaround for formatting strings from native code
/// </summary>
public static unsafe class Logging
{
    /// <summary> Get message based on platform </summary>
    public static string GetLogMessage(IntPtr format, IntPtr args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return AppleLogCallback(format, args);
        }

        // Special marshalling is needed on Linux desktop 64 bits.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && IntPtr.Size == 8)
        {
            return LinuxX64LogCallback(format, args);
        }

        int byteLength = VsnPrintf(IntPtr.Zero, UIntPtr.Zero, format, args) + 1;
        if (byteLength <= 1)
        {
            return string.Empty;
        }

        nint buffer = Marshal.AllocHGlobal(byteLength);
        _ = VsPrintf(buffer, format, args);

        string? result = Marshal.PtrToStringUTF8(buffer);
        Marshal.FreeHGlobal(buffer);

        return result!;
    }

    private static string AppleLogCallback(IntPtr format, IntPtr args)
    {
        IntPtr buffer = IntPtr.Zero;
        try
        {
            int count = Native.VasPrintfApple(ref buffer, format, args);
            if (count == -1)
            {
                return string.Empty;
            }
            return Marshal.PtrToStringUTF8(buffer) ?? string.Empty;
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
    }

    private static unsafe string LinuxX64LogCallback(IntPtr format, IntPtr args)
    {
        // The args pointer cannot be reused between two calls. We need to make a copy of the underlying structure.
        VaListLinuxX64 listStructure = *(VaListLinuxX64*)args;

        // Get length of args
        int size = sizeof(VaListLinuxX64);
        nint listPointer = Marshal.AllocHGlobal(size);
        *(VaListLinuxX64*)listPointer = listStructure;
        int byteLength = Native.VsnPrintfLinux(nint.Zero, nuint.Zero, format, listPointer) + 1;
        *(VaListLinuxX64*)listPointer = listStructure;

        IntPtr utf8Buffer = IntPtr.Zero;
        utf8Buffer = Marshal.AllocHGlobal(byteLength);

        // Print result into buffer
        _ = Native.VsPrintfLinux(utf8Buffer, format, listPointer);
        string? result = Marshal.PtrToStringUTF8(utf8Buffer);

        Marshal.FreeHGlobal(listPointer);
        Marshal.FreeHGlobal(utf8Buffer);

        return result!;
    }

    // https://github.com/dotnet/runtime/issues/51052
    private static int VsnPrintf(IntPtr buffer, UIntPtr size, IntPtr format, IntPtr args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return Native.VsnPrintfWindows(buffer, size, format, args);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return Native.VsnPrintfLinux(buffer, size, format, args);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("ANDROID")))
        {
            return Native.VsPrintfLinux(buffer, format, args);
        }
        return -1;
    }

    // https://github.com/dotnet/runtime/issues/51052
    private static int VsPrintf(IntPtr buffer, IntPtr format, IntPtr args)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return Native.VsPrintfWindows(buffer, format, args);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return Native.VsPrintfLinux(buffer, format, args);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("ANDROID")))
        {
            return Native.VsPrintfLinux(buffer, format, args);
        }
        return -1;
    }
}
