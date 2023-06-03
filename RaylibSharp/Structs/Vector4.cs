namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;

/// <summary> Vector4, 4 components </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Vector4
{
    /// <summary> Vector x component </summary>
    public float /*float*/ X;
    /// <summary> Vector y component </summary>
    public float /*float*/ Y;
    /// <summary> Vector z component </summary>
    public float /*float*/ Z;
    /// <summary> Vector w component </summary>
    public float /*float*/ W;
}
#pragma warning restore CA1711,IDE0005

