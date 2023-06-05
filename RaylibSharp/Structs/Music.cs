namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> Music, audio stream, anything longer than ~10 seconds should be streamed </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Music
{
    /// <summary> Audio stream </summary>
    public AudioStream /* AudioStream */ Stream;
    /// <summary> Total number of frames (considering channels) </summary>
    public uint /* unsigned int */ Framecount;
    /// <summary> Music looping enable </summary>
    public bool /* bool */ Looping;
    /// <summary> Type of music context (audio filetype) </summary>
    public int /* int */ Ctxtype;
    /// <summary> Audio context data, depends on type </summary>
    public IntPtr /* void * */ Ctxdata;
}

#pragma warning restore CA1711,IDE0005
