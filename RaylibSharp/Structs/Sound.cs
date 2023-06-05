namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> Sound </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Sound
{
    /// <summary> Audio stream </summary>
    public AudioStream /* AudioStream */ Stream;
    /// <summary> Total number of frames (considering channels) </summary>
    public uint /* unsigned int */ Framecount;
}

#pragma warning restore CA1711,IDE0005
