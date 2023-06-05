namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> Wave, audio wave data </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Wave
{
    /// <summary> Total number of frames (considering channels) </summary>
    public uint /* unsigned int */ Framecount;
    /// <summary> Frequency (samples per second) </summary>
    public uint /* unsigned int */ Samplerate;
    /// <summary> Bit depth (bits per sample): 8, 16, 32 (24 not supported) </summary>
    public uint /* unsigned int */ Samplesize;
    /// <summary> Number of channels (1-mono, 2-stereo, ...) </summary>
    public uint /* unsigned int */ Channels;
    /// <summary> Buffer data pointer </summary>
    public IntPtr /* void * */ Data;
}

#pragma warning restore CA1711,IDE0005
