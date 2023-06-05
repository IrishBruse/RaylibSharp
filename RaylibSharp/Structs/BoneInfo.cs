namespace Raylib;

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
    public string /* char[32] */ Name;
    /// <summary> Bone parent </summary>
    public int /* int */ Parent;
}

#pragma warning restore CA1711,IDE0005
