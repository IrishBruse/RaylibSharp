namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;

/// <summary> Sound </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Sound
{
    /// <summary> Audio stream </summary>
    public AudioStream /*AudioStream*/ Stream;
    /// <summary> Total number of frames (considering channels) </summary>
    public uint /*unsigned int*/ Framecount;
}
#pragma warning restore CA1711,IDE0005

