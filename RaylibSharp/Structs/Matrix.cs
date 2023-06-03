namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;

/// <summary> Matrix, 4x4 components, column major, OpenGL style, right-handed </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Matrix
{
    /// <summary> Matrix first row (4 components) </summary>
    public float /*float*/ M0;
    /// <summary> Matrix first row (4 components) </summary>
    public float /*float*/ M4;
    /// <summary> Matrix first row (4 components) </summary>
    public float /*float*/ M8;
    /// <summary> Matrix first row (4 components) </summary>
    public float /*float*/ M12;
    /// <summary> Matrix second row (4 components) </summary>
    public float /*float*/ M1;
    /// <summary> Matrix second row (4 components) </summary>
    public float /*float*/ M5;
    /// <summary> Matrix second row (4 components) </summary>
    public float /*float*/ M9;
    /// <summary> Matrix second row (4 components) </summary>
    public float /*float*/ M13;
    /// <summary> Matrix third row (4 components) </summary>
    public float /*float*/ M2;
    /// <summary> Matrix third row (4 components) </summary>
    public float /*float*/ M6;
    /// <summary> Matrix third row (4 components) </summary>
    public float /*float*/ M10;
    /// <summary> Matrix third row (4 components) </summary>
    public float /*float*/ M14;
    /// <summary> Matrix fourth row (4 components) </summary>
    public float /*float*/ M3;
    /// <summary> Matrix fourth row (4 components) </summary>
    public float /*float*/ M7;
    /// <summary> Matrix fourth row (4 components) </summary>
    public float /*float*/ M11;
    /// <summary> Matrix fourth row (4 components) </summary>
    public float /*float*/ M15;
}
#pragma warning restore CA1711,IDE0005

