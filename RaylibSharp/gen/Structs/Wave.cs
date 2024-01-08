namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Wave, audio wave data </summary>
public unsafe partial struct Wave
{
    /// <summary> Total number of frames (considering channels) </summary>
    public uint FrameCount;
    /// <summary> Frequency (samples per second) </summary>
    public uint SampleRate;
    /// <summary> Bit depth (bits per sample): 8, 16, 32 (24 not supported) </summary>
    public uint SampleSize;
    /// <summary> Number of channels (1-mono, 2-stereo, ...) </summary>
    public uint Channels;
    /// <summary> Buffer data pointer </summary>
    public IntPtr Data;
}

/// <summary> Wave, audio wave data </summary>
[StructLayout(LayoutKind.Sequential)]
unsafe struct UnmanagedWave
{
    /// <summary> Total number of frames (considering channels) </summary>
    public uint FrameCount;
    /// <summary> Frequency (samples per second) </summary>
    public uint SampleRate;
    /// <summary> Bit depth (bits per sample): 8, 16, 32 (24 not supported) </summary>
    public uint SampleSize;
    /// <summary> Number of channels (1-mono, 2-stereo, ...) </summary>
    public uint Channels;
    /// <summary> Buffer data pointer </summary>
    public void* Data;
}

#pragma warning restore CA1711,IDE0005
