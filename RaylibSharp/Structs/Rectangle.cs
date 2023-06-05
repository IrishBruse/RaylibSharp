namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> Rectangle, 4 components </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Rectangle
{
    /// <summary> Rectangle top-left corner position x </summary>
    public float /* float */ X;
    /// <summary> Rectangle top-left corner position y </summary>
    public float /* float */ Y;
    /// <summary> Rectangle width </summary>
    public float /* float */ Width;
    /// <summary> Rectangle height </summary>
    public float /* float */ Height;
}

#pragma warning restore CA1711,IDE0005
