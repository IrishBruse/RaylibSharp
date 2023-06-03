namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;

/// <summary> Vector3, 3 components </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Vector3
{
    /// <summary> Vector x component </summary>
    public float /*float*/ X;
    /// <summary> Vector y component </summary>
    public float /*float*/ Y;
    /// <summary> Vector z component </summary>
    public float /*float*/ Z;
}
#pragma warning restore CA1711,IDE0005

