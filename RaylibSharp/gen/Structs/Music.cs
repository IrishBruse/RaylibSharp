namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Music, audio stream, anything longer than ~10 seconds should be streamed </summary>
public unsafe partial struct Music
{
    /// <summary> Audio stream </summary>
    public AudioStream Stream;
    /// <summary> Total number of frames (considering channels) </summary>
    public uint FrameCount;
    /// <summary> Music looping enable </summary>
    public bool Looping;
    /// <summary> Type of music context (audio filetype) </summary>
    public int CtxType;
    /// <summary> Audio context data, depends on type </summary>
    public IntPtr CtxData;
}

/// <summary> Music, audio stream, anything longer than ~10 seconds should be streamed </summary>
[StructLayout(LayoutKind.Sequential)]
unsafe struct UnmanagedMusic
{
    /// <summary> Audio stream </summary>
    public UnmanagedAudioStream Stream;
    /// <summary> Total number of frames (considering channels) </summary>
    public uint FrameCount;
    /// <summary> Music looping enable </summary>
    public bool Looping;
    /// <summary> Type of music context (audio filetype) </summary>
    public int CtxType;
    /// <summary> Audio context data, depends on type </summary>
    public void* CtxData;
}

#pragma warning restore CA1711,IDE0005
