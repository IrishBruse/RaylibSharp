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
    public Matrix4x4 ViewOffsetL;
    /// <summary> VR view offset matrices (per eye) </summary>
    public Matrix4x4 ViewOffsetR;
    /// <summary> VR left lens center </summary>
    public Vector2 LeftLensCenter;
    /// <summary> VR right lens center </summary>
    public Vector2 RightLensCenter;
    /// <summary> VR left screen center </summary>
    public Vector2 LeftScreenCenter;
    /// <summary> VR right screen center </summary>
    public Vector2 RightScreenCenter;
    /// <summary> VR distortion scale </summary>
    public Vector2 Scale;
    /// <summary> VR distortion scale in </summary>
    public Vector2 ScaleIn;
}

/// <summary> VrStereoConfig, VR stereo rendering configuration for simulator </summary>
[StructLayout(LayoutKind.Sequential)]
internal unsafe struct UnmanagedVrStereoConfig
{
    /// <summary> VR projection matrices (per eye) </summary>
    public fixed float ProjectionL[16];
    /// <summary> VR projection matrices (per eye) </summary>
    public fixed float ProjectionR[16];
    /// <summary> VR view offset matrices (per eye) </summary>
    public fixed float ViewOffsetL[16];
    /// <summary> VR view offset matrices (per eye) </summary>
    public fixed float ViewOffsetR[16];
    /// <summary> VR left lens center </summary>
    public fixed float LeftLensCenter[2];
    /// <summary> VR right lens center </summary>
    public fixed float RightLensCenter[2];
    /// <summary> VR left screen center </summary>
    public fixed float LeftScreenCenter[2];
    /// <summary> VR right screen center </summary>
    public fixed float RightScreenCenter[2];
    /// <summary> VR distortion scale </summary>
    public fixed float Scale[2];
    /// <summary> VR distortion scale in </summary>
    public fixed float ScaleIn[2];
}

#pragma warning restore CA1711,IDE0005
