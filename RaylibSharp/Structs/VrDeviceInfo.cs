namespace Raylib;

#pragma warning disable CA1711,IDE0005

using System.Runtime.InteropServices;
using System.Numerics;
using System.Drawing;

/// <summary> VrDeviceInfo, Head-Mounted-Display device parameters </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct VrDeviceInfo
{
    /// <summary> Horizontal resolution in pixels </summary>
    public int /* int */ Hresolution;
    /// <summary> Vertical resolution in pixels </summary>
    public int /* int */ Vresolution;
    /// <summary> Horizontal size in meters </summary>
    public float /* float */ Hscreensize;
    /// <summary> Vertical size in meters </summary>
    public float /* float */ Vscreensize;
    /// <summary> Screen center in meters </summary>
    public float /* float */ Vscreencenter;
    /// <summary> Distance between eye and display in meters </summary>
    public float /* float */ Eyetoscreendistance;
    /// <summary> Lens separation distance in meters </summary>
    public float /* float */ Lensseparationdistance;
    /// <summary> IPD (distance between pupils) in meters </summary>
    public float /* float */ Interpupillarydistance;
    /// <summary> Lens distortion constant parameters </summary>
    public Vector4 /* float[4] */ Lensdistortionvalues;
    /// <summary> Chromatic aberration correction parameters </summary>
    public Vector4 /* float[4] */ Chromaabcorrection;
}

#pragma warning restore CA1711,IDE0005
