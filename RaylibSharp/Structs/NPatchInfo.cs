namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;

/// <summary> NPatchInfo, n-patch layout info </summary>
[StructLayout(LayoutKind.Sequential)]
public struct NPatchInfo
{
    /// <summary> Texture source rectangle </summary>
    public Rectangle /*Rectangle*/ Source;
    /// <summary> Left border offset </summary>
    public int /*int*/ Left;
    /// <summary> Top border offset </summary>
    public int /*int*/ Top;
    /// <summary> Right border offset </summary>
    public int /*int*/ Right;
    /// <summary> Bottom border offset </summary>
    public int /*int*/ Bottom;
    /// <summary> Layout of the n-patch: 3x3, 1x3 or 3x1 </summary>
    public int /*int*/ Layout;
}
#pragma warning restore CA1711,IDE0005

