namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> Bone, skeletal animation bone </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct BoneInfo
{
    /// <summary> Bone name </summary>
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
    public fixed char Name[32];
    /// <summary> Bone parent </summary>
    public int Parent;
}

#pragma warning restore CA1711,IDE0005
