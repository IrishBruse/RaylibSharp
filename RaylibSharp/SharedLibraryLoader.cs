namespace RaylibSharp;

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#pragma warning disable CA2255

internal sealed class SharedLibraryLoader
{
    [ModuleInitializer]
    internal static void Init()
    {
        NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), Resolve);
    }

    private static IntPtr Resolve(string libName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        string runtimeId = RuntimeID();

        string dllPath = $"{AppContext.BaseDirectory}/runtimes/{runtimeId}/native/{libName}";

        NativeLibrary.TryLoad(dllPath, out IntPtr libHandle);

        return libHandle;
    }

    private static string RuntimeID()
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
