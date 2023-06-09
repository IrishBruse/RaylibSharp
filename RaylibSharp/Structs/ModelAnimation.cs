namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> ModelAnimation </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct ModelAnimation
{
    /// <summary> Number of bones </summary>
    public int Bonecount;
    /// <summary> Number of animation frames </summary>
    public int Framecount;
    /// <summary> Bones information (skeleton) </summary>
    public IntPtr Bones;
    /// <summary> Poses array by frame </summary>
    public IntPtr Frameposes;
    /// <summary> Animation name </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string Name;
}

#pragma warning restore CA1711,IDE0005
