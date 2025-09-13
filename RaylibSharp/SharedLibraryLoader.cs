namespace RaylibSharp;

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#pragma warning disable CA2255

internal static class SharedLibraryLoader
{
    private static IntPtr? LibHandle;

    [ModuleInitializer]
    internal static void Init()
    {
        NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), Resolve);
    }

    private static IntPtr Resolve(string libName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (LibHandle.HasValue)
        {
            return LibHandle.Value;
        }

        string runtimeId = RuntimeID();

        string dllPath = Path.Join(AppContext.BaseDirectory, "runtimes", runtimeId, "native", libName);

        if (runtimeId == "linux-x64")
        {
            dllPath += ".so";
        }

        Raylib.TraceLog(TraceLogLevel.Info, $"Loaded native Raylib dll from {dllPath}");

        if (NativeLibrary.TryLoad(dllPath, out IntPtr handle))
        {
            LibHandle = handle;
            return handle;
        }

        if (NativeLibrary.TryLoad("./" + libName, out handle))
        {
            LibHandle = handle;
            return handle;
        }

        return IntPtr.Zero;
    }

    private static string RuntimeID()
    {
        string runtimeId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? RuntimeInformation.ProcessArchitecture == Architecture.X64 ? "win-x64" : "win-x86"
            : RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
                ? RuntimeInformation.ProcessArchitecture == Architecture.X64 ? "linux-x64" : "linux-x86"
                : RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                ? RuntimeInformation.ProcessArchitecture == Architecture.X64 ? "osx-x64" : "osx-x86"
                : "browser-wasm";
        return runtimeId;
    }
}
