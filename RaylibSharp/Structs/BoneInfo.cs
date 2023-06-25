namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> Bone, skeletal animation bone </summary>
public unsafe partial struct BoneInfo
{
    /// <summary> Bone name </summary>
    public string Name;
    /// <summary> Bone parent </summary>
    public int Parent;
}

/// <summary> Bone, skeletal animation bone </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct UnmanagedBoneInfo
{
    /// <summary> Bone name </summary>
    public fixed char Name[32];
    /// <summary> Bone parent </summary>
    public int Parent;
}

#pragma warning restore CA1711,IDE0005
