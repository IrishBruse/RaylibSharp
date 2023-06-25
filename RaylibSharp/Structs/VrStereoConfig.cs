namespace RaylibSharp;

#pragma warning disable CA1711,IDE0005,CA1051

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;
using System.Runtime.InteropServices.Marshalling;

/// <summary> VrStereoConfig, VR stereo rendering configuration for simulator </summary>
public unsafe partial struct VrStereoConfig
{
    /// <summary> VR projection matrices (per eye) </summary>
    public Matrix4x4 ProjectionL;
    /// <summary> VR projection matrices (per eye) </summary>
    public Matrix4x4 ProjectionR;
    /// <summary> VR view offset matrices (per eye) </summary>
    public Matrix4x4 ViewoffsetL;
    /// <summary> VR view offset matrices (per eye) </summary>
    public Matrix4x4 ViewoffsetR;
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

/// <summary> VrStereoConfig, VR stereo rendering configuration for simulator </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct UnmanagedVrStereoConfig
{
    /// <summary> VR projection matrices (per eye) </summary>
    public fixed float ProjectionL[16];
    /// <summary> VR projection matrices (per eye) </summary>
    public fixed float ProjectionR[16];
    /// <summary> VR view offset matrices (per eye) </summary>
    public fixed float ViewoffsetL[16];
    /// <summary> VR view offset matrices (per eye) </summary>
    public fixed float ViewoffsetR[16];
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
