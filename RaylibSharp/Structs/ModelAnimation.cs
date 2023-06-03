namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;

/// <summary> ModelAnimation </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ModelAnimation
{
    /// <summary> Number of bones </summary>
    public int /*int*/ Bonecount;
    /// <summary> Number of animation frames </summary>
    public int /*int*/ Framecount;
    /// <summary> Bones information (skeleton) </summary>
    public IntPtr /*BoneInfo **/ Bones;
    /// <summary> Poses array by frame </summary>
    public IntPtr /*Transform ***/ Frameposes;
    /// <summary> Animation name </summary>
    public fixed sbyte /*char[32]*/ Name[32];
}
#pragma warning restore CA1711,IDE0005

