namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> VrStereoConfig, VR stereo rendering configuration for simulator </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct VrStereoConfig
{
    /// <summary> VR projection matrices (per eye) </summary>
    public Matrix4x4 Projection1;
    /// <summary> VR projection matrices (per eye) </summary>
    public Matrix4x4 Projection2;
    /// <summary> VR view offset matrices (per eye) </summary>
    public Matrix4x4 Viewoffset1;
    /// <summary> VR view offset matrices (per eye) </summary>
    public Matrix4x4 Viewoffset2;
    /// <summary> VR left lens center </summary>
    public Vector2 Leftlenscenter;
    /// <summary> VR right lens center </summary>
    public Vector2 Rightlenscenter;
    /// <summary> VR left screen center </summary>
    public Vector2 Leftscreencenter;
    /// <summary> VR right screen center </summary>
    public Vector2 Rightscreencenter;
    /// <summary> VR distortion scale </summary>
    public Vector2 Scale;
    /// <summary> VR distortion scale in </summary>
    public Vector2 Scalein;
}

#pragma warning restore CA1711,IDE0005
