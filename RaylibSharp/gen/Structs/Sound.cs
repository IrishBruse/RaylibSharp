namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Sound </summary>
public unsafe partial struct Sound
{
    /// <summary> Audio stream </summary>
    public AudioStream Stream;
    /// <summary> Total number of frames (considering channels) </summary>
    public uint FrameCount;
}

/// <summary> Sound </summary>
[StructLayout(LayoutKind.Sequential)]
unsafe struct UnmanagedSound
{
    /// <summary> Audio stream </summary>
    public UnmanagedAudioStream Stream;
    /// <summary> Total number of frames (considering channels) </summary>
    public uint FrameCount;
}

#pragma warning restore CA1711,IDE0005
