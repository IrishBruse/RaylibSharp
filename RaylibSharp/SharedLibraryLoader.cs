namespace RaylibSharp;

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#pragma warning disable CA2255

static class SharedLibraryLoader
{
    static IntPtr? libHandle;

    [ModuleInitializer]
    internal static void Init()
    {
        Console.WriteLine("Locating native Raylib dll");
        NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), Resolve);
    }

    static IntPtr Resolve(string libName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (libHandle.HasValue)
        {
            return libHandle.Value;
        }

        string runtimeId = RuntimeID();

        string dllPath = $"{AppContext.BaseDirectory}/runtimes/{runtimeId}/native/{libName}";

        Console.WriteLine($"Loaded native Raylib dll from {dllPath}");

        if (NativeLibrary.TryLoad(dllPath, out IntPtr handle))
        {
            libHandle = handle;
            return handle;
        }
        else
        {
            return IntPtr.Zero;
        }
    }

    static string RuntimeID()
    {
        string runtimeId;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            if (RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                runtimeId = "win-x64";
            }
            else
            {
                runtimeId = "win-x86";
            }
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            if (RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                runtimeId = "linux-x64";
            }
            else
            {
                runtimeId = "linux-x86";
            }
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            if (RuntimeInformation.ProcessArchitecture == Architecture.X64)
            {
                runtimeId = "osx-x64";
            }
            else
            {
                runtimeId = "osx-x86";
            }
        }
        else
        {
            runtimeId = "browser-wasm";
        }

        return runtimeId;
    }
}
