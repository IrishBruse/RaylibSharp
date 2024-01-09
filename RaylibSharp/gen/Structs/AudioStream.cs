namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> AudioStream, custom audio stream </summary>
public unsafe partial struct AudioStream
{
    /// <summary> Pointer to internal data used by the audio system </summary>
    public IntPtr Buffer;
    /// <summary> Pointer to internal data processor, useful for audio effects </summary>
    public IntPtr Processor;
    /// <summary> Frequency (samples per second) </summary>
    public uint SampleRate;
    /// <summary> Bit depth (bits per sample): 8, 16, 32 (24 not supported) </summary>
    public uint SampleSize;
    /// <summary> Number of channels (1-mono, 2-stereo, ...) </summary>
    public uint Channels;
}

/// <summary> AudioStream, custom audio stream </summary>
[StructLayout(LayoutKind.Sequential)]
unsafe struct UnmanagedAudioStream
{
    /// <summary> Pointer to internal data used by the audio system </summary>
    public IntPtr Buffer;
    /// <summary> Pointer to internal data processor, useful for audio effects </summary>
    public IntPtr Processor;
    /// <summary> Frequency (samples per second) </summary>
    public uint SampleRate;
    /// <summary> Bit depth (bits per sample): 8, 16, 32 (24 not supported) </summary>
    public uint SampleSize;
    /// <summary> Number of channels (1-mono, 2-stereo, ...) </summary>
    public uint Channels;
}

#pragma warning restore CA1711,IDE0005
