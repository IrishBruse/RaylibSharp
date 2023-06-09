namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> AudioStream, custom audio stream </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct AudioStream
{
    /// <summary> Pointer to internal data used by the audio system </summary>
    public IntPtr Buffer;
    /// <summary> Pointer to internal data processor, useful for audio effects </summary>
    public IntPtr Processor;
    /// <summary> Frequency (samples per second) </summary>
    public uint Samplerate;
    /// <summary> Bit depth (bits per sample): 8, 16, 32 (24 not supported) </summary>
    public uint Samplesize;
    /// <summary> Number of channels (1-mono, 2-stereo, ...) </summary>
    public uint Channels;
}

#pragma warning restore CA1711,IDE0005
