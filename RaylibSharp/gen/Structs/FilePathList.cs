namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> File path list </summary>
[NativeMarshalling(typeof(FilePathListMarshaller))]
public unsafe partial struct FilePathList
{
    /// <summary> Filepaths max entries </summary>
    public uint Capacity;
    /// <summary> Filepaths entries count </summary>
    public uint Count;
    /// <summary> Filepaths entries </summary>
    public string[] Paths;
    internal sbyte** PathsPtr;
}

/// <summary> File path list </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct UnmanagedFilePathList
{
    /// <summary> Filepaths max entries </summary>
    public uint Capacity;
    /// <summary> Filepaths entries count </summary>
    public uint Count;
    /// <summary> Filepaths entries </summary>
    public sbyte** Paths;
}

#pragma warning restore CA1711,IDE0005
