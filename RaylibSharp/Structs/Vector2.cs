namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;

/// <summary> Vector2, 2 components </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Vector2
{
    /// <summary> Vector x component </summary>
    public float /*float*/ X;
    /// <summary> Vector y component </summary>
    public float /*float*/ Y;
}
#pragma warning restore CA1711,IDE0005

