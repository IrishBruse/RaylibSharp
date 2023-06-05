namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> VrStereoConfig, VR stereo rendering configuration for simulator </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct VrStereoConfig
{
    /// <summary> VR projection matrices (per eye) </summary>
    public Matrix /* Matrix[2] */ Projection1;
    /// <summary> VR projection matrices (per eye) </summary>
    public Matrix /* Matrix[2] */ Projection;
    /// <summary> VR view offset matrices (per eye) </summary>
    public Matrix /* Matrix[2] */ Viewoffset1;
    /// <summary> VR view offset matrices (per eye) </summary>
    public Matrix /* Matrix[2] */ Viewoffset;
    /// <summary> VR left lens center </summary>
    public Vector2 /* float[2] */ Leftlenscenter;
    /// <summary> VR right lens center </summary>
    public Vector2 /* float[2] */ Rightlenscenter;
    /// <summary> VR left screen center </summary>
    public Vector2 /* float[2] */ Leftscreencenter;
    /// <summary> VR right screen center </summary>
    public Vector2 /* float[2] */ Rightscreencenter;
    /// <summary> VR distortion scale </summary>
    public Vector2 /* float[2] */ Scale;
    /// <summary> VR distortion scale in </summary>
    public Vector2 /* float[2] */ Scalein;
}

#pragma warning restore CA1711,IDE0005
