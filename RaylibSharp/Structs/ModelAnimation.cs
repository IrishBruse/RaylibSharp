namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> ModelAnimation </summary>
[NativeMarshalling(typeof(ModelAnimationMarshaller))]
public unsafe partial struct ModelAnimation
{
    /// <summary> Number of bones </summary>
    public int Bonecount;
    /// <summary> Number of animation frames </summary>
    public int Framecount;
    /// <summary> Bones information (skeleton) </summary>
    public BoneInfo[] Bones;
    /// <summary> Poses array by frame </summary>
    public Transform[][] Frameposes;
    /// <summary> Animation name </summary>
    public string Name;
}

/// <summary> ModelAnimation </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct UnmanagedModelAnimation
{
    /// <summary> Number of bones </summary>
    public int Bonecount;
    /// <summary> Number of animation frames </summary>
    public int Framecount;
    /// <summary> Bones information (skeleton) </summary>
    public UnmanagedBoneInfo* Bones;
    /// <summary> Poses array by frame </summary>
    public UnmanagedTransform** Frameposes;
    /// <summary> Animation name </summary>
    public fixed char Name[32];
}

#pragma warning restore CA1711,IDE0005
