namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Sound </summary>
public unsafe partial struct Sound
{
    /// <summary> Audio stream </summary>
    public AudioStream Stream;
    /// <summary> Total number of frames (considering channels) </summary>
    public uint Framecount;
}

/// <summary> Sound </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct UnmanagedSound
{
    /// <summary> Audio stream </summary>
    public UnmanagedAudioStream Stream;
    /// <summary> Total number of frames (considering channels) </summary>
    public uint Framecount;
}

#pragma warning restore CA1711,IDE0005
