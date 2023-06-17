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
    public fixed float Leftlenscenter[2];
    /// <summary> VR right lens center </summary>
    public fixed float Rightlenscenter[2];
    /// <summary> VR left screen center </summary>
    public fixed float Leftscreencenter[2];
    /// <summary> VR right screen center </summary>
    public fixed float Rightscreencenter[2];
    /// <summary> VR distortion scale </summary>
    public fixed float Scale[2];
    /// <summary> VR distortion scale in </summary>
    public fixed float Scalein[2];
}

#pragma warning restore CA1711,IDE0005
