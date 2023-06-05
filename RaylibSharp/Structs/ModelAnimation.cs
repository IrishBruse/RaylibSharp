namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> ModelAnimation </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct ModelAnimation
{
    /// <summary> Number of bones </summary>
    public int /* int */ Bonecount;
    /// <summary> Number of animation frames </summary>
    public int /* int */ Framecount;
    /// <summary> Bones information (skeleton) </summary>
    public IntPtr /* BoneInfo * */ Bones;
    /// <summary> Poses array by frame </summary>
    public IntPtr /* Transform ** */ Frameposes;
    /// <summary> Animation name </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public string /* char[32] */ Name;
}

#pragma warning restore CA1711,IDE0005
