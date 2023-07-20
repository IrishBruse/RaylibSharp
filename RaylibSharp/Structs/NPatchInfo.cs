namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> NPatchInfo, n-patch layout info </summary>
public unsafe partial struct NPatchInfo
{
    /// <summary> Texture source rectangle </summary>
    public RectangleF Source;
    /// <summary> Left border offset </summary>
    public int Left;
    /// <summary> Top border offset </summary>
    public int Top;
    /// <summary> Right border offset </summary>
    public int Right;
    /// <summary> Bottom border offset </summary>
    public int Bottom;
    /// <summary> Layout of the n-patch: 3x3, 1x3 or 3x1 </summary>
    public int Layout;
}

/// <summary> NPatchInfo, n-patch layout info </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct UnmanagedNPatchInfo
{
    /// <summary> Texture source rectangle </summary>
    public RectangleF Source;
    /// <summary> Left border offset </summary>
    public int Left;
    /// <summary> Top border offset </summary>
    public int Top;
    /// <summary> Right border offset </summary>
    public int Right;
    /// <summary> Bottom border offset </summary>
    public int Bottom;
    /// <summary> Layout of the n-patch: 3x3, 1x3 or 3x1 </summary>
    public int Layout;
}

#pragma warning restore CA1711,IDE0005
