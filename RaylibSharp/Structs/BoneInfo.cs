namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;

/// <summary> Bone, skeletal animation bone </summary>
[StructLayout(LayoutKind.Sequential)]
public struct BoneInfo
{
    /// <summary> Bone name </summary>
    public fixed sbyte /*char[32]*/ Name[32];
    /// <summary> Bone parent </summary>
    public int /*int*/ Parent;
}
#pragma warning restore CA1711,IDE0005

