namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;

/// <summary> File path list </summary>
[StructLayout(LayoutKind.Sequential)]
public struct FilePathList
{
    /// <summary> Filepaths max entries </summary>
    public uint /*unsigned int*/ Capacity;
    /// <summary> Filepaths entries count </summary>
    public uint /*unsigned int*/ Count;
    /// <summary> Filepaths entries </summary>
    public IntPtr /*char ***/ Paths;
}
#pragma warning restore CA1711,IDE0005

