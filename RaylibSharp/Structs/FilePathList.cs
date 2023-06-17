namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> File path list </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct FilePathList
{
    /// <summary> Filepaths max entries </summary>
    public uint Capacity;
    /// <summary> Filepaths entries count </summary>
    public uint Count;
    /// <summary> Filepaths entries </summary>
    public sbyte** Paths;
}

#pragma warning restore CA1711,IDE0005
